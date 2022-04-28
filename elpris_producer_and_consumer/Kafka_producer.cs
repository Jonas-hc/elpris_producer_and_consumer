using System;
using System.Net;
using Confluent.Kafka;

namespace elpris_producer_and_consumer
{
    public class Kafka_producer
    {
        public Kafka_producer()
        {
        }

        public async void Produce(string message)
        {
            var config = new ProducerConfig
            {
                BootstrapServers = "localhost:9092",
                ClientId = Dns.GetHostName(),
            };

            using var producer = new ProducerBuilder<Null, string>(config).Build();
            _ = await producer.ProduceAsync("quickstart-events", new Message<Null, string> { Value = message });
        }
    }
}
