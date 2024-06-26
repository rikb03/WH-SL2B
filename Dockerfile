FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["Dierentuin/Dierentuin/Dierentuin.csproj", "Dierentuin/Dierentuin/"]
COPY . .

WORKDIR "/src/Dierentuin/Dierentuin"

RUN dotnet build "./Dierentuin.csproj" -c $configuration -o /app/build


FROM build AS publish
ARG configuration=Release
RUN dotnet publish "./Dierentuin.csproj" -c $configuration -o /app/publish /p:UseAppHost=false


FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Copy the database file to the container (please dont do this in production😖)
COPY ["Dierentuin/Dierentuin/Dierentuin12.db", "/app"]

ENV ASPNETCORE_ENVIRONMENT=development
ENTRYPOINT ["dotnet", "Dierentuin.dll"]