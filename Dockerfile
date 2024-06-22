FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080:8080

RUN dotnet --info

# ENV ASPNETCORE_URLS=http://+:8080

FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["Dierentuin/Dierentuin/Dierentuin.csproj", "Dierentuin/Dierentuin/"]
COPY . .
WORKDIR "/src/Dierentuin/Dierentuin"
RUN dotnet build "Dierentuin.csproj" -c $configuration -o /app/build

FROM --platform=$BUILDPLATFORM build AS publish
ARG configuration=Release
RUN dotnet publish "Dierentuin.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM --platform=$BUILDPLATFORM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Dierentuin.dll"]