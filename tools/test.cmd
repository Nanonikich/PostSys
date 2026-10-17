@echo off
setlocal
set DOTNET_CLI_UI_LANGUAGE=en
cd /d "%~dp0..\src"
dotnet test PostSys.sln -c Release --no-restore
if errorlevel 1 (
  dotnet test PostSys.sln -c Release
)
exit /b %ERRORLEVEL%
