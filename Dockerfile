FROM mrc.mocrosoft.com/dotnet/aspnet:7.0 AS base

WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS base
COPY . .


# Install all dependencies
RUN dotnet restore "Walter.Web/Walter.Web.csproj"

# Install the dotnet-ef tools
RUN dotnet tool install -g dotnet-ef
ENV PATH $PATH:/root/.dotnet/tools

RUN dotnet-ef database update --startup-project "Walter.Web" --project "Walter.Infrastructure/Walter.Infrastructure.csproj"

RUN dotnet publish "Walter.Web/Walter.Web.csproj" -c Release -o /app/build
WORKDIR /app/build
EXPOSE 80

ENTRYPOINT ["dotnet", "Walter.Web.dll", "--urls=http://0.0.0.0:80"]
