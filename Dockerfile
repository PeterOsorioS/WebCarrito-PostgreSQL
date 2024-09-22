# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy and restore dependencies
COPY WebCarrito.sln ./
COPY Datos/Datos.csproj Datos/
COPY Entidad/Entidad.csproj Entidad/
COPY Servicio/Servicio.csproj Servicio/
COPY ShopMaster/ShopMaster.csproj ShopMaster/
RUN dotnet restore

# Copy the remaining files and build the application
COPY . .
RUN dotnet publish -c Release -o /app/publish

# Final Stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Copy the built application from the build stage
COPY --from=build /app/publish .

# Expose the port for the application (HTTP and optionally HTTPS)
EXPOSE 8080

# Set environment variables if needed (e.g. ASPNETCORE_ENVIRONMENT)
ENV ASPNETCORE_URLS=http://+:8080

# Set the entry point to run the application
ENTRYPOINT ["dotnet", "ShopMaster.dll"]
