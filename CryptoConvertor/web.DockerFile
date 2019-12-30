FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["CryptoConvertor.UI.Web/CryptoConvertor.UI.Web.csproj", "CryptoConvertor.UI.Web/"]
RUN dotnet restore "./CryptoConvertor.UI.Web/CryptoConvertor.UI.Web.csproj"

# Setup NodeJs
RUN apt-get update && \
    apt-get install -y wget && \
    apt-get install -y gnupg2 && \
    wget -qO- https://deb.nodesource.com/setup_10.x | bash - && \
    apt-get install -y build-essential nodejs

COPY . .
WORKDIR /src
RUN dotnet build "./CryptoConvertor.UI.Web/CryptoConvertor.UI.Web.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "./CryptoConvertor.UI.Web/CryptoConvertor.UI.Web.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "./CryptoConvertor.UI.Web.dll"]