@echo off
setlocal
cd /d "%~dp0..\src"
docker compose down
exit /b %ERRORLEVEL%
