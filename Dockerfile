FROM mcr.microsoft.com/dotnet/sdk:8.0.403-alpine3.20 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "src/Application/Application.csproj" --verbosity normal
RUN dotnet publish "src/Application/Application.csproj" \
  --configuration Release \
  --no-restore \
  --output /app/publish \
  --verbosity normal \
  -p:AssemblyName=Application

FROM mcr.microsoft.com/dotnet/aspnet:8.0.10-alpine3.20
RUN apk --no-cache add curl jq
WORKDIR /home/app
COPY --from=build --chown=app:app /app/publish .
USER app
ENTRYPOINT ["dotnet", "Application.dll"]
