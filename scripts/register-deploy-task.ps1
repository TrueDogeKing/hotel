# Registers scripts/deploy.ps1 as a Windows Task Scheduler job, run every 5
# minutes. Run this ONCE, from an elevated PowerShell, on the machine hosting
# production.
#
# MultipleInstances IgnoreNew matters here: without it, two overlapping
# `docker compose up --build` runs could trip over each other.

$ErrorActionPreference = "Stop"
$repoRoot = Split-Path -Parent $PSScriptRoot
$scriptPath = Join-Path $repoRoot "scripts\deploy.ps1"

$action = New-ScheduledTaskAction `
    -Execute "powershell.exe" `
    -Argument "-NoProfile -ExecutionPolicy Bypass -File `"$scriptPath`""

$trigger = New-ScheduledTaskTrigger -Once -At (Get-Date) `
    -RepetitionInterval (New-TimeSpan -Minutes 5) `
    -RepetitionDuration ([TimeSpan]::MaxValue)

$settings = New-ScheduledTaskSettingsSet `
    -MultipleInstances IgnoreNew `
    -StartWhenAvailable `
    -DontStopOnIdleEnd

Register-ScheduledTask `
    -TaskName "CampCenter Auto-Deploy" `
    -Action $action `
    -Trigger $trigger `
    -Settings $settings `
    -Description "Every 5 min: check origin/main, pull + docker compose up --build if there is a new commit." `
    -RunLevel Highest `
    -User $env:USERNAME

Write-Host "Registered. Check with: Get-ScheduledTask -TaskName 'CampCenter Auto-Deploy'"
