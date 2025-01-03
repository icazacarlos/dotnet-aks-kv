FROM mcr.microsoft.com/dotnet/sdk:8.0.404-alpine3.20@sha256:4f1ce95847c5b28f957eba8333ed4a0df87c2899c51937e2c11db22be5b46bce AS build
WORKDIR /src
COPY src ./
RUN dotnet restore "Application/Application.csproj" --verbosity normal
RUN dotnet publish "Application/Application.csproj" \
  --configuration Release \
  --no-restore \
  --no-self-contained \
  --output /app/publish \
  --runtime linux-musl-x64 \
  --verbosity normal
USER app  

FROM mcr.microsoft.com/dotnet/aspnet:8.0.11-alpine3.20@sha256:a0062262fd674573be2314890006acadfd233fd7fdf89b3a5f44694b7632b29e
ENV \
  DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false \
  LC_ALL=en_US.UTF-8 \
  LANG=en_US.UTF-8
RUN apk add --no-cache \
  icu-data-full \
  icu-libs
WORKDIR /home/app
COPY --from=build --chown=app:app /app/publish .
USER app
ENTRYPOINT ["dotnet", "Application.dll"]
