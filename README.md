# .NET WebApi

## Description

ASPNET WebAPI to fetch secrets from Azure Key Vault.

## Configuration

Replace placeholders accordingly:

- `{KEY VAULT NAME}`: name of Key Vault Resource
- `{MANAGED_IDENTITY_CLIENT_ID}`: user-assigned Managed Identity client ID
- `{REGISTRY_PATH}`: registry URL
- `{SECRET NAME}`: name of secret to retrieve

Configure `nodeSelector` and `tolerations` in the file _overlays/dev/patch-deployment.yaml_

## Docker

Build Docker Container Image:

```
docker build --no-cache --progress=plain --tag local/dotnet-aks-kv:1.0.0 .
```

Upload Container Image to a Container Registry:

```
docker tag local/dotnet-aks-kv:1.0.0 {REGISTRY_PATH}/dotnet-aks-kv:1.0.0

docker push {REGISTRY_PATH}/dotnet-aks-kv:1.0.0
```

## Kustomize

Build project and validate generated manifest file:

```
kustomize build overlays/dev | tee development.yaml
```

Example of generated manifest:
[example.yaml](.assets/example.yaml)

## Deploy

Use `kubectl` to deploy generated manifest file.
