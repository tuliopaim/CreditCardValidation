#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0-alpine AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/CreditCardValidation/CreditCardValidation.csproj", "src/CreditCardValidation/"]
COPY ["src/CreditCardValidation.Core/CreditCardValidation.Core.csproj", "src/CreditCardValidation.Core/"]
RUN dotnet restore "src/CreditCardValidation/CreditCardValidation.csproj"
COPY . .
WORKDIR "/src/src/CreditCardValidation"
RUN dotnet build "CreditCardValidation.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CreditCardValidation.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CreditCardValidation.dll"]