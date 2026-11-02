# Script to update report connection strings for Docker deployment
# Run this during Docker build to replace |DataDirectory| with absolute paths

param(
    [switch]$Revert  # Use -Revert to restore |DataDirectory| for local development
)

$reportsRoot = "wwwroot\ReportsRoot"

if ($Revert) {
    Write-Host "Reverting reports to use |DataDirectory| for local development..."
    # Replace absolute Docker paths with |DataDirectory| token
    $pattern = 'Data Source=/app/wwwroot/App_Data/'
    $replacement = 'Data Source=|DataDirectory|\'
} else {
    Write-Host "Updating reports for Docker deployment..."
    # Replace |DataDirectory| token with absolute Docker paths
    $pattern = 'Data Source=\|DataDirectory\|\\'
    $replacement = 'Data Source=/app/wwwroot/App_Data/'
}

$files = Get-ChildItem -Path $reportsRoot -Filter "*.flxr" -Recurse

$count = 0
foreach ($file in $files) {
    $content = Get-Content $file.FullName -Raw
    $newContent = $content -replace $pattern, $replacement
    if ($newContent -ne $content) {
        Set-Content -Path $file.FullName -Value $newContent -NoNewline
        $count++
        Write-Host "  Updated: $($file.Name)"
    }
}

Write-Host "`nTotal files updated: $count"
