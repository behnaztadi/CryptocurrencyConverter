using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoConvertor.Infa.Messaging.RabbitMq
{
    public class RabbitMqConfiguration
    {
        public string HostName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string VirtualHost { get; set; }
        public string Port { get; set; }

        public string Uri => $"rabbitmq://{UserName}:{Password}@{HostName}:{Port}";

        public string ExchangeLoadedQueueName { get; set; }
        public string LoadExchangeQueueName { get; set; }

        public string ExchangeLoadedQueueNameUri => $"rabbitmq://{HostName}/{ExchangeLoadedQueueName}";

        public string LoadExchangeQueueNameUri => $"rabbitmq://{HostName}/{LoadExchangeQueueName}";
    }
}
