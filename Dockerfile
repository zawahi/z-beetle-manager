# Stage 1: Build Frontend
FROM node:20-alpine AS frontend-build
WORKDIR /src/Frontend
COPY Frontend/package*.json ./
RUN npm install
COPY Frontend/ ./
RUN npm run build

# Stage 2: Build Backend
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS backend-build
WORKDIR /src/Backend
COPY Backend/*.csproj ./
RUN dotnet restore
COPY Backend/ ./
# Vueのビルド結果をバックエンドのwwwrootに配置
COPY --from=frontend-build /src/Frontend/dist ./wwwroot
RUN dotnet publish -c Release -o /app/publish

# Stage 3: Final Runtime
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=backend-build /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "KuwagataApp.Backend.dll"]
