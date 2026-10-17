# Репозиторий проекта PostSys

## Архитектурные паттерны и подходы

### Domain-Driven Design (DDD)

<details>
<summary>Используемые технологии и библиотеки</summary>

### База данных

- Entity Framework Core 10.0.13
- Health Checks 9.0.0

### Тестирование

- Microsoft.AspNetCore.Mvc.Testing 10.0.13
- Moq 4.21.0
- NUnit 5.0.0
- Respawn 7.0.0
- Snapshooter 1.3.1
- Testcontainers 4.16.0

### Архитектура и паттерны

- HotChocolate 16.6.8
- MediatR 14.2.0

### Инфраструктура

- Microsoft.Extensions.* 10.0.13
- NLog 6.2.2

### Дополнительные библиотеки

- CSharpFunctionalExtensions 3.7.0

### Библиотеки приложения

- Avalonia 12.1.4
- ReactiveUI.Avalonia 12.1.6
- Splat.Microsoft.Extensions.DependencyInjection 21.0.0

### Клиент

- GraphQL.Client 6.1.0
- Microsoft.AspNetCore.SignalR.Client 10.0.13

</details>

<details>
<summary>Конфигурации</summary>

### Конфигурации сервиса и приложения

- Для сервиса: `./src/postSys.service.prj/appsettings.json`
- Для приложения: `./src/postSys.gui.prj/appsettings.json`

</details>

<details>
<summary>База данных</summary>

### Настройка базы данных

1. Скачать и установить PostgreSQL.
2. Задать строку подключения:
  - Для запуска сервиса:
    - Файл: `./src/postSys.service.prj/appsettings.json`.
  - Для интеграционных тестов:
    - Файл: `./src/tests/postSys.integrationTests.prj/appsettings.json`.
3. Установить .NET SDK, если он не установлен (можно проверить в cmd командой `dotnet --version`).
4. Применить миграции, запустив скрипт "run_migrations.cmd".

</details>

<details>
<summary>Сервис</summary>

### Как запустить сервис

Если нет MS Visual Studio 2022:

1. Убедиться, что установлен пакет SDK для .NET 10.0.
2. Клонировать репозиторий PostSys.
3. Запустить "build.cmd".
4. Запустить exe-файл `postSys.service`, расположенный по пути `./src/postSys.service.prj/bin/Release/`.
5. Перейти по адресу:
   - При полноценном запуске: "http://localhost:5001/graphql/".
   - При отладке: "http://localhost:5000/graphql/".

Если установлен MSVS2022, также можно открыть файл решения PostSys.sln и собрать его.

### Логи сервиса

Логи сервиса располагаются по пути: `C:\\ProgramData\PostSys\postSys.service\Logs`.

### Скриншоты сервиса

![alt text](screenshots/service/queries.png 'Запросы')
![alt text](screenshots/service/mutations.png 'Мутации')
![alt text](screenshots/service/schema.png 'Схема данных')

</details>

<details>
<summary>Docker</summary>

### Как поднять базу данных и сервис в Docker

1. Убедиться, что установлен Docker Desktop.
2. Настроить переменные postgres для подключения к базе данных у сервиса "backend".
3. Перейти через cmd в папку src и выполнить команду `docker-compose up --build`.
4. После успешного запуска базы данных и сервиса через команду, выполнить миграции через запуск файла "run_migrations.cmd".
5. Убедиться, что сервис запущен по его адресу, отображаемому в Docker Desktop (добавить в url `/graphql`).

</details>

<details>
<summary>Приложение</summary>

### Как запустить приложение

1. Запустить сервис по инструкции.
2. Запустить exe-файл `postSys.gui`, расположенный по пути `./src/postSys.gui.prj/bin/Release/`.

### Логи приложения

Логи приложения располагаются по пути: `C:\\ProgramData\PostSys\postSys.gui\Logs`.

### Скриншоты приложения

![alt text](screenshots/gui/authorization.png 'Авторизация')
![alt text](screenshots/gui/orders.png 'Заказы')

</details>
