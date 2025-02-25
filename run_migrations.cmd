@echo off
REM Переход в каталог миграций
cd /d "%~dp0src"

REM Выполнение команды миграции
"C:\Program Files\dotnet\dotnet.exe" ef database update --project "postSys.dal.prj\postSys.dal.csproj" --startup-project "postSys.service.prj\postSys.service.csproj" --context PostSys.Dal.Persistence.WriteDbContext --configuration Debug 20250107125543_Initial

REM Проверка на успешное выполнение команды
IF %ERRORLEVEL% NEQ 0 (
    echo Миграции завершены с ошибками.
    @pause
) ELSE (
    echo Миграции успешно применены.
    @pause
)