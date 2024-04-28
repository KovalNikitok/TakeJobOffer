#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

RUN apt-get update \
    && apt-get install -y curl

USER app
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["TakeJobOffer.API/TakeJobOffer.API.csproj", "TakeJobOffer.API/"]
COPY ["TakeJobOffer.Application/TakeJobOffer.Application.csproj", "TakeJobOffer.Application/"]
COPY ["TakeJobOffer.Domain/TakeJobOffer.Domain.csproj", "TakeJobOffer.Domain/"]
COPY ["TakeJobOffer.DAL/TakeJobOffer.DAL.csproj", "TakeJobOffer.DAL/"]
RUN dotnet restore "./TakeJobOffer.API/TakeJobOffer.API.csproj"
COPY . .
WORKDIR "/src/TakeJobOffer.API"
RUN dotnet build "./TakeJobOffer.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./TakeJobOffer.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TakeJobOffer.API.dll"]