FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["CryptoConvertor.Services.CryptoCurrency/CryptoConvertor.Services.CryptoCurrency.csproj", "CryptoConvertor.Services.CryptoCurrency/"]
RUN dotnet restore "./CryptoConvertor.Services.CryptoCurrency/CryptoConvertor.Services.CryptoCurrency.csproj"
COPY . .
WORKDIR /src
RUN dotnet build "./CryptoConvertor.Services.CryptoCurrency/CryptoConvertor.Services.CryptoCurrency.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "./CryptoConvertor.Services.CryptoCurrency/CryptoConvertor.Services.CryptoCurrency.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "./CryptoConvertor.Services.CryptoCurrency.dll"]