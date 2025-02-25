# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Directory.Packages.props", "."]
COPY ["Directory.Build.props", "."]
COPY ["postSys.service.prj/postSys.service.csproj", "postSys.service.prj/"]
COPY ["common/postSys.framework.common.prj/postSys.framework.common.csproj", "common/postSys.framework.common.prj/"]
COPY ["common/postSys.service.common.prj/postSys.service.common.csproj", "common/postSys.service.common.prj/"]
COPY ["postSys.dal.prj/postSys.dal.csproj", "postSys.dal.prj/"]
COPY ["common/postSys.dal.common.prj/postSys.dal.common.csproj", "common/postSys.dal.common.prj/"]
COPY ["postSys.domain.prj/postSys.domain.csproj", "postSys.domain.prj/"]
COPY ["common/postSys.common.prj/postSys.common.csproj", "common/postSys.common.prj/"]
COPY ["postSys.readModels.prj/postSys.readModels.csproj", "postSys.readModels.prj/"]
RUN dotnet restore "./postSys.service.prj/postSys.service.csproj"
COPY . .
WORKDIR "/src/postSys.service.prj"
RUN dotnet build "./postSys.service.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./postSys.service.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "postSys.service.dll"]