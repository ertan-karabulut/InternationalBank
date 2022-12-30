#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["WebBlazor/WebBlazor.csproj", "WebBlazor/"]
RUN dotnet restore "WebBlazor/WebBlazor.csproj"
COPY . .
WORKDIR "/src/WebBlazor"
RUN dotnet build "WebBlazor.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebBlazor.csproj" -c Release -o /app/publish


FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html
COPY --from=publish /app/publish/wwwroot .