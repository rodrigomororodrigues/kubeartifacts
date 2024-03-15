# Use the specific version of .NET runtime to ensure reproducible builds
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base

# Create a non-root user for running the application securely
RUN groupadd -r myuser && useradd --no-log-init -r -g myuser myuser

WORKDIR /app
EXPOSE 80

# Setting up environment variables if needed, replace with your actual variables
# ENV VariableName=Value

# Use the specific version of .NET SDK
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src

# Optimizing layer caching by restoring dependencies before copying entire source code
COPY ["apimysqlrodrigomoro/apimysqlrodrigomoro.csproj", "apimysqlrodrigomoro/"]
RUN dotnet restore "apimysqlrodrigomoro/apimysqlrodrigomoro.csproj"

COPY . .
WORKDIR "/src/apimysqlrodrigomoro"
RUN dotnet build "apimysqlrodrigomoro.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "apimysqlrodrigomoro.csproj" -c Release -o /app/publish

FROM base AS final

# Ensure to switch to the non-root user
USER myuser

WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "apimysqlrodrigomoro.dll"]

# Including a health check if applicable (example for a web app)
HEALTHCHECK --interval=30s --timeout=3s --retries=3 CMD curl --silent --fail localhost:80/healthz || exit 1
