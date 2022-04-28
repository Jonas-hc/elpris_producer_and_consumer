using System;
using System.Globalization;
using Newtonsoft.Json.Linq;

namespace elpris_producer_and_consumer
{
    public class PriceParser
    {
        public string Json = "";
        public PriceParser()
        {
        }

        public string[] ParsePrice(string json)
        {
            string[] myInfo = new string[2];
            string time = "0";
            string price = "0.0";
            myInfo[0] = time;
            myInfo[1] = price;

            DateTime now = DateTime.Now;
            string t1 = now.ToString("%M");
            string t2 = now.ToString("%d");
            string t3 = now.ToString("yyyy");
            string t4 = t1 + "/" + t2 + "/" + t3;

            
            var jo = JObject.Parse(json);
            foreach (JObject item in jo["data"]["elspotprices"])
            {
                if (item["HourDK"].ToString().Substring(0, 10).IndexOf(t4) > -1)
                {
                    if (item["PriceArea"].ToString().IndexOf("DK") > -1)
                    {
                        time = t4;
                        myInfo[0] = time;
                        if (item["SpotPriceDKK"].ToString().Length < 0)
                        {
                            myInfo[1] = item["SpotPriceDKK"].ToString();
                            
                        }
                    }
                }
            }
            return myInfo;
        }
    }
}
