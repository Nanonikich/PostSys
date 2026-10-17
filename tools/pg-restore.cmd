@echo off
setlocal EnableExtensions
cd /d "%~dp0.."

if "%~1"=="" (
  echo Использование: tools\pg-restore.cmd путь\к\файлу.sql
  exit /b 1
)

if not exist "%~1" (
  echo Файл не найден: %~1
  exit /b 1
)

if not exist "src\.env" (
  echo Файл src\.env не найден. Скопируйте src\.env.example в src\.env.
  exit /b 1
)

for /f "usebackq tokens=1,* delims==" %%A in (`findstr /R /V "^#" "src\.env" ^| findstr /R /V "^$"`) do (
  if not "%%A"=="" set "%%A=%%B"
)

if "%POSTGRES_DB%"=="" set "POSTGRES_DB=postgres"
if "%POSTGRES_USER%"=="" set "POSTGRES_USER=postgres"
if "%POSTGRES_PASSWORD%"=="" (
  echo POSTGRES_PASSWORD не задан в src\.env
  exit /b 1
)

set "TMP_IN_CONTAINER=/tmp/postsys_restore.sql"
echo ВНИМАНИЕ: восстановление перезапишет объекты в базе %POSTGRES_DB%.
echo Файл: %~1
docker cp "%~1" postsys-db:%TMP_IN_CONTAINER%
if errorlevel 1 exit /b 1

docker exec -e PGPASSWORD=%POSTGRES_PASSWORD% postsys-db psql -U %POSTGRES_USER% -d %POSTGRES_DB% -v ON_ERROR_STOP=1 -f %TMP_IN_CONTAINER%
set "ERR=%ERRORLEVEL%"
docker exec postsys-db rm -f %TMP_IN_CONTAINER% >nul 2>&1
exit /b %ERR%
