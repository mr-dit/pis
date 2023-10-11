#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["pis-web-api/pis-web-api.csproj", "pis-web-api/"]
RUN dotnet restore "pis-web-api/pis-web-api.csproj"
COPY . .
WORKDIR "/src/pis-web-api"
RUN dotnet build "pis-web-api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "pis-web-api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "pis-web-api.dll"]
