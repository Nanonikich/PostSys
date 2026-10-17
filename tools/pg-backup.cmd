@echo off
setlocal EnableExtensions
cd /d "%~dp0.."

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

set "BACKUP_DIR=%~dp0..\backups"
if not exist "%BACKUP_DIR%" mkdir "%BACKUP_DIR%"

for /f %%I in ('powershell -NoProfile -Command "Get-Date -Format yyyyMMdd_HHmmss"') do set "STAMP=%%I"
set "TMP_IN_CONTAINER=/tmp/postsys_backup_%STAMP%.sql"
set "OUT_SQL=%BACKUP_DIR%\postsys_%POSTGRES_DB%_%STAMP%.sql"

echo Создание резервной копии PostgreSQL ^(контейнер postsys-db^)...
docker exec -e PGPASSWORD=%POSTGRES_PASSWORD% postsys-db pg_dump -U %POSTGRES_USER% -d %POSTGRES_DB% --format=plain --no-owner --no-acl -f %TMP_IN_CONTAINER%
if errorlevel 1 (
  echo Ошибка pg_dump. Убедитесь, что контейнер postsys-db запущен.
  exit /b 1
)

docker cp postsys-db:%TMP_IN_CONTAINER% "%OUT_SQL%"
if errorlevel 1 (
  echo Ошибка копирования файла резервной копии.
  exit /b 1
)
docker exec postsys-db rm -f %TMP_IN_CONTAINER% >nul 2>&1

echo Готово: %OUT_SQL%
exit /b 0
