@echo off
setlocal
cd /d "%~dp0..\src"
echo Applying EF migrations...
dotnet ef database update --project "postSys.dal.prj\postSys.dal.csproj" --startup-project "postSys.service.prj\postSys.service.csproj" --context PostSys.Dal.Persistence.WriteDbContext
exit /b %ERRORLEVEL%
