FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["WebScrapper/WebScrapper.csproj", "WebScrapper/"]
RUN dotnet restore "WebScrapper/WebScrapper.csproj"
COPY . .
WORKDIR "/src/WebScrapper"
RUN dotnet build "WebScrapper.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebScrapper.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "WebScrapper.dll"]
