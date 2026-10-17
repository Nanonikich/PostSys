@echo off
setlocal
cd /d "%~dp0..\src"

if not exist .env (
  echo Creating .env from .env.example — edit passwords before production use.
  copy /Y .env.example .env >nul
)

echo Starting PostSys Docker stack...
docker compose up --build -d
if errorlevel 1 exit /b %ERRORLEVEL%

echo.
echo GraphQL:   http://localhost:%BACKEND_PORT%/graphql
echo Readiness: http://localhost:%BACKEND_PORT%/readiness
echo Liveness:  http://localhost:%BACKEND_PORT%/liveness
echo Metrics:   http://localhost:%BACKEND_PORT%/metrics
echo (default BACKEND_PORT=5000; observability: tools\docker-up-observability.cmd)
exit /b 0
