@echo off
setlocal
set DOTNET_CLI_UI_LANGUAGE=en
cd /d "%~dp0..\src"
echo Building PostSys.sln (Release)...
dotnet build PostSys.sln -c Release
exit /b %ERRORLEVEL%
