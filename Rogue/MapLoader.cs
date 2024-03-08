

using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Xml.Linq;

namespace Rogue
{
    internal class MapLoader
    {
        public Map LoadTestmap()
        {
            Map test = new Map();
            test.mapWidth = 8;
            test.layers = new MapLayer[3]; 
           test.layers[0].mapTiles = new int[] 
           {
            2, 2, 2, 2, 2, 2, 2, 2,
            2, 1, 1, 2, 1, 1, 1, 2,
            2, 1, 1, 2, 1, 1, 1, 2,
            2, 1, 1, 1, 1, 1, 2, 2,
            2, 2, 2, 2, 1, 1, 1, 2,
            2, 1, 1, 1, 1, 1, 1, 2,
            2, 2, 2, 2, 2, 2, 2, 2
            };
            return test;
        }
        

         public Map LoadFromFile(string fileName)
         {
            bool fileFound = File.Exists(fileName);
            if (fileFound == false)
            {
                Console.WriteLine($"File {fileName} not found");
                return LoadTestmap(); // Return the test map as fallback
            }


            using (StreamReader reader = File.OpenText(fileName))
            {
                Map map = JsonConvert.DeserializeObject<Map>(reader.ReadToEnd());
                return map;
            }


        }
    }
    
}
