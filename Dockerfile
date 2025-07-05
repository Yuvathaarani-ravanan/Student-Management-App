# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy .csproj and restore
COPY StudentManagementApp.csproj ./
RUN dotnet restore

# Copy all files and build
COPY . ./
RUN dotnet publish StudentManagementApp.csproj -c Release -o out

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out ./
ENTRYPOINT ["dotnet", "StudentManagementApp.dll"]
