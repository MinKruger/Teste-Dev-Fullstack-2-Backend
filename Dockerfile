# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copia o arquivo de solução e os arquivos dos projetos
COPY ["API/backend.sln", "./API/"]
COPY ["API/API.csproj", "./API/"]
COPY ["Infrastructure/Infrastructure.csproj", "./Infrastructure/"]
COPY ["CrossCutting/CrossCutting.csproj", "./CrossCutting/"]
COPY ["Domain/Domain.csproj", "./Domain/"]
COPY ["Application/Application.csproj", "./Application/"]

# Restaura as dependências especificando o caminho para a solução
RUN dotnet restore "./API/backend.sln"

# Copia todo o conteúdo do diretório do projeto para o container
COPY . .

# Define o diretório de trabalho para o projeto principal (API)
WORKDIR "/src/API"

# Compila o projeto
RUN dotnet build "./backend.sln" -c Release -o /app/build

FROM build AS publish
# Publica o projeto
RUN dotnet publish "./backend.sln" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app

# Copia os artefatos publicados para o container final
COPY --from=publish /app/publish .

# Define o ponto de entrada
ENTRYPOINT ["dotnet", "API.dll"]
