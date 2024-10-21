# dotnet webapi

## Description

ASPNET WebAPI to fetch secrets from Azure Key Vault without using Service Principal.

- Deploy environment = AKS Cluster.
- AKS Cluster is configured with Managed Identity with proper configuration to access Azure Key Vault.

## Create directory layout
```
mkdir -p dotnet-aks-kv/src
```

## Create project
```
cd dotnet-aks-kv

dotnet new sln --name Application

dotnet new webapi -o src/Application

dotnet sln add src/Application/Application.csproj
```

## Add dependencies
```
dotnet add package Azure.Extensions.AspNetCore.Configuration.Secrets

dotnet add package Azure.Identity
```

## Restore dependencies
```
dotnet restore Application.sln \
   --verbosity normal
```

## Build & publish project
```
dotnet publish "src/Application/Application.csproj" \
   --configuration Release \
   --no-restore \
   --verbosity normal \
   -p:AssemblyName=Application
```

## Configure environment
```
export ASPNETCORE_HTTP_PORTS=8080
export AZURE_CLIENT_ID=<Managed Identity Client ID>
export ASPNETCORE_ENVIRONMENT=Production
export KEY_VAULT_NAME=<Key Vault Name>
export KEY_VAULT_URI=https://<Key Vault Name>.vault.azure.net
```

## Clean up
```
rm -rf src/Application/obj; \
rm -rf src/Application/bin
```

## Run application
```
dotnet src/Application/bin/Release/net8.0/Application.dll
```

## Client Test
```
curl -s http://localhost:8080/weatherforecast | jq . -C
curl -s http://localhost:8080/clientId
curl -s http://localhost:8080/clientSecret
curl -s http://localhost:8080/clientCertificate
```

## Combo
```
rm -rf src/Application/obj; \
rm -rf src/Application/bin; \
dotnet restore Application.sln \
   --verbosity normal; \
dotnet publish "src/Application/Application.csproj" \
   --configuration Release \
   --no-restore \
   --verbosity normal \
   -p:AssemblyName=Application; \
dotnet src/Application/bin/Release/net8.0/Application.dll
```

## Docker
```
docker build --no-cache -t local/dotnet-aks-kv:latest .

docker run -p 8080:8080 --rm local/dotnet-aks-kv:latest
```
