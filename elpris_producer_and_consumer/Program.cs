using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace elpris_producer_and_consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Run();

            Kafka_consumer kc = new Kafka_consumer();
            kc.Start(true);
            kc.Consume();
        }

        static async void Run()
        { 
            PriceDownloader pd = new PriceDownloader();
            string json = await pd.GetTodaysPricesJsonAsync();

            PriceParser pp = new PriceParser();
            string[] price = pp.ParsePrice(json);

            JSONCache jc = new JSONCache();
            dynamic jsonObject = new JObject();
            jsonObject.Date = price[0];
            jsonObject.Price = price[1];
            string jsonString = JsonConvert.SerializeObject(jsonObject);
            jc.Put(jsonString);

            Kafka_producer kc = new Kafka_producer();
            string message = $"The price today {price[0]}, is {price[1]} dkk";
            kc.Produce(message);
        }
    }
}
