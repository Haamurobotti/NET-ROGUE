

namespace Rogue
{
    internal class MapLoader
    {
        public Map LoadTestmap()
        {
            Map test = new Map();
            test.mapWidth = 8;
            test.mapTiles = new int[] {
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
        public Map ReadMapFromFileTest(string fileName)
        {
            using (StreamReader reader = File.OpenText(fileName))
            {
               

                string line;
                while (true)
                {
                    line = reader.ReadLine();
                    if (line == null)
                    {
                        break; // End of file
                    }
                    Console.WriteLine(line);
                }

            }
            return LoadTestmap(); // Return the test map.
        }

         public Map LoadFromFile(string fileName)
        {
            bool fileFound = File.Exists(fileName);
            if (fileFound == false)
            {
                Console.WriteLine($"File {fileName} not found");
                return LoadTestmap(); // Return the test map as fallback
            }

            string fileContents;

            using (StreamReader reader = File.OpenText(fileName))
            {
                // TODO: Read all lines into fileContens
            }

            Map loadedMap = /*Use the correct JSON function here*/(fileContents);

            return loadedMap;
        }
    }
    
}
