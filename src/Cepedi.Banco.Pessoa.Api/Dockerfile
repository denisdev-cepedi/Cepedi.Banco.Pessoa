#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Cepedi.Banco.Pessoa.Api/Cepedi.Banco.Pessoa.Api.csproj", "src/Cepedi.Banco.Pessoa.Api/"]
COPY ["src/Cepedi.Banco.Pessoa.IoC/Cepedi.Banco.Pessoa.IoC.csproj", "src/Cepedi.Banco.Pessoa.IoC/"]
COPY ["src/Cepedi.Banco.Pessoa.Dados/Cepedi.Banco.Pessoa.Dados.csproj", "src/Cepedi.Banco.Pessoa.Dados/"]
COPY ["src/Cepedi.Banco.Pessoa.Dominio/Cepedi.Banco.Pessoa.Dominio.csproj", "src/Cepedi.Banco.Pessoa.Dominio/"]
COPY ["src/Cepedi.Banco.Pessoa.Compartilhado/Cepedi.Banco.Pessoa.Compartilhado.csproj", "src/Cepedi.Banco.Pessoa.Compartilhado/"]
RUN dotnet restore "./src/Cepedi.Banco.Pessoa.Api/Cepedi.Banco.Pessoa.Api.csproj"
COPY . .
WORKDIR "/src/src/Cepedi.Banco.Pessoa.Api"
RUN dotnet build "./Cepedi.Banco.Pessoa.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Cepedi.Banco.Pessoa.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Cepedi.Banco.Pessoa.Api.dll"]