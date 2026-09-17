# syntax=docker/dockerfile:1

FROM node:22-alpine AS client
RUN npm install --global pnpm@11.23.0
WORKDIR /client
COPY client/package.json client/pnpm-lock.yaml ./
RUN pnpm install --frozen-lockfile
COPY client/ ./
RUN pnpm build

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /source
COPY application/src/EmployeeManagementSystem.Api.csproj application/src/
RUN dotnet restore application/src/EmployeeManagementSystem.Api.csproj
COPY application/ application/
RUN dotnet publish application/src/EmployeeManagementSystem.Api.csproj \
    --configuration Release \
    --no-restore \
    --output /app/publish \
    /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish ./
COPY --from=client /client/dist ./wwwroot
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "EmployeeManagementSystem.Api.dll"]
