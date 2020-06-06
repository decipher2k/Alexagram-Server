#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Alexagram Server/Alexagram Server.csproj", "Alexagram Server/"]
RUN dotnet restore "Alexagram Server/Alexagram Server.csproj"
COPY . .
WORKDIR "/src/Alexagram Server"
RUN dotnet build "Alexagram Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Alexagram Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Alexagram Server.dll"]