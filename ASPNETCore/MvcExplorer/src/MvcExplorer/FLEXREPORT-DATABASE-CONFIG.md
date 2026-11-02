# FlexReport Database Connection Configuration

## Problem

FlexReport files (.flxr) need different database connection strings when running locally vs in Docker:

- **Local**: `Data Source=|DataDirectory|\C1NWind.db` (uses AppDomain DataDirectory)
- **Docker**: `Data Source=|DataDirectory|/C1NWind.db` (absolute Linux path)

## Solution

### For Local Development

Report files use `|DataDirectory|` token which is automatically resolved by the AppDomain configuration in `Startup.cs`:

```csharp
AppDomain.CurrentDomain.SetData("DataDirectory", appDataPath);
```

Connection string in .flxr files:

```xml
<ConnectionString>Data Source=|DataDirectory|\C1NWind.db</ConnectionString>
```

### For Docker Deployment

The Dockerfile automatically converts connection strings during the build process using a PowerShell command that replaces `|DataDirectory|` with absolute paths.

The conversion happens in the Dockerfile build stage:

```dockerfile
RUN pwsh -Command "Get-ChildItem -Path wwwroot/ReportsRoot -Filter '*.flxr' -Recurse | ..."
```

## Manual Conversion (if needed)

### Convert to Docker paths (before building Docker image):

```powershell
cd src/MvcExplorer
.\update-reports-for-docker.ps1
```

### Revert back to local paths (for local development):

```powershell
cd src/MvcExplorer
.\update-reports-for-docker.ps1 -Revert
```

Or use the inline command:

```powershell
Get-ChildItem -Path "wwwroot\ReportsRoot" -Filter "*.flxr" -Recurse | ForEach-Object {
    $content = Get-Content $_.FullName -Raw
    $content = $content -replace 'Data Source=|DataDirectory|/C1NWind\.db', 'Data Source=|DataDirectory|\C1NWind.db'
    Set-Content -Path $_.FullName -Value $content -NoNewline
}
```

## Workflow

1. **Development**: Work with files using `|DataDirectory|` token
2. **Docker Build**: Run `docker build` - Dockerfile automatically converts paths
3. **Git**: Commit files with `|DataDirectory|` token (not absolute paths)

## Important Notes

- Always commit .flxr files with `|DataDirectory|` token (for local development)
- Never manually commit files with absolute Docker paths
- The Dockerfile handles path conversion automatically during build
- The Startup.cs detects Docker environment and configures appropriately
