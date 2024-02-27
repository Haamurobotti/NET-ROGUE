using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue
{
    internal class Map
    {
        public int mapWidth;
        public int[] mapTiles;
        public int getTileId(int X, int Y)
        {
            int index = X + Y * mapWidth;
            int tiledId = mapTiles[index];
            return tiledId;
        }
        public void DrawMap()
        {

            Console.ForegroundColor = ConsoleColor.Gray;
            int mapHeight = mapTiles.Length / mapWidth;
            for (int y = 0; y < mapHeight; y++)
            {
                for (int x = 0; x < mapWidth; x++)
                {
                    int index = x + y * mapWidth;
                    int tiledId = mapTiles[index];

                    Console.SetCursorPosition(x, y);
                    switch (tiledId)
                    {
                        case 1:
                            Console.Write(".");
                            break;
                        case 2:
                            Console.Write("#");
                            break;
                        default:
                            Console.Write(" ");
                            break;
                    }

                }
            }
        }
    }
}
