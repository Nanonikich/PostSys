# Репозиторий проекта PostSys

Почтовая система: GraphQL-сервис + мобильный клиент почтальона (Avalonia **Android**).

## Документация

- [Схема сущностей](docs/PostSys-Schema-Entities.docx)
- [Архитектура DDD](docs/PostSys-Architecture-DDD.docx)

## Скрипты (`tools/`)

| Скрипт | Назначение |
|--------|------------|
| `tools/build.cmd` | Сборка solution (Release) |
| `tools/build-android.cmd` | Сборка Android APK |
| `tools/run-service.cmd` | Локальный запуск API |
| `tools/docker-up.cmd` / `docker-down.cmd` | Docker Compose |
| `tools/docker-up-observability.cmd` | Docker + Prometheus + Grafana |
| `tools/run-migrations.cmd` | Миграции EF |
| `tools/pg-backup.cmd` / `pg-restore.cmd` | Резервная копия / восстановление PostgreSQL |
| `tools/test.cmd` | Тесты |

Корневые `build.cmd` / `run_migrations.cmd` вызывают скрипты из `tools/`.

<details>
<summary>Сервис и Docker</summary>

```bat
cd src
copy .env.example .env
rem задайте POSTGRES_PASSWORD и JWT_SIGNING_KEY
docker compose up --build -d
rem метрики и дашборды:
tools\docker-up-observability.cmd
```

- GraphQL: `http://localhost:5000/graphql`
- Готовность: `http://localhost:5000/readiness` (также `/health`)
- Жизнеспособность: `http://localhost:5000/liveness`
- Метрики Prometheus: `http://localhost:5000/metrics`
- Grafana (профиль observability): `http://localhost:3000`
- Миграции при старте: `POSTSYS_RUN_MIGRATIONS=true` (для нескольких реплик — `false` + `tools\run-migrations.cmd`)
- Резервная копия БД: `tools\pg-backup.cmd` → каталог `backups/`
- Пример проб оркестратора: `deploy/k8s/backend-deployment.yaml`
- JWT обязателен для **mutations**; `authorizePostman` возвращает `{ postmanId, accessToken, expiresAt }`

</details>

<details>
<summary>Android на Windows</summary>

1. `dotnet workload install android` + Android SDK / эмулятор  
2. Backend (`tools\docker-up.cmd` или `tools\run-service.cmd`)  
3. Запуск:

```bat
tools\build-android.cmd
dotnet build src\postSys.gui.Android.prj\postSys.gui.Android.csproj -t:Run -f net10.0-android
```

Адрес сервиса с эмулятора по умолчанию: `http://10.0.2.2:5000`.  
Deep link настроек: `postsys://configure?service=http://10.0.2.2:5000`

</details>