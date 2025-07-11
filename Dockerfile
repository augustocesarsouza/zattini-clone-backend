# Usar uma imagem do .NET SDK para compilar a aplicação
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiar os arquivos do projeto
COPY . ./
RUN dotnet restore

# Publicar o projeto principal (Marisa.Api)
RUN dotnet publish Zattini.Api/Zattini.Api.csproj -c Release -o /out

# Usar uma imagem mais leve do .NET Runtime para rodar a aplicação
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Copiar os arquivos publicados do estágio anterior
COPY --from=build /out .

# Definir a porta que será exposta
EXPOSE 80

# Comando para rodar a aplicação
CMD ["dotnet", "Zattini.Api.dll"]