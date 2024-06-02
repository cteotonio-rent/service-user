#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8090

ENV ASPNETCORE_ENVIRONMENT=Development
ENV DOTNET_USE_POLLING_FILE_WATCHER=1
ENV ASPNETCORE_HTTP_PORTS=8090
ENV DOTNET_RUNNING_IN_CONTAINER=true
ENV DOTNET_VERSION=8.0.1
ENV ASPNET_VERSION=8.0.1

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["backend/rent.api/rent.api.csproj", "backend/rent.api/"]
COPY ["shared/rent.communication/rent.communication.csproj", "shared/rent.communication/"]
COPY ["backend/rent.application/rent.application.csproj", "backend/rent.application/"]
COPY ["shared/rent.exceptions/rent.exceptions.csproj", "shared/rent.exceptions/"]
COPY ["backend/rent.domain/rent.domain.csproj", "backend/rent.domain/"]
COPY ["backend/rent.infrastructure/rent.infrastructure.csproj", "backend/rent.infrastructure/"]
RUN dotnet restore "./backend/rent.api/rent.api.csproj"
COPY . .
WORKDIR "/src/backend/rent.api"
RUN dotnet build "./rent.api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./rent.api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "rent.api.dll"]