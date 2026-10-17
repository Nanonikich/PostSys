@echo off
setlocal
set DOTNET_CLI_UI_LANGUAGE=en
cd /d "%~dp0.."

echo Building Android GUI (requires Android SDK + workload)...
dotnet build "src\postSys.gui.Android.prj\postSys.gui.Android.csproj" -c Release -f net10.0-android
if errorlevel 1 exit /b %ERRORLEVEL%

echo.
echo To deploy to a running emulator/device:
echo   dotnet build src\postSys.gui.Android.prj\postSys.gui.Android.csproj -t:Run -f net10.0-android
exit /b 0
