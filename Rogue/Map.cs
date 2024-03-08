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
        public MapLayer[] layers;
        public int getTileId(int X, int Y)
        {
            MapLayer ground = layers[0];

            int index = X + Y * mapWidth;
            int tiledId = ground.mapTiles[index];
            return tiledId;
        }
        public void DrawMap()
        {
            MapLayer ground = layers[0];
            Console.ForegroundColor = ConsoleColor.Gray;
            int mapHeight = ground.mapTiles.Length / mapWidth;
            for (int y = 0; y < mapHeight; y++)
            {
                for (int x = 0; x < mapWidth; x++)
                {
                    int index = x + y * mapWidth;
                    int tiledId = ground.mapTiles[index];

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
