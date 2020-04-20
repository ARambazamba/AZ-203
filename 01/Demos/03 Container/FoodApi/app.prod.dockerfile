FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /build
COPY . .
RUN dotnet restore "FoodApi.csproj"
RUN dotnet publish -c Release -o /app

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS final
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "FoodApi.dll"]

# Build Image
# docker build --rm -f "app.prod.dockerfile" -t foodapi .

# Publish Image to dockerhub
# docker tag foodapi arambazamba/foodapi
# docker push arambazamba/foodapi

# Run Image
# docker run foodapi -d --rm -it -p 8080:5000

# Browse using: http://localhost:8080