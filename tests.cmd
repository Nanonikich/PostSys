@echo off
setlocal enabledelayedexpansion

rem Параметры для работы
set PROJECT_DIR=C:\Users\Acer\Desktop\PostSys\src\tests\postSys.unitTests.prj

rem Перейти в папку с проектами тестов
cd /d %PROJECT_DIR%

rem Цикл по всем проектам внутри папки
for %%f in (*.csproj) do (
    echo Запуск тестов для %%f...
    
    rem Выполнение тестов и вывод результата в консоль
    dotnet test "%%f" --configuration Debug --verbosity normal

    rem Проверка кода завершения тестов
    if !ERRORLEVEL! NEQ 0 (
        echo В процессе тестирования %%f произошли ошибки.
    ) else (
        echo Unit-тесты для %%f завершены успешно.
    )

    echo.
)

echo Завершение работы.
pause
exit /b