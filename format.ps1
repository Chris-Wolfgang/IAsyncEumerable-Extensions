#!/usr/bin/env pwsh
<#
.SYNOPSIS
    Formats all C# code in the repository using dotnet format.

.DESCRIPTION
    This script runs 'dotnet format' on the solution to ensure consistent code formatting.
    Run this before committing to ensure your code passes the formatting checks in CI.

.PARAMETER Check
    If specified, only checks formatting without making changes (like CI does).

.EXAMPLE
    .\format.ps1
    Formats all code in the repository.

.EXAMPLE
    .\format.ps1 -Check
    Checks formatting without making changes.
#>

param(
    [switch]$Check
)

$ErrorActionPreference = "Stop"

Write-Host "🎨 Code Formatting Script" -ForegroundColor Cyan
Write-Host ""

# Find solution file
$solutions = Get-ChildItem -Path . -Filter "*.sln*" -File | Select-Object -First 1

if (-not $solutions)
{
    Write-Host "❌ No solution file found!" -ForegroundColor Red
    exit 1
}

$solutionFile = $solutions.FullName
Write-Host "📁 Found solution: $($solutions.Name)" -ForegroundColor Green
Write-Host ""

if ($Check)
{
    Write-Host "🔍 Checking code formatting (read-only mode)..." -ForegroundColor Yellow
    Write-Host ""
    
    dotnet format $solutionFile --verify-no-changes --verbosity diagnostic
    
    if ($LASTEXITCODE -eq 0)
    {
        Write-Host ""
        Write-Host "✅ All files are properly formatted!" -ForegroundColor Green
    }
    else
    {
        Write-Host ""
        Write-Host "❌ Formatting issues detected!" -ForegroundColor Red
        Write-Host "Run '.\format.ps1' (without -Check) to fix them automatically." -ForegroundColor Yellow
        exit 1
    }
}
else
{
    Write-Host "✏️  Formatting code..." -ForegroundColor Yellow
    Write-Host ""
    
    dotnet format $solutionFile --verbosity diagnostic
    
    if ($LASTEXITCODE -eq 0)
    {
        Write-Host ""
        Write-Host "✅ Code formatting complete!" -ForegroundColor Green
        Write-Host "Review changes and commit them." -ForegroundColor Cyan
    }
    else
    {
        Write-Host ""
        Write-Host "❌ Formatting failed!" -ForegroundColor Red
        exit 1
    }
}
