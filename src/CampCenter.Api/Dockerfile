FROM mcr.microsoft.com/dotnet/sdk:10.0-alpine AS build

ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Najpierw same pliki projektów + ustawienia wspólne – lepsze cache'owanie warstwy restore.
COPY ["Directory.Build.props", "./"]
COPY ["src/CampCenter.Domain/CampCenter.Domain.csproj", "src/CampCenter.Domain/"]
COPY ["src/CampCenter.Application/CampCenter.Application.csproj", "src/CampCenter.Application/"]
COPY ["src/CampCenter.Infrastructure/CampCenter.Infrastructure.csproj", "src/CampCenter.Infrastructure/"]
COPY ["src/CampCenter.Api/CampCenter.Api.csproj", "src/CampCenter.Api/"]
RUN dotnet restore "src/CampCenter.Api/CampCenter.Api.csproj"

# Reszta źródeł i publikacja.
COPY src/ ./src/
WORKDIR /src/src/CampCenter.Api
RUN dotnet publish "CampCenter.Api.csproj" \
    -c $BUILD_CONFIGURATION \
    -o /app/publish \
    --no-restore \
    /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0-alpine

# curl – do healthchecka; icu/tzdata – globalizacja (polskie znaki, strefy czasowe);
# krb5-libs – biblioteka GSSAPI wymagana przez Npgsql (libgssapi_krb5.so.2).
RUN apk add --no-cache curl icu-libs tzdata krb5-libs
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
ENV ASPNETCORE_HTTP_PORTS=8080

HEALTHCHECK --interval=5s --timeout=3s --start-period=20s --retries=10 \
    CMD curl --fail http://localhost:8080/health || exit 1

EXPOSE 8080
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "CampCenter.Api.dll"]
