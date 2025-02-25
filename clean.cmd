@echo off

echo Clean...
dotnet clean src -c Release %*
if %ERRORLEVEL% neq 0 goto error

echo Remove packages...
rmdir /Q /S src\packages

echo Remove output...
rmdir /Q /S output\Release
rmdir /Q /S output\Debug

echo Remove bin_obj...
for /R /D %%i in (*) do @(
	cd %%i
	if exist bin @rmdir /Q /S bin
	if exist obj @rmdir /Q /S obj		
)

pause
exit /b 0
:error
pause
exit /b 1