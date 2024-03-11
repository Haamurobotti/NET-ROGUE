using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Rogue
{
    internal class Map
    {
        public int mapWidth;
        public MapLayer[] layers;
        List<Enemy> enemies;
        List<Item> items;
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
        public void LoadEnemiesAndIems()
        {
            enemies = new List<Enemy>();


            MapLayer enemyLayer = layers[2];

            int[] enemyTiles = enemyLayer.mapTiles;
            int mapHeight = enemyTiles.Length / mapWidth;
            for (int y = 0; y < mapHeight; y++)
            {
                for (int x = 0; x < mapWidth; x++)
                {
                    Vector2 position = new Vector2(x, y);
                    int index = x + y * mapWidth;
                    int tileId = enemyTiles[index];
                    switch (tileId)
                    {
                        case 0:
                            // ei mitään tässä kohtaa
                            break;
                        case 1:
                            enemies.Add(new Enemy("Orc", position, 'o', ConsoleColor.Red));
                            break;
                        case 2:
                            // jne...
                            break;
                    }
                }
            }

            items = new List<Item>();


            MapLayer itemLayers = layers[2];

            // sama esineille...
            items = new List<Item>();
            int[] itemTiles = itemLayers.mapTiles;
            mapHeight = itemTiles.Length / mapWidth;
            for (int y = 0; y < mapHeight; y++)
            {
                for (int x = 0; x < mapWidth; x++)
                {
                    Vector2 position = new Vector2(x, y);
                    int index = x + y * mapWidth;
                    int tileId = itemTiles[index];
                    switch (tileId)
                    {
                        case 0:
                            // ei mitään tässä kohtaa
                            break;
                        case 1:
                            items.Add(new Item("Sword", position, 's', ConsoleColor.Red));
                            break;
                        case 2:
                            // jne...
                            break;
                    }
                }
            }
        }
    }
}
