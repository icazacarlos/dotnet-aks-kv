# .NET WebApi

## Description

ASPNET WebAPI to fetch secrets from Azure Key Vault.

## Create Directory Layout
```
mkdir -p dotnet-aks-kv/src
```

## Create Project
```
cd dotnet-aks-kv

dotnet new sln --name Application

dotnet new webapi -o src/Application

dotnet sln add src/Application/Application.csproj
```

## Add Dependencies
```
dotnet add package Azure.Extensions.AspNetCore.Configuration.Secrets

dotnet add package Azure.Identity
```

## Restore Dependencies
```
dotnet restore Application.sln --verbosity normal
```

## Build & Publish
```
dotnet publish "src/Application/Application.csproj" \
   --configuration Release \
   --no-restore \
   --output ./dist \
   --verbosity normal
```

## Run Application

### Log in to Azure

```
az login
```

Ensure the developer has the `Key Vault Secrets User` role assigned to access the Key Vault resource.

### Configure Environment Variables

Replace placeholders accordingly before configuring environment variables:

```
export KEY_VAULT_URI=https://<KEY VAULT NAME>.vault.azure.net
export SECRET_NAME=<SECRET NAME>
```

### Run Application

```
dotnet dist/Application.dll
```

By default, the application listens on port `8080`, and the hosting environment is set to `Production`.

This behavior can be modified by changing these environment variables:

- `ASPNETCORE_HTTP_PORTS`
- `ASPNETCORE_ENVIRONMENT`

### Test Application

Execute a simple `curl` request

```
curl -s http://localhost:8080/getSecret
```

Example Output:

The response contains the secret name and its current value:

```
safe-to-delete: 2b8c9cb4-eab0-44c7-be81-d39bf82b7bb2
```

## Run application in Kubernetes

Refer to the instructions in the `k8s` branch.
