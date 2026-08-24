# Registers two Windows Task Scheduler jobs. Run this ONCE, from an elevated
# PowerShell, on the machine hosting production. Re-running it is safe: both
# tasks are replaced, not duplicated.
#
#   CampCenter Auto-Deploy  - every 5 minutes: pull + rebuild if origin/main moved.
#   CampCenter Boot-Up      - at system startup: wait for Docker, then bring the
#                             stack up. This is the one that matters after a power
#                             cut, because a scheduled run does nothing at all when
#                             there is no new commit, so on a quiet week nothing
#                             would ever start the containers again.
#
# -LogonType S4U on both is the other half of surviving a power cut. The default
# (InteractiveToken) only runs a task while that user is logged on - so a machine
# that reboots to the lock screen and waits for someone to type a password would
# never deploy and never come back up. S4U runs the task whether or not anyone is
# logged on, and unlike -Password it needs no stored credential.
#
# MultipleInstances IgnoreNew matters here: without it, two overlapping
# `docker compose up --build` runs could trip over each other.
#
# WINDOWS CAVEAT, please read: Docker Desktop runs inside a user session, so it
# does not start until someone logs on - and until it does, neither task can
# reach the daemon, whatever their triggers say. On a machine that must survive
# an unattended reboot you need one of:
#   * Docker Desktop set to "Start Docker Desktop when you log in", plus Windows
#     configured to log this account in automatically after boot; or
#   * Docker Engine running as a service (Linux host, or WSL2 with a systemd
#     distro), which is what scripts/deploy.sh + register-deploy-cron.sh are
#     written for and the more dependable arrangement for a server.
# The Boot-Up task waits five minutes for the daemon and logs a clear error to
# deploy.log if it never appears, so a misconfigured host is visible rather than
# silently down.

$ErrorActionPreference = "Stop"
$repoRoot = Split-Path -Parent $PSScriptRoot
$scriptPath = Join-Path $repoRoot "scripts\deploy.ps1"

$settings = New-ScheduledTaskSettingsSet `
    -MultipleInstances IgnoreNew `
    -StartWhenAvailable `
    -DontStopOnIdleEnd

$principal = New-ScheduledTaskPrincipal `
    -UserId "$env:USERDOMAIN\$env:USERNAME" `
    -LogonType S4U `
    -RunLevel Highest

# --- Every 5 minutes: check origin/main. ---

$deployAction = New-ScheduledTaskAction `
    -Execute "powershell.exe" `
    -Argument "-NoProfile -ExecutionPolicy Bypass -File `"$scriptPath`" -Mode scheduled"

$deployTrigger = New-ScheduledTaskTrigger -Once -At (Get-Date) `
    -RepetitionInterval (New-TimeSpan -Minutes 5) `
    -RepetitionDuration ([TimeSpan]::MaxValue)

Register-ScheduledTask `
    -TaskName "CampCenter Auto-Deploy" `
    -Action $deployAction `
    -Trigger $deployTrigger `
    -Settings $settings `
    -Principal $principal `
    -Description "Every 5 min: check origin/main, pull + docker compose up --build if there is a new commit." `
    -Force

# --- At boot: make sure the stack is running. ---

$bootAction = New-ScheduledTaskAction `
    -Execute "powershell.exe" `
    -Argument "-NoProfile -ExecutionPolicy Bypass -File `"$scriptPath`" -Mode boot"

$bootTrigger = New-ScheduledTaskTrigger -AtStartup

Register-ScheduledTask `
    -TaskName "CampCenter Boot-Up" `
    -Action $bootAction `
    -Trigger $bootTrigger `
    -Settings $settings `
    -Principal $principal `
    -Description "At startup (e.g. after a power cut): wait for Docker, then docker compose up -d." `
    -Force

Write-Host "Registered. Check with: Get-ScheduledTask -TaskName 'CampCenter *'"
Write-Host "Test the boot path without rebooting: Start-ScheduledTask -TaskName 'CampCenter Boot-Up'"
Write-Host "Logs land in: $(Join-Path $repoRoot 'deploy.log')"
