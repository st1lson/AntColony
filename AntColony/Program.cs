using AntColony.Core;
using AntColony.Handlers;
using Newtonsoft.Json;
using System.IO;

namespace AntColony
{
    internal class Program
    {
        static void Main(string[] args) 
        {
            string data = File.ReadAllText("config.json");
            Config config = JsonConvert.DeserializeObject<Config>(data);
            new InputHandler(config).Menu();
        }
    }
}
