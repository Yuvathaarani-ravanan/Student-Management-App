# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy everything first (important for dependencies like Services/Models)
COPY . ./

# Restore and publish
RUN dotnet restore
RUN dotnet publish StudentManagementApp.csproj -c Release -o out

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out ./
ENTRYPOINT ["dotnet", "StudentManagementApp.dll"]
