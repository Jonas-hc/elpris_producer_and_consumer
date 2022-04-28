using System;
using System.IO;

namespace elpris_producer_and_consumer
{
    public class JSONCache
    {
        public JSONCache()
        {
        }

        public void Put(string json)
        {
            string filename = GetFilename();
            File.WriteAllText(filename, json);
        }

        public string GetFilename()
        {
            return $"elpris.json";
        }
    }
}
