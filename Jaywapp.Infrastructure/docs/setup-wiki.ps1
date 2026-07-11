# GitHub Wiki 초기화 스크립트 (Windows PowerShell)
# 사용법:
#   1. GitHub 웹 → Wiki 탭 → "Create the first page" 클릭 (아무 내용이나 Save)
#   2. 이 스크립트 실행: powershell -ExecutionPolicy Bypass -File docs\setup-wiki.ps1

$RepoRoot = Split-Path -Parent $PSScriptRoot
if (-not $RepoRoot) { $RepoRoot = Split-Path -Parent (Split-Path -Parent $MyInvocation.MyCommand.Path) }

$WikiDir = Join-Path $env:TEMP "wiki-$(Get-Random)"
New-Item -ItemType Directory -Path $WikiDir -Force | Out-Null

Write-Host "Cloning wiki repository..."
git clone git@github.com:jaywapp/Jaywapp.Infrastructure.wiki.git $WikiDir

if ($LASTEXITCODE -ne 0) {
    Write-Host "ERROR: Wiki repository not found." -ForegroundColor Red
    Write-Host "Please create the first wiki page on GitHub web UI first:" -ForegroundColor Yellow
    Write-Host "  https://github.com/jaywapp/Jaywapp.Infrastructure/wiki/_new" -ForegroundColor Cyan
    Remove-Item -Recurse -Force $WikiDir -ErrorAction SilentlyContinue
    exit 1
}

Write-Host "Copying wiki pages..."
Copy-Item "$RepoRoot\docs\wiki\*.md" $WikiDir -Force

Push-Location $WikiDir
git add -A
git commit -m "Add wiki pages: Architecture, Common, Helpers, Filtering, Refactoring Log"
git push origin master
Pop-Location

Remove-Item -Recurse -Force $WikiDir
Write-Host "Wiki pages pushed successfully!" -ForegroundColor Green
