# Етап базового образу
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Етап збірки
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Копіюємо скрипт wait-for-it.sh на етапі збірки
COPY ./scripts/waitforit/wait-for-it.sh /app/wait-for-it.sh
RUN chmod +x /app/wait-for-it.sh

# Виконуємо збірку проєкту
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Go1Bet.Api/Go1Bet.Api.csproj", "Go1Bet.Api/"]
COPY ["Go1Bet.Infrastructure/Go1Bet.Infrastructure.csproj", "Go1Bet.Infrastructure/"]
COPY ["Go1Bet.Core/Go1Bet.Core.csproj", "Go1Bet.Core/"]
RUN dotnet restore "./Go1Bet.Api/Go1Bet.Api.csproj"
COPY . .
WORKDIR "/src/Go1Bet.Api"
RUN dotnet build "Go1Bet.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Етап публікації
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Go1Bet.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Фінальний етап
FROM base AS final
WORKDIR /app

# Копіюємо публікацію з попереднього етапу
COPY --from=publish /app/publish .

# Копіюємо wait-for-it.sh у фінальний образ
COPY --from=build /app/wait-for-it.sh /app/wait-for-it.sh

# Використовуємо скрипт wait-for-it.sh для перевірки готовності PostgreSQL
ENTRYPOINT ["/app/wait-for-it.sh", "postgres_db:5432", "--", "dotnet", "Go1Bet.Api.dll"]