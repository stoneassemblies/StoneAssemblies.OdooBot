ARG PACKAGE_VERSION=${PACKAGE_VERSION:-1.0.0}
ARG DOTNET_SDK_VERSION=${DOTNET_SDK_VERSION:-8.0}
ARG DOTNET_ASP_NETCORE_RUNTIME_VERSION=${DOTNET_ASP_NETCORE_RUNTIME_VERSION:-8.0}
ARG DOCKER_REPOSITORY_PROXY=${DOCKER_REPOSITORY_PROXY:-mcr.microsoft.com}

FROM mcr.microsoft.com/dotnet/sdk:$DOTNET_SDK_VERSION AS build

ARG NUGET_REPOSITORY_PROXY=${NUGET_REPOSITORY_PROXY:-https://api.nuget.org/v3/index.json}

WORKDIR /build/src

# RUN dotnet new tool-manifest
COPY .config/dotnet-tools.json .config/dotnet-tools.json
RUN dotnet tool restore

ADD dotnet.csproj.tar.gz .
RUN dotnet restore --source $NUGET_REPOSITORY_PROXY

COPY src/. .
WORKDIR /build/src/StoneAssemblies.OdooBot
RUN dotnet publish -c Release -o /build/output/Release/StoneAssemblies.OdooBot /p:ServerGarbageCollection=false

WORKDIR /build

FROM mcr.microsoft.com/dotnet/aspnet:$DOTNET_ASP_NETCORE_RUNTIME_VERSION
ENV ASPNETCORE_URLS=http://0.0.0.0:80
WORKDIR /app

COPY --from=build /build/output/Release/StoneAssemblies.OdooBot .

# RUN rm appsettings.json

ENTRYPOINT ["dotnet", "StoneAssemblies.OdooBot.dll"]