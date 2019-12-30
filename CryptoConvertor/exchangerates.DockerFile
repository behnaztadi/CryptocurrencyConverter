FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["CryptoConvertor.Services.ExchnageRates/CryptoConvertor.Services.ExchnageRates.csproj", "CryptoConvertor.Services.ExchnageRates/"]
RUN dotnet restore "./CryptoConvertor.Services.ExchnageRates/CryptoConvertor.Services.ExchnageRates.csproj"
COPY . .
WORKDIR /src
RUN dotnet build "./CryptoConvertor.Services.ExchnageRates/CryptoConvertor.Services.ExchnageRates.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "./CryptoConvertor.Services.ExchnageRates/CryptoConvertor.Services.ExchnageRates.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "./CryptoConvertor.Services.ExchnageRates.dll"]