@set DOTNET_CLI_UI_LANGUAGE=en
@echo Building PostSys...
cd src
@dotnet build --configuration Release PostSys.sln
@pause