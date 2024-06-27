<h1 align="center">
  <a href="https://takejoboffer.ru/">
    <img src="/Frontend/Web/public/favicon-32x32.png" alt="TakeJobOffer" width="32" height="32"/>
    TakeJobOffer
  </a>
</h1>

[TakeJobOffer](https://takejoboffer.ru) — это веб-сайт, отображающий собранную аналитику по профессиям и навыкам, которые для них требуются. Наша цель - помочь соискателям работы с составлением резюме и его SEO-оптимизацией.

## Оглавление
- [Требования](#требования)
- [Установка](#установка)
- [Конфигурация](#конфигурация)
- [Запуск](#запуск)
- [Схема работы](#схема-работы)
- [Архитектура](#архитектура)
- [Авторство](#авторство)
- [Лицензия](#лицензия)
- [Благодарности](#благодарности)

## Требования
- Docker
- Docker Compose
- Для сбора аналитики используется другой наш проект - [hh-analyzer](https://github.com/KovalNikitok/hh-analyzer)

## Установка
1. Клонируйте репозиторий:
    ```sh
    git clone https://github.com/KovalNikitok/TakeJobOffer.git
    ```
2. Перейдите в папку проекта:
    ```sh
    cd takejoboffer
    ```

## Конфигурация
1. Создайте файл `.env` в корне проекта и определите следующие переменные окружения (часть переменных уже задана, но может быть заменена, а для некоторых требуется задать их самостоятельно, напрмиер: пароли или api-токены):
    ```dotenv
    POSTGRES_PASSWORD=
    POSTGRES_USER=
    POSTGRES_DB=takejobofferdb
    POSTGRES_PORT=5432
    REDIS_CACHE_PORT=6379
    REDIS_CACHE_DATABASES=1
    ASPNETCORE_ENVIRONMENT=Production
    ASPNETCORE_HTTPS_PORTS=8081
    ASPNETCORE_HTTP_PORTS=8080
    ASPNETCORE_ConnectionString_DB=User ID=postgres;Password=;Host=;Port=5432;Database=takejobofferdb;
    ASPNETCORE_Kestrel__Certificates__Default__Password=
    ASPNETCORE_Kestrel__Certificates__Default__Path=/home/app/.aspnet/https/takejoboffer.ru.pfx
    BACKEND_VOL_PATH=/home/app/.aspnet/https
    FRONTEND_PORT=3000
    HHANALYZER_HHApiSettings__ConnectionString=https://api.hh.ru
    HHANALYZER_HHApiSettings__AccessToken=
    HHANALYZER_HHApiSettings__Agent=
    HHANALYZER_TakeJobOfferApiSettings__ConnectionString=
    BASE_DOMAIN=
    CERTBOT_EMAIL=
    ```

## Запуск
1. Соберите и запустите проект с помощью Docker Compose:
    ```sh
    docker compose up --build 
    ```

## Схема работы
1. **Frontend-Web**: Отображает собранную аналитику по профессиям и навыкам. Написан с использованием Node.js + JavaScript + TypeScript + React + Html + CSS + Antd, по архитектуре FSD (feature-sliced design).
2. **Backend**: Обрабатывает запросы от фронтенда, взаимодействует с базами данных, кэшем и сервисом аналитики для парсинга и обработки данных. Написан c применением технологий .Net Core, C#, ASP.NET Web Api, Entity Framework, Fluent Result, xUnit по принципам Clean Architecture.

## Архитектура
- **Frontend-Web**: FSD (feature-sliced design)
- **Backend**: Clean Architecture

## Авторство
- [Коваль Никита](https://github.com/KovalNikitok)

## Лицензия
Этот проект лицензируется на условиях лицензии MIT. Подробнее см. файл [LICENSE](https://github.com/KovalNikitok/TakeJobOffer/blob/master/LICENSE.txt).

## Благодарности
Выражаю отдельную благодарность вдохновителю проекта, [Алексею Кивайко](https://www.youtube.com/@goingtoit).