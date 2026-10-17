@echo off
setlocal
cd /d "%~dp0..\src"

if not exist .env (
  echo Creating .env from .env.example — edit passwords before production use.
  copy /Y .env.example .env >nul
)

echo Starting PostSys Docker stack with Prometheus and Grafana...
docker compose --profile observability up --build -d
if errorlevel 1 exit /b %ERRORLEVEL%

echo.
echo GraphQL:    http://localhost:%BACKEND_PORT%/graphql
echo Readiness:  http://localhost:%BACKEND_PORT%/readiness
echo Liveness:   http://localhost:%BACKEND_PORT%/liveness
echo Metrics:    http://localhost:%BACKEND_PORT%/metrics
echo Prometheus: http://localhost:%PROMETHEUS_PORT%/
echo Grafana:    http://localhost:%GRAFANA_PORT%/  (admin / see GRAFANA_ADMIN_PASSWORD)
echo (defaults: BACKEND=5000, PROMETHEUS=9090, GRAFANA=3000)
exit /b 0
