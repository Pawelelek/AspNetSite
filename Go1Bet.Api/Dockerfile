#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Go1Bet.Api/Go1Bet.Api.csproj", "Go1Bet.Api/"]
COPY ["Go1Bet.Infrastructure/Go1Bet.Core.csproj", "Go1Bet.Infrastructure/"]
COPY ["Go1Bet.Core/Go1Bet.Core.csproj", "Go1Bet.Core/"]
RUN dotnet restore "./Go1Bet.Api/Go1Bet.Api.csproj"
COPY . .
WORKDIR "/src/Go1Bet.Api"
RUN dotnet build "Go1Bet.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Go1Bet.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Go1Bet.Api.dll"]