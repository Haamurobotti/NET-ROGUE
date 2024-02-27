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

    }
}
