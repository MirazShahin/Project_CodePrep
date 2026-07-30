# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy solution file and project files (DevPrep.sln use korun)
COPY ["DevPrep.sln", "./"]
COPY ["CodePrep.API/CodePrep.API.csproj", "CodePrep.API/"]

RUN dotnet restore "CodePrep.API/CodePrep.API.csproj"

# Copy everything else and build
COPY . .
WORKDIR "/src/CodePrep.API"
RUN dotnet publish -c Release -o /app/publish

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "CodePrep.API.dll"]
