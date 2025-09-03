#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 7075

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Slovko.NL.Api/Slovko.NL.Api.csproj", "Slovko.NL.Api/"]
RUN dotnet restore "Slovko.NL.Api/Slovko.NL.Api.csproj"
COPY . .
WORKDIR "/src/Slovko.NL.Api"
RUN dotnet build "Slovko.NL.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Slovko.NL.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY ["Slovko.NL.Api/Orfo.db", "/app"] 
ENTRYPOINT ["dotnet", "Slovko.NL.Api.dll"]

