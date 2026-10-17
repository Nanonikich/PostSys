# syntax=docker/dockerfile:1

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER root
RUN apt-get update \
	&& apt-get install -y --no-install-recommends curl \
	&& rm -rf /var/lib/apt/lists/*
USER $APP_UID
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Directory.Packages.props", "."]
COPY ["Directory.Build.props", "."]
COPY ["postSys.service.prj/postSys.service.csproj", "postSys.service.prj/"]
COPY ["common/postSys.framework.common.prj/postSys.framework.common.csproj", "common/postSys.framework.common.prj/"]
COPY ["postSys.application.prj/postSys.application.csproj", "postSys.application.prj/"]
COPY ["postSys.dal.prj/postSys.dal.csproj", "postSys.dal.prj/"]
COPY ["postSys.domain.prj/postSys.domain.csproj", "postSys.domain.prj/"]
COPY ["common/postSys.common.prj/postSys.common.csproj", "common/postSys.common.prj/"]
COPY ["postSys.readModels.prj/postSys.readModels.csproj", "postSys.readModels.prj/"]
RUN dotnet restore "./postSys.service.prj/postSys.service.csproj"
COPY . .
WORKDIR "/src/postSys.service.prj"
RUN dotnet build "./postSys.service.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./postSys.service.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
USER root
WORKDIR /app
ENV POSTSYS_DATA_DIR=/app/data
ENV ASPNETCORE_URLS=http://+:8080
RUN mkdir -p /app/data && chown -R "$APP_UID":"$APP_UID" /app/data
USER $APP_UID
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "postSys.service.dll"]