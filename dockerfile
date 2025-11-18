# ===========================
# Build Stage
# ===========================
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copy project files
COPY *.csproj ./
RUN dotnet restore

# Copy all source code
COPY . ./
RUN dotnet publish -c Release -o /app/publish

# ===========================
# Runtime Stage
# ===========================
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final
WORKDIR /app

# Copy published output
COPY --from=build /app/publish .

# Expose port
EXPOSE 8080

# Render uses PORT env variable, so map ASP.NET Core to it
ENV ASPNETCORE_URLS=http://0.0.0.0:8080

# Run the application
ENTRYPOINT ["dotnet", "OnlineContactBook.dll"]
  
