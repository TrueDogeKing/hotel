# Auto-deploy: checks origin/main and, if there is a new commit, pulls it and
# rebuilds the production stack. Run repeatedly by Windows Task Scheduler (see
# scripts/register-deploy-task.ps1) rather than a server/webhook, so it needs
# no exposed port at all.
#
# Deliberately never runs `git reset --hard` / `git clean`: if the working tree
# is not clean, it stops and logs that instead of touching anything. This is
# meant to run unattended, so silently discarding local changes would be worse
# than a failed deploy.
#
# ASCII-only on purpose: Windows PowerShell 5.1 reads a BOM-less .ps1 using the
# system codepage, not UTF-8, so non-ASCII text here is a real source of parser
# errors depending on the machine's locale. Keep it plain.
#
# Called with -Mode boot (see the "CampCenter Boot-Up" task in
# scripts/register-deploy-task.ps1) it first waits for the Docker daemon and
# brings the stack up, then carries on with the usual update check. That is the
# path that matters after a power cut.
#
# $ErrorActionPreference stays at its default (Continue) rather than "Stop":
# under Stop, redirecting a native command's stderr with 2>&1 turns every line
# it writes there — including git's routine "From https://..." progress notes,
# which are not errors — into a terminating exception. Real native-command
# failures are instead caught explicitly below via $LASTEXITCODE.

param(
    # "boot" when Task Scheduler fires this at startup, "scheduled" every 5 minutes.
    [ValidateSet("scheduled", "boot")]
    [string]$Mode = "scheduled"
)

$repoRoot = Split-Path -Parent $PSScriptRoot
Set-Location $repoRoot

$logFile = Join-Path $repoRoot "deploy.log"
function Write-Log($message) {
    $line = "$(Get-Date -Format 'yyyy-MM-dd HH:mm:ss')  $message"
    Add-Content -Path $logFile -Value $line
    Write-Host $line
}

if ($Mode -eq "boot") {
    Write-Log "Boot run: making sure the stack is up."

    # At boot this races Docker Desktop, which is never accepting connections
    # yet. Five minutes of patience, then give up and let the 5-minute schedule
    # retry.
    $dockerReady = $false
    foreach ($attempt in 1..60) {
        docker info 2>&1 | Out-Null
        if ($LASTEXITCODE -eq 0) { $dockerReady = $true; break }
        Start-Sleep -Seconds 5
    }

    if (-not $dockerReady) {
        Write-Log "ERROR: Docker is still not responding 5 minutes after boot; the scheduled runs will retry"
        exit 1
    }

    # `restart: unless-stopped` already brings back containers that were running
    # when the power went - but not ones that a `docker compose down` (or a failed
    # deploy) left absent, and not if the daemon itself was down at the time. This
    # is idempotent: containers already running are left alone, and no --build, so
    # it starts the images that are already on disk instead of waiting on a rebuild.
    docker compose --env-file .env -f docker/docker-compose.prod.yml up -d 2>&1 |
        ForEach-Object { Write-Log "  $_" }
    if ($LASTEXITCODE -ne 0) {
        Write-Log "ERROR: docker compose up failed on the boot run"
        exit 1
    }

    Write-Log "Stack is up."
}

try {
    git fetch origin main 2>&1 | Out-Null
    if ($LASTEXITCODE -ne 0) { throw "git fetch failed (exit $LASTEXITCODE)" }

    $local = git rev-parse HEAD
    $remote = git rev-parse origin/main

    if ($local -eq $remote) {
        # Quiet exit: logging "nothing new" every 5 minutes would just be noise,
        # and this is the expected, common case.
        exit 0
    }

    Write-Log "New commit on origin/main: $($local.Substring(0,7)) -> $($remote.Substring(0,7))"

    $dirty = git status --porcelain
    if ($dirty) {
        Write-Log "ABORTED: working tree is not clean, refusing to touch it automatically. Check manually: git status"
        exit 1
    }

    git pull --ff-only origin main 2>&1 | ForEach-Object { Write-Log "  $_" }
    if ($LASTEXITCODE -ne 0) { throw "git pull failed (exit $LASTEXITCODE)" }

    # The API applies EF Core migrations on startup (Database__MigrateAutomatically),
    # so the riskiest moment for the data is right after this deploy. Not fatal: a
    # deploy that touches nothing but the frontend should not be blocked by it, and
    # the daily backup still runs on its own schedule.
    Write-Log "Backing up the database before deploying..."
    docker compose --env-file .env -f docker/docker-compose.prod.yml `
        exec -T db-backup /bin/sh /usr/local/bin/backup-db.sh predeploy 2>&1 |
        ForEach-Object { Write-Log "  $_" }
    if ($LASTEXITCODE -ne 0) {
        Write-Log "WARNING: pre-deploy backup failed, continuing (check backups/backup.log)"
    }

    Write-Log "Rebuilding and restarting containers..."
    docker compose --env-file .env -f docker/docker-compose.prod.yml up -d --build 2>&1 |
        ForEach-Object { Write-Log "  $_" }
    if ($LASTEXITCODE -ne 0) { throw "docker compose up failed (exit $LASTEXITCODE)" }

    Write-Log "Deploy finished: now at $($remote.Substring(0,7))"
}
catch {
    Write-Log "ERROR: $_"
    exit 1
}
