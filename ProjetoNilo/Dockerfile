#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ProjetoNilo/ProjetoNilo.csproj", "ProjetoNilo/"]
RUN dotnet restore "ProjetoNilo/ProjetoNilo.csproj"
COPY . .
WORKDIR "/src/ProjetoNilo"
RUN dotnet build "ProjetoNilo.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ProjetoNilo.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProjetoNilo.dll"]