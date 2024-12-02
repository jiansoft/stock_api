#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
RUN ln -snf /usr/share/zoneinfo/Asia/Taipei /etc/localtime && echo Asia/Taipei > /etc/timezone
USER app
WORKDIR /app
EXPOSE 7000
EXPOSE 7001

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["StockApi.csproj", "."]
RUN dotnet restore "./StockApi.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "./StockApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./StockApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=ture

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

VOLUME ["/app/logs"]

ENTRYPOINT ["dotnet", "StockApi.dll"]