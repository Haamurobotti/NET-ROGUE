using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using ZeroElectric.Vinculum;

namespace Rogue
{
    public enum MapTile : int
    {
        Floor = 5,
        Wall = 2
    }
    internal class Map
    {
        public int mapWidth;
        public MapLayer[] layers;
        List<Enemy> enemies;
        List<Item> items;
        Texture WallImage;
        public MapTile getTileId(int X, int Y)
        {
            MapLayer ground = layers[0];

            int index = X + Y * mapWidth;
            int tiledId = ground.mapTiles[index];
            return (MapTile)tiledId;
        }
        public void DrawMap()
        {
            MapLayer ground = layers[0];
           
            Console.ForegroundColor = ConsoleColor.Green;
            int mapHeight = ground.mapTiles.Length / mapWidth;
            
            for (int y = 0; y < mapHeight; y++)
            {
                for (int x = 0; x < mapWidth; x++)
                {
                    
                    int index = x + y * mapWidth;
                    int tiledId = ground.mapTiles[index];
                    if (tiledId == 0)
                    {
                        continue;
                    }
                    int tileIndex = tiledId - 1;
                    //int pixelX = (int)(sija)
                    Console.SetCursorPosition(x, y);
                    int tileX = x * Game.tileSize;
                    int tileY = y * Game.tileSize;
                    switch (tileIndex)
                    {
                        case 1:
                            Console.Write(".");
                           
                            Raylib.DrawRectangle(tileX,tileY, Game.tileSize, Game.tileSize, Raylib.BEIGE);
                            //Raylib.DrawTextureRec(WallImage, )
                            break;
                        case 2:
                            //Rectangle WallRect = new Rectangle()
                            float wallX = 1 % 8;
                            float wallY = 1 / 8;
                            Console.Write("#");
                            Raylib.DrawRectangle(tileX, tileY, Game.tileSize, Game.tileSize, Raylib.GRAY);
                            Raylib.DrawText("#", tileX, tileY, Game.tileSize, Raylib.WHITE);
                           // Raylib.DrawTextureRec(WallImage, )
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
