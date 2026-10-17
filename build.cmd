@echo off
call "%~dp0tools\build.cmd"
if errorlevel 1 exit /b %ERRORLEVEL%
pause
