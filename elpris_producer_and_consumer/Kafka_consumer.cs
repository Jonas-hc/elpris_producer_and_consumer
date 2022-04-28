using System;
using System.Threading;
using Confluent.Kafka;

namespace elpris_producer_and_consumer
{
    public class Kafka_consumer
    {
        private bool running;
        public Kafka_consumer()
        {
        }

        public void Start(bool run)
        {
            running = run;
        }

        public void Consume()
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = "localhost:9092",
                GroupId = "Gruppe",
                AutoOffsetReset = AutoOffsetReset.Latest,
            };
            using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
            consumer.Subscribe("quickstart-events");
            Console.WriteLine("Ready");

            while (running)
            {
                var consumeResult = consumer.Consume();

                string message = consumeResult.Message.Value;

                Console.WriteLine($"Besked modtaget: {message}");

                Console.WriteLine("Sleep");
                Thread.Sleep(5000);
                Console.WriteLine("Awake");

                Start(false);
            }
            Console.WriteLine("Close");
            consumer.Close();
        }
    }
}
