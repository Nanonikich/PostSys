# Репозиторий проекта PostSys

Почтовая система: GraphQL-сервис + мобильный клиент почтальона (Avalonia Android / iOS).

## Документация

- [Схема сущностей](docs/PostSys-Schema-Entities.docx)
- [Архитектура DDD](docs/PostSys-Architecture-DDD.docx)

<details>
<summary>Используемые технологии</summary>

- .NET 10, EF Core, PostgreSQL, HotChocolate, MediatR, SignalR
- Avalonia 12 + ReactiveUI (Android / iOS)
- Docker Compose для backend + БД
- NUnit, Testcontainers, Moq

</details>

<details>
<summary>База данных</summary>

1. Установить PostgreSQL **или** поднять через Docker (см. ниже).
2. Строка подключения: `./src/postSys.service.prj/appsettings.json` (локально) либо переменные `POSTGRES_*` в Docker.
3. Миграции:
   - при старте сервиса применяются автоматически;
   - вручную: `run_migrations.cmd` (нужен `dotnet ef`).

</details>

<details>
<summary>Сервис</summary>

### Локально

```bat
dotnet run --project src/postSys.service.prj
```

GraphQL: `http://localhost:5000/graphql` (порт зависит от launchSettings/appsettings).

### Docker

```bat
cd src
copy .env.example .env
docker compose up --build -d
```

- GraphQL: `http://localhost:5000/graphql`
- Health: `http://localhost:5000/health`
- Миграции при старте backend — отдельный `run_migrations.cmd` после Docker **не обязателен**.

Mobile GUI **не** входит в Compose: APK/IPA запускаются на эмуляторе/устройстве, а не в контейнере (это обычная практика и не избыточный «мобильный Docker»).

</details>

<details>
<summary>Мобильное приложение (проверка на Windows)</summary>

Desktop GUI удалён. На Windows проверяется **Android**.

### Подготовка

1. Установить [.NET 10 SDK](https://dotnet.microsoft.com/download) и workload:

```bat
dotnet workload install android
```

2. Установить [Android SDK](https://aka.ms/dotnet-android-install-sdk) (через Android Studio или cmdline-tools). Задать `ANDROID_HOME` / `AndroidSdkDirectory`, если SDK не в `%LOCALAPPDATA%\Android\Sdk`.

3. Создать и запустить AVD (эмулятор) в Android Studio → Device Manager.

4. Поднять backend (`docker compose up` или `dotnet run`), чтобы API был на хосте.

### Сборка и запуск

```bat
dotnet build src/postSys.gui.Android.prj/postSys.gui.Android.csproj -t:Run -f net10.0-android
```

или открыть `PostSys.sln` в Visual Studio / Rider и Run на Android Emulator.

### Адрес сервиса с эмулятора

По умолчанию GUI на Android использует `http://10.0.2.2:5000` (это loopback хоста из эмулятора).  
На физическом устройстве в Settings укажите IP ПК в локальной сети, например `http://192.168.x.x:5000`.

### iOS

Сборка и запуск только на **macOS** (Xcode + `dotnet workload install ios`). На Windows проект iOS в solution — stub, чтобы solution собирался.

</details>

<details>
<summary>Логи</summary>

- Сервис (Windows): `%ProgramData%\PostSys\...\Logs`
- Mobile: LocalApplicationData / sandbox приложения

</details>
