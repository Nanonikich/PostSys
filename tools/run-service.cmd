@echo off
setlocal
cd /d "%~dp0.."
dotnet run --project "src\postSys.service.prj\postSys.service.csproj" -c Debug
exit /b %ERRORLEVEL%
