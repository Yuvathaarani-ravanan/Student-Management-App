# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy everything and restore dependencies
COPY . . 
RUN dotnet restore

# Publish the app
RUN dotnet publish "StudentManagementApp.csproj" -c Release -o out

# Stage 2: Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out . 

# Use environment PORT variable required by Render
ENV ASPNETCORE_URLS=http://+:$PORT

# Expose the port (optional, Render will still bind automatically)
EXPOSE 80

ENTRYPOINT ["dotnet", "StudentManagementApp.dll"]
