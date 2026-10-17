@echo off
setlocal
cd /d "%~dp0src"

echo Applying EF migrations (WriteDbContext)...
dotnet ef database update --project "postSys.dal.prj\postSys.dal.csproj" --startup-project "postSys.service.prj\postSys.service.csproj" --context PostSys.Dal.Persistence.WriteDbContext

if %ERRORLEVEL% NEQ 0 (
  echo Migrations failed.
  exit /b %ERRORLEVEL%
)

echo Migrations applied successfully.
endlocal
