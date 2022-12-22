#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Clients/MvcApp/MvcApp.ECommerce/MvcApp.ECommerce.csproj", "src/Clients/MvcApp/MvcApp.ECommerce/"]
RUN dotnet restore "src/Clients/MvcApp/MvcApp.ECommerce/MvcApp.ECommerce.csproj"
COPY . .
WORKDIR "/src/src/Clients/MvcApp/MvcApp.ECommerce"
RUN dotnet build "MvcApp.ECommerce.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MvcApp.ECommerce.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MvcApp.ECommerce.dll"]