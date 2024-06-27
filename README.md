<h1 align="center">
  <a href="https://takejoboffer.ru/">
    <img src="/Frontend/Web/public/favicon-32x32.png" alt="TakeJobOffer" width="32" height="32"/>
    TakeJobOffer
  </a>
</h1>

[TakeJobOffer](https://takejoboffer.ru) � ��� ���-����, ������������ ��������� ��������� �� ���������� � �������, ������� ��� ��� ���������. ���� ���� - ������ ����������� ������ � ������������ ������ � ��� SEO-������������.

## ����������
- [����������](#����������)
- [���������](#���������)
- [������������](#������������)
- [������](#������)
- [����� ������](#�����-������)
- [�����������](#�����������)
- [���������](#���������)
- [��������](#��������)
- [�������������](#�������������)

## ����������
- Docker
- Docker Compose
- ��� ����� ��������� ������������ ������ ��� ������ - [hh-analyzer](https://github.com/KovalNikitok/hh-analyzer)

## ���������
1. ���������� �����������:
    ```sh
    git clone https://github.com/KovalNikitok/TakeJobOffer.git
    ```
2. ��������� � ����� �������:
    ```sh
    cd takejoboffer
    ```

## ������������
1. �������� ���� `.env` � ����� ������� � ���������� ��������� ���������� ��������� (����� ���������� ��� ������, �� ����� ���� ��������, � ��� ��������� ��������� ������ �� ��������������, ��������: ������ ��� api-������):
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

## ������
1. �������� � ��������� ������ � ������� Docker Compose:
    ```sh
    docker compose up --build 
    ```

## ����� ������
1. **Frontend-Web**: ���������� ��������� ��������� �� ���������� � �������. ������� � �������������� Node.js + JavaScript + TypeScript + React + Html + CSS + Antd, �� ����������� FSD (feature-sliced design).
2. **Backend**: ������������ ������� �� ���������, ��������������� � ������ ������, ����� � �������� ��������� ��� �������� � ��������� ������. ������� c ����������� ���������� .Net Core, C#, ASP.NET Web Api, Entity Framework, Fluent Result, xUnit �� ��������� Clean Architecture.

## �����������
- **Frontend-Web**: FSD (feature-sliced design)
- **Backend**: Clean Architecture

## ���������
- [������ ������](https://github.com/KovalNikitok)

## ��������
���� ������ ������������� �� �������� �������� MIT. ��������� ��. ���� [LICENSE](https://github.com/KovalNikitok/TakeJobOffer/blob/master/LICENSE.txt).

## �������������
������� ��������� ������������� ������������ �������, [������� �������](https://www.youtube.com/@goingtoit).