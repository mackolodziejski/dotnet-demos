FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/TripsDemo.Api/TripsDemo.Api.csproj", "src/TripsDemo.Api/"]
COPY ["src/TripsDemo.Core/TripsDemo.Core.csproj", "src/TripsDemo.Core/"]
RUN dotnet restore "./src/TripsDemo.Api/TripsDemo.Api.csproj"
COPY . .
WORKDIR "/src/src/TripsDemo.Api"
RUN dotnet build "./TripsDemo.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./TripsDemo.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TripsDemo.Api.dll"]