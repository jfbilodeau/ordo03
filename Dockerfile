# Stage 1: Build stage - uses .NET SDK image to compile the application
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
# Set working directory inside container
WORKDIR /src
# Copy all files from host machine to container
COPY . .
# Build and publish the Ordo app in Release mode
RUN dotnet publish "Ordo/Ordo.csproj" -c Release -o /app/publish

# Stage 2: Runtime stage - uses lightweight .NET runtime image for production
FROM mcr.microsoft.com/dotnet/aspnet:10.0
# Set working directory for runtime container
WORKDIR /app
# Configure ASP.NET Core to listen on port 8080
ENV ASPNETCORE_URLS=http://+:8080
# Copy published artifacts from build stage
COPY --from=build /app/publish .
# Expose app port
EXPOSE 8080
# Define the entry point to run the .NET application when container starts
ENTRYPOINT ["dotnet", "Ordo.dll"]