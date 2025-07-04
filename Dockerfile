FROM mcr.microsoft.com/dotnet/sdk:9.0-preview AS build
WORKDIR /app

COPY . ./
RUN dotnet restore
RUN dotnet publish "StudentManagementApp.csproj" -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:9.0-preview
WORKDIR /app
COPY --from=build /app/out .

# Important: Expose the port that matches what Kestrel listens to
EXPOSE 5000
ENTRYPOINT ["dotnet", "StudentManagementApp.dll"]
