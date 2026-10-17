@echo off
call "%~dp0tools\run-migrations.cmd"
if errorlevel 1 (
  echo Migrations failed.
  exit /b %ERRORLEVEL%
)
echo Migrations applied successfully.
