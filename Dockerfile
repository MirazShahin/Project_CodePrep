# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["DevPrep.sln", "./"]
COPY ["CodePrep.API/CodePrep.API.csproj", "CodePrep.API/"]
COPY ["CodePrep.Application/CodePrep.Application.csproj", "CodePrep.Application/"]
COPY ["CodePrep.Domain/CodePrep.Domain.csproj", "CodePrep.Domain/"]
COPY ["CodePrep.Infrastructure/CodePrep.Infrastructure.csproj", "CodePrep.Infrastructure/"]
COPY ["CodePrepBlazor.web/CodePrepBlazor.csproj", "CodePrepBlazor.web/"]

RUN dotnet restore "DevPrep.sln"

COPY . .
WORKDIR "/src/CodePrep.API"
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

# Override at runtime:
# -e ConnectionStrings__DefaultConnection="Host=...;Port=5432;Database=...;Username=...;Password=..."
ENTRYPOINT ["dotnet", "CodePrep.API.dll"]
