#!/usr/bin/env bash
# Registers scripts/deploy.sh as a cron job, run every 5 minutes. Run this
# ONCE on the machine hosting production. Does not need sudo as long as the
# user running it also owns the Docker socket (i.e. is in the `docker` group).

set -euo pipefail

repo_root="$(cd "$(dirname "${BASH_SOURCE[0]}")/.." && pwd)"
script_path="$repo_root/scripts/deploy.sh"
chmod +x "$script_path"

cron_line="*/5 * * * * $script_path >/dev/null 2>&1"
marker="# campcenter-auto-deploy"

# Idempotent: replaces a previous registration instead of appending a
# duplicate line if this is run more than once.
(crontab -l 2>/dev/null | grep -v "$marker"; echo "$cron_line $marker") | crontab -

echo "Registered. Check with: crontab -l"
echo "Logs land in: $repo_root/deploy.log"
