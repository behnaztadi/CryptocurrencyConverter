# **Crypto Convertor**

A simple, not so ambiguous application to convert Cryptocurrency to currency, which is made as an technical assignment.

# **How to run application**

Navigate to source code direct, and then navigate to CryptoConvertor folder. You should be able to see the docker-compose.yaml file. Make sure docker is running on your machine, probably on Linux mode. Then run the following command:  docker-compose up –build

After images downloaded and containers are created, you can navigate to webui using this link: [http://localhost:5012](http://localhost:5012)

# **Tech Stack**

- Dotnet Core 2.2
- SignalR
- Angular
- MS Test, Moq
- Rabbitmq
- Docker
- Docker-Compose

# **Solution Design**

A simple solution design with using Microservice and Dockerise concepts. I tried to keep it quit simple and minimal.

![Image description](https://raw.githubusercontent.com/behnaztadi/CryptocurrencyConverter/master/img/solutiondesign.png)

The solution contains the following projects:

- **Web App** : is a simple UI which triggers the cryptocurrency calculations.
- **Cryptocurrency Calculator** : It gets the exchange rates from Exchange Rates service and calculate the quote for incoming cryptocurrency symbol.
- **Exchange Rate** : An Api which load and distribute the list of exchange rates against base currency, per request from the Message Broker. This project is an Api Core project, but it could be an console app as well.

