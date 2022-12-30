#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app/Upload
COPY InternationalBankApi/Upload/ .
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY BusinessLayer/*.csproj BusinessLayer/

COPY CoreLayer/*.csproj CoreLayer/

COPY DataAccessLayer/*.csproj DataAccessLayer/

COPY EntityLayer/*.csproj EntityLayer/

COPY InternationalBankApi/*.csproj InternationalBankApi/

RUN dotnet restore "InternationalBankApi/InternationalBankApi.csproj"
COPY . .
WORKDIR "/src/InternationalBankApi"
RUN dotnet build "InternationalBankApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "InternationalBankApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "InternationalBankApi.dll"]


