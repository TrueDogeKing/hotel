#!/usr/bin/env bash
# Auto-deploy: checks origin/main and, if there is a new commit, pulls it and
# rebuilds the production stack. Run repeatedly by cron (see
# scripts/register-deploy-cron.sh) rather than a server/webhook, so it needs
# no exposed port at all.
#
# Deliberately never runs `git reset --hard` / `git clean`: if the working tree
# is not clean, it stops and logs that instead of touching anything. This is
# meant to run unattended, so silently discarding local changes would be worse
# than a failed deploy.
#
# Linux counterpart of scripts/deploy.ps1 — see that file for the Windows
# version and the PowerShell-specific pitfall (stderr-as-terminating-error)
# that does not apply here, since bash does not treat a command's stderr as an
# exception the way strict-mode PowerShell does.

set -uo pipefail

repo_root="$(cd "$(dirname "${BASH_SOURCE[0]}")/.." && pwd)"
cd "$repo_root"

log_file="$repo_root/deploy.log"
log() {
    printf '%s  %s\n' "$(date '+%Y-%m-%d %H:%M:%S')" "$1" | tee -a "$log_file"
}

git fetch origin main >/dev/null 2>&1
if [ $? -ne 0 ]; then
    log "ERROR: git fetch failed"
    exit 1
fi

local_rev="$(git rev-parse HEAD)"
remote_rev="$(git rev-parse origin/main)"

if [ "$local_rev" = "$remote_rev" ]; then
    # Quiet exit: logging "nothing new" every 5 minutes would just be noise,
    # and this is the expected, common case.
    exit 0
fi

log "New commit on origin/main: ${local_rev:0:7} -> ${remote_rev:0:7}"

if [ -n "$(git status --porcelain)" ]; then
    log "ABORTED: working tree is not clean, refusing to touch it automatically. Check manually: git status"
    exit 1
fi

if ! git pull --ff-only origin main >>"$log_file" 2>&1; then
    log "ERROR: git pull failed"
    exit 1
fi

# The API applies EF Core migrations on startup (Database__MigrateAutomatically),
# so the riskiest moment for the data is right after this deploy. Not fatal: a
# deploy that touches nothing but the frontend should not be blocked by it, and
# the daily backup still runs on its own schedule.
log "Backing up the database before deploying..."
if ! docker compose --env-file .env -f docker/docker-compose.prod.yml \
    exec -T db-backup /bin/sh /usr/local/bin/backup-db.sh predeploy >>"$log_file" 2>&1; then
    log "WARNING: pre-deploy backup failed, continuing (check backups/backup.log)"
fi

log "Rebuilding and restarting containers..."
if ! docker compose --env-file .env -f docker/docker-compose.prod.yml up -d --build >>"$log_file" 2>&1; then
    log "ERROR: docker compose up failed"
    exit 1
fi

log "Deploy finished: now at ${remote_rev:0:7}"
