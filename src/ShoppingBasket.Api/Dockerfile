#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/ShoppingBasket.Api/ShoppingBasket.Api.csproj", "src/ShoppingBasket.Api/"]
COPY ["src/ShoppingBasket.Core/ShoppingBasket.Core.csproj", "src/ShoppingBasket.Core/"]
COPY ["src/ShoppingBasket.Application.Contracts/ShoppingBasket.Application.Contracts.csproj", "src/ShoppingBasket.Application.Contracts/"]
COPY ["src/ShoppingBasket.Application/ShoppingBasket.Application.csproj", "src/ShoppingBasket.Application/"]
COPY ["src/ShoppingBasket.Repository/ShoppingBasket.Repository.csproj", "src/ShoppingBasket.Repository/"]
COPY ["src/ShoppingBasket.Domain/ShoppingBasket.Domain.csproj", "src/ShoppingBasket.Domain/"]
RUN dotnet restore "src/ShoppingBasket.Api/ShoppingBasket.Api.csproj"
COPY . .
WORKDIR "/src/src/ShoppingBasket.Api"
RUN dotnet build "ShoppingBasket.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ShoppingBasket.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ShoppingBasket.Api.dll"]