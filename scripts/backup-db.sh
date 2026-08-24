#!/bin/sh
# Automatic PostgreSQL backups for the production stack.
#
# Runs inside the `db-backup` sidecar of docker/docker-compose.prod.yml, which
# uses the same postgres:16-alpine image as the server itself so pg_dump always
# matches the server version. The file is bind-mounted into the container, so
# `git pull` alone updates it — no rebuild needed.
#
# Modes:
#   backup-db.sh [label]  one dump now; the optional label ends up in the file
#                         name (deploy.sh passes "predeploy")
#   backup-db.sh --daemon dump once if the last one is older than
#                         BACKUP_CATCHUP_HOURS, then every day at BACKUP_TIME
#   backup-db.sh --check  healthcheck: is there a dump newer than
#                         BACKUP_STALE_HOURS?
#
# The schedule is a plain poll loop rather than cron on purpose: busybox crond
# would run the job with an environment it rebuilds itself, and the database
# password reaches this script through the environment (PGPASSWORD). Looping in
# the daemon process keeps the credentials where they already are, and the only
# schedule this needs is "once a day".

set -u

BACKUP_DIR=${BACKUP_DIR:-/backups}
BACKUP_TIME=${BACKUP_TIME:-03:30}
BACKUP_RETENTION_DAYS=${BACKUP_RETENTION_DAYS:-14}
BACKUP_CATCHUP_HOURS=${BACKUP_CATCHUP_HOURS:-24}
BACKUP_STALE_HOURS=${BACKUP_STALE_HOURS:-36}

PGHOST=${PGHOST:-postgres}
PGUSER=${PGUSER:-campcenter}
PGDATABASE=${PGDATABASE:-campcenter}
export PGHOST PGUSER PGDATABASE

log_file="$BACKUP_DIR/backup.log"

log() {
    line="$(date '+%Y-%m-%d %H:%M:%S')  $*"
    # stdout goes to `docker compose logs db-backup`; the file keeps the same
    # history next to the dumps, where whoever collects them will look first.
    echo "$line"
    if [ -w "$BACKUP_DIR" ]; then
        echo "$line" >>"$log_file"
    fi
}

# Files older than the retention window, deleted only after a fresh dump landed
# (see run_backup) so a broken database never ages the good backups away.
# Deletes with `rm` in a loop instead of find -delete: -delete is an optional
# busybox applet feature and this runs on busybox find.
prune_old() {
    find "$BACKUP_DIR" -maxdepth 1 -type f \
        -name "${PGDATABASE}-*.sql.gz" \
        -mtime "+$BACKUP_RETENTION_DAYS" 2>/dev/null |
        while read -r old; do
            rm -f "$old" && log "Pruned (older than ${BACKUP_RETENTION_DAYS}d): $(basename "$old")"
        done
}

# Newest dump within the last $1 hours, empty if there is none.
recent_backup() {
    find "$BACKUP_DIR" -maxdepth 1 -type f \
        -name "${PGDATABASE}-*.sql.gz" \
        -mmin "-$(( $1 * 60 ))" 2>/dev/null | head -n 1
}

run_backup() {
    label=${1:-}
    [ -n "$label" ] && label="-$label"

    if [ ! -d "$BACKUP_DIR" ] || [ ! -w "$BACKUP_DIR" ]; then
        log "ERROR: $BACKUP_DIR is missing or not writable, no backup taken"
        return 1
    fi

    if ! pg_isready -q -t 15; then
        log "ERROR: no answer from $PGUSER@$PGHOST/$PGDATABASE, no backup taken"
        return 1
    fi

    stamp=$(date '+%Y%m%d-%H%M%S')
    target="$BACKUP_DIR/${PGDATABASE}-${stamp}${label}.sql.gz"
    plain="$target.plain.part"
    part="$target.part"

    # --clean --if-exists so the dump can be replayed straight into an existing
    # database; --no-owner --no-privileges so it also restores under a role with
    # a different name. Written as *.part and renamed only once gzip verifies,
    # so an interrupted run never leaves a truncated file looking like a backup.
    if ! pg_dump --clean --if-exists --no-owner --no-privileges >"$plain"; then
        log "ERROR: pg_dump failed, previous backups left untouched"
        rm -f "$plain"
        return 1
    fi

    if ! gzip -9 -c "$plain" >"$part" || ! gzip -t "$part"; then
        log "ERROR: compressing the dump failed, previous backups left untouched"
        rm -f "$plain" "$part"
        return 1
    fi
    rm -f "$plain"
    mv "$part" "$target"

    # ls, not du: du reports blocks used, which is 0 for a small file on some
    # mounted filesystems, and "Backup OK (0)" reads like a broken backup.
    log "Backup OK: $(basename "$target") ($(ls -lh "$target" | awk '{print $5}'))"
    prune_old
    return 0
}

# A bare run means "dump now": the sidecar passes --daemon explicitly, so no
# argument means a human is running this by hand.
case "${1:-manual}" in
    --check)
        # Healthcheck. Unhealthy means "the backups stopped happening", which is
        # the failure worth noticing — a container that is up but no longer
        # dumping looks exactly like a working one from the outside.
        if [ -n "$(recent_backup "$BACKUP_STALE_HOURS")" ]; then
            exit 0
        fi
        echo "no backup of $PGDATABASE in the last ${BACKUP_STALE_HOURS}h"
        exit 1
        ;;

    --daemon)
        case "$BACKUP_TIME" in
            [0-2][0-9]:[0-5][0-9]) ;;
            *)
                echo "ERROR: BACKUP_TIME must be HH:MM, got '$BACKUP_TIME'" >&2
                exit 1
                ;;
        esac

        log "Backup daemon started: daily at $BACKUP_TIME $(date '+%Z'), keeping ${BACKUP_RETENTION_DAYS} days in $BACKUP_DIR"

        last_run=""
        last_catchup=0
        while :; do
            if [ "$(date '+%H:%M')" = "$BACKUP_TIME" ] && [ "$(date '+%F')" != "$last_run" ]; then
                # The daily run. Marked as done before dumping, so a failure
                # does not retry every 20 seconds for the rest of the day —
                # the catch-up below picks it up an hour later instead.
                last_run=$(date '+%F')
                run_backup

            elif [ "$(( $(date '+%s') - last_catchup ))" -ge 3600 ]; then
                # Catch-up, checked hourly and immediately on startup: covers
                # the first start, downtime across BACKUP_TIME, and a dump that
                # failed because the database was not up yet.
                last_catchup=$(date '+%s')
                if [ -z "$(recent_backup "$BACKUP_CATCHUP_HOURS")" ]; then
                    log "No backup in the last ${BACKUP_CATCHUP_HOURS}h, taking one now"
                    run_backup
                fi
            fi
            sleep 20
        done
        ;;

    --*)
        echo "usage: backup-db.sh [label | --daemon | --check]" >&2
        exit 2
        ;;

    *)
        run_backup "${1:-manual}"
        exit $?
        ;;
esac
