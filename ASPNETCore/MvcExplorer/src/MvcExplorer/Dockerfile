# Use the official .NET 10 SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /app

# Copy the project files and restore any dependencies
COPY MvcExplorer.csproj ./
COPY Nuget.config ./
COPY MvcExplorer.sa3licx ./
RUN dotnet restore

# Copy the remaining files and build the application
COPY . ./

# Update report connection strings for Docker (absolute paths instead of |DataDirectory|)
RUN pwsh -Command "Get-ChildItem -Path wwwroot/ReportsRoot -Filter '*.flxr' -Recurse | ForEach-Object { \
    \$content = Get-Content \$_.FullName -Raw; \
    \$content = \$content -replace 'Data Source=\\|DataDirectory\\|\\\\', 'Data Source=/app/wwwroot/App_Data/'; \
    Set-Content -Path \$_.FullName -Value \$content -NoNewline \
}"

# Manually copy license file to obj directory where build expects it
RUN mkdir -p obj/Release/net10.0 && cp MvcExplorer.sa3licx obj/Release/net10.0/.sa3licx

RUN dotnet publish MvcExplorer.csproj -c Release -o out

# Use the official .NET runtime image to run the application
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app

# Install required libraries including NTLM support
RUN apt-get update && apt-get install -y \
    libgdiplus \
    libc6-dev \
    libsqlite3-0 \
    libsqlite3-dev \
    libkrb5-3 \
    libgssapi-krb5-2 \
    krb5-user \
    libssl-dev \
    gss-ntlmssp \        
    && rm -rf /var/lib/apt/lists/*

COPY --from=build /app/out .

# Create necessary directories
RUN mkdir -p /app/wwwroot/Temp && chmod 777 /app/wwwroot/Temp
RUN mkdir -p /app/wwwroot/App_Data && chmod 777 /app/wwwroot/App_Data
RUN mkdir -p /app/wwwroot/Data && chmod 777 /app/wwwroot/Data
RUN mkdir -p /data/dataengine-workspace && chmod 777 /data/dataengine-workspace

ENV TMPDIR=/tmp
ENV TEMP=/tmp
ENV TMP=/tmp

RUN chmod 777 /tmp

# SSRS configuration
ENV AppSettings__SsrsUrl="http://ssrs.componentone.com:8000/ReportServer"
ENV AppSettings__SsrsUserName="ssrs_demo"
ENV AppSettings__SsrsPassword="bjqveS5gh89BH1Q"

ENV DOTNET_RUNNING_IN_CONTAINER="true"

# Configure NTLM / Negotiate
ENV KRB5_CONFIG=/etc/krb5.conf
ENV GSS_USE_PROXY=yes
ENV COMPlus_LegacyHttpClientFactory=1
ENV DOTNET_SYSTEM_NET_HTTP_USESOCKETSHTTPHANDLER=0

# Minimal Kerberos config (not used by NTLM, but ok)
RUN echo "[libdefaults]\n\
    default_realm = COMPONENTONE.COM\n\
    dns_lookup_realm = false\n\
    dns_lookup_kdc = false\n\
    ticket_lifetime = 24h\n\
    renew_lifetime = 7d\n\
    forwardable = true\n\
[realms]\n\
[domain_realm]" > /etc/krb5.conf

EXPOSE 8080

ENTRYPOINT ["dotnet", "MvcExplorer.dll"]
