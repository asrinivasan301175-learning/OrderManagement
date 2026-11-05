
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /src

COPY OrderManagement.slnx .

COPY OrderManagement.Domain/OrderManagement.Domain.csproj OrderManagement.Domain/
COPY OrderManagement.Application/OrderManagement.Application.csproj OrderManagement.Application/
COPY OrderManagement.Infrastructure/OrderManagement.Infrastructure.csproj OrderManagement.Infrastructure/
COPY OrderManagement.API/OrderManagement.API.csproj OrderManagement.API/

# Restore NuGet packages
RUN dotnet restore OrderManagement.slnx

# Copy remaining source code
COPY . .

# Publish API
RUN dotnet publish OrderManagement.API/OrderManagement.API.csproj \
    -c Release \
    -o /app/publish \
    --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final

WORKDIR /app

COPY --from=build /app/publish .

# Configure ASP.NET Core
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

EXPOSE 8080

ENTRYPOINT ["dotnet", "OrderManagement.API.dll"]
