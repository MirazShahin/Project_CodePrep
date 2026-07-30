# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy solution file and ALL project files referenced in the solution
COPY ["DevPrep.sln", "./"]
COPY ["CodePrep.API/CodePrep.API.csproj", "CodePrep.API/"]
COPY ["CodePrep.Application/CodePrep.Application.csproj", "CodePrep.Application/"]
COPY ["CodePrep.Domain/CodePrep.Domain.csproj", "CodePrep.Domain/"]
COPY ["CodePrep.Infrastructure/CodePrep.Infrastructure.csproj", "CodePrep.Infrastructure/"]
COPY ["CodePrepBlazor.web/CodePrepBlazor.csproj", "CodePrepBlazor.web/"]

# Restore everything using the solution
RUN dotnet restore "DevPrep.sln"

# Copy everything else and publish
COPY . .
WORKDIR "/src/CodePrep.API"
RUN dotnet publish -c Release -o /app/publish

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "CodePrep.API.dll"]
