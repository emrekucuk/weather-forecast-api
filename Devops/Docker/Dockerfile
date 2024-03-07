FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build 

# This prevents pipeline from: Nuget Tool Internet Access Error
RUN apt-get update && apt-get install -y libcurl4

# Copy everything else and build
WORKDIR /src
Add . /src
RUN ls -la
RUN dotnet restore "/src/weather-forecast-api.sln"
RUN dotnet build "/src/weather-forecast-api.sln"

# Publish
WORKDIR /src
RUN dotnet publish -c release -o /Output --no-restore

# Final stage / image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /App
COPY --from=build /Output .

EXPOSE 80
ENTRYPOINT ["dotnet", "weather-forecast-api.dll"]