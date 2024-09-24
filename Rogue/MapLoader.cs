using Newtonsoft.Json;
using TurboMapReader;

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


        public Map? LoadFromFile(string fileName)
        {
            bool fileFound = File.Exists(fileName);

            if (fileFound == false)
            {
                Console.WriteLine($"File {fileName} not found");
                return LoadTestmap(); // Return the test map as fallback
            }


            /*using (StreamReader reader = File.OpenText(fileName))
            {

                Map map = JsonConvert.DeserializeObject<Map>(reader.ReadToEnd());
                
                return map;
            }*/
            // Lataa tiedosto käyttäen TurboMapReaderia   
            TurboMapReader.TiledMap mapMadeInTiled = TurboMapReader.MapReader.LoadMapFromFile(fileName);

            // Tarkista onnistuiko lataaminen
            if (mapMadeInTiled != null)
            {
                // Muuta Map olioksi ja palauta
                return ConvertTiledMapToMap(mapMadeInTiled);
            }
            else
            {
                // OH NO!
                return null;
            }


        }
        public Map ConvertTiledMapToMap(TiledMap turboMap)
        {
            // Luo tyhjä kenttä
            Map rogueMap = new Map();
            rogueMap.layers = new MapLayer[3];
           
            // Muunna tason "ground" tiedot
            TurboMapReader.MapLayer groundLayer = turboMap.GetLayerByName("ground");
            rogueMap.mapWidth = groundLayer.width;
            TurboMapReader.MapLayer enemyLayer = turboMap.GetLayerByName("enemies");
            TurboMapReader.MapLayer itemLayer = turboMap.GetLayerByName("items");
            // TODO: Lue kentän leveys. Kaikilla TurboMapReader.MapLayer olioilla on sama leveys

            // Kuinka monta kenttäpalaa tässä tasossa on?
            int howManyTiles = groundLayer.data.Length;
            
            // Taulukko jossa palat ovat
            int[] groundTiles = groundLayer.data;
            int[] enemyTiles = enemyLayer.data;
            int[] itemTiles = itemLayer.data; 
            // Luo uusi taso tietojen perusteella
            MapLayer myGroundLayer = new MapLayer(howManyTiles);
            myGroundLayer.name = "ground";

            MapLayer myEnemyLayer = new MapLayer(howManyTiles);
            myEnemyLayer.name = "enemies";

            MapLayer myItemLayer = new MapLayer(howManyTiles);
            myItemLayer.name = "enemies";
            // TODO: lue tason palat
            myGroundLayer.mapTiles = groundTiles;
            myEnemyLayer.mapTiles = enemyTiles;
            myItemLayer.mapTiles = itemTiles;   

            // Tallenna taso kenttään
            rogueMap.layers[0] = myGroundLayer;

            // TODO: Muunna tason "enemies" tiedot...
            rogueMap.layers[1] = myEnemyLayer;
            // TODO: Muunna tason "items" tiedot...
            rogueMap.layers[2] =  myItemLayer;
            // Lopulta palauta kenttä
            return rogueMap;
        }
    }

}
