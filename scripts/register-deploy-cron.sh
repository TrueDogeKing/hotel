#!/usr/bin/env bash
# Registers scripts/deploy.sh with cron: every 5 minutes, plus once at boot.
# Run this ONCE on the machine hosting production. Does not need sudo as long as
# the user running it also owns the Docker socket (i.e. is in the `docker` group).
#
# The @reboot line is what makes the site come back on its own after a power
# cut: cron itself resumes its schedule at boot, but a scheduled run only
# rebuilds when there is a new commit, so on a quiet week nothing would ever
# start the stack. `deploy.sh boot` waits for the daemon and runs
# `docker compose up -d` unconditionally instead.
#
# Written for Ubuntu (the production host). The pre-flight below checks the three
# things that actually decide whether the site comes back on its own after a
# power cut: cron installed and enabled, Docker enabled as a system service, and
# this user able to talk to the Docker socket without sudo.

set -euo pipefail

repo_root="$(cd "$(dirname "${BASH_SOURCE[0]}")/.." && pwd)"
script_path="$repo_root/scripts/deploy.sh"
chmod +x "$script_path"

# --- Pre-flight ---------------------------------------------------------------
# Registering into a crontab that never runs, or for a user who cannot reach
# Docker, produces a machine that looks configured and silently stays down. Say
# so now instead.

problems=0

if ! command -v crontab >/dev/null 2>&1; then
    echo "FATAL: crontab is not installed. On Ubuntu: sudo apt install cron"
    exit 1
fi

# `is-enabled` is the one that matters: it answers "will this start at boot",
# which "is-active" does not.
if command -v systemctl >/dev/null 2>&1; then
    if [ "$(systemctl is-enabled cron 2>/dev/null)" != "enabled" ]; then
        echo "WARNING: the cron service is not enabled at boot, so @reboot will never fire."
        echo "         Fix with: sudo systemctl enable --now cron"
        problems=$((problems + 1))
    fi

    if [ "$(systemctl is-enabled docker 2>/dev/null)" != "enabled" ]; then
        echo "WARNING: the Docker service is not enabled at boot, so nothing will restart the containers."
        echo "         Fix with: sudo systemctl enable --now docker"
        problems=$((problems + 1))
    fi
fi

if ! docker info >/dev/null 2>&1; then
    echo "WARNING: cannot talk to Docker as $(id -un). Every deploy will fail, quietly."
    echo "         Fix with: sudo usermod -aG docker $(id -un)   (then log out and back in)"
    problems=$((problems + 1))
fi

if [ ! -f "$repo_root/.env" ]; then
    echo "WARNING: $repo_root/.env is missing; docker compose will refuse to start the stack."
    echo "         Copy .env.example to .env and fill in the production values."
    problems=$((problems + 1))
fi
# ------------------------------------------------------------------------------

marker="# campcenter-auto-deploy"
schedule_line="*/5 * * * * $script_path >/dev/null 2>&1 $marker"
boot_line="@reboot $script_path boot >/dev/null 2>&1 $marker"

# Idempotent: replaces a previous registration instead of appending
# duplicate lines if this is run more than once.
#
# `|| true` is load-bearing: on a host with no crontab yet - a fresh server, the
# usual case - grep matches nothing and exits 1, which under `set -e` aborted the
# whole registration before anything was written.
(
    crontab -l 2>/dev/null | grep -v "$marker" || true
    echo "$schedule_line"
    echo "$boot_line"
) | crontab -

echo "Registered (every 5 minutes + at boot). Check with: crontab -l"
echo "Logs land in: $repo_root/deploy.log"
echo "Test the boot path without rebooting: $script_path boot"

if [ "$problems" -gt 0 ]; then
    echo
    echo "$problems warning(s) above: the cron entries exist, but the host is not"
    echo "ready to bring the site back on its own yet. Fix them, then re-check."
    exit 1
fi
