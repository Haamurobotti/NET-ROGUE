using System.Numerics;
using ZeroElectric.Vinculum;

namespace Rogue
{
    public enum MapTile : int
    {
        Floor = 19,
        Wall = 0,
        
    }
    public enum EnemyTile : int
    {
        Demon = 59,
    }
    public enum ItemTile : int
    {
        Amulet = 34
    }
    internal class Map
    {
        public int mapWidth;
        public MapLayer[] layers;
        List<Enemy> enemies;
        List<Item> items;
        public static List<int> WallTileNumbers = new List<int> { 1};
        public static List<int> FloorTileNumbers = new List<int> { 20};
        Texture WallImage;
        Texture TileImage;
        Texture EnemyImage;
        Texture ItemImage;
        public MapTile getTileId(int X, int Y)
        {
           MapLayer ground = layers[0];

            int index = X + Y * mapWidth;
           // int tiledId = ground.mapTiles[index];
            //return (MapTile)tiledId;
            // Calculate index: index = x + y * mapWidth
            int indexInMap = X + Y * mapWidth;

            // Use the index to get a map tile from map's array
            MapLayer groundLayer = GetLayer("ground");
            int[] mapTiles = groundLayer.mapTiles;
            int tileId = mapTiles[indexInMap];

            if (WallTileNumbers.Contains(tileId))
            {
                // Is a wall
                return MapTile.Wall;
            }
            else if (FloorTileNumbers.Contains(tileId))
            {
                // One of the floortiles
                return MapTile.Floor;
            }
            else
            {
                // Count everything else as wall for now.
                return MapTile.Wall;
            }
        }
        public Enemy getEnemyTileId(int X, int Y)
        {
            /*
            for (int i = 0; i < enemies.Count; i++)
            {
                Enemy enemy = enemies[i];
                enemy
                X = X;
            }*/
            foreach (Enemy enemy in enemies)
            {
                if (enemy.position.X == X && enemy.position.Y == Y)
                {
                    
                    return enemy;
                }
                
                
                
                   
            }
            return null;
        }
        public Item getItemTileId(int X, int Y)
        {
            foreach (Item item in items)
            {
                if (item.position.X == X && item.position.Y == Y)
                {

                    return item;
                }
            }
            return null;
        }
        public void DrawMap()
        {
            MapLayer ground = GetLayer("ground");
            int[] mapTiles = ground.mapTiles;

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
                    int tileIndex = tiledId -1;
                    //int pixelX = (int)(sija)
                    Console.SetCursorPosition(x, y);
                    int tileX = x * Game.tileSize;
                    int tileY = y * Game.tileSize;
                    switch (tileIndex)
                    {
                        case (int)MapTile.Floor:
                            Console.Write(".");
                            int tilelX = 2;
                            int tilelY = 2;
                            int imagePixelX1 = (tilelX) * Game.tileSize;
                            int imagePixelY1 = (tilelY) * Game.tileSize;
                            Rectangle imageRectangle1 = new Rectangle(imagePixelX1, imagePixelY1, Game.tileSize, Game.tileSize);
                            Vector2 pixelPosition1 = new Vector2(tileX, tileY);
                            // Raylib.DrawRectangle(tileX,tileY, Game.tileSize, Game.tileSize, Raylib.BEIGE);
                            Raylib.DrawTextureRec(TileImage, imageRectangle1, pixelPosition1, Raylib.WHITE);
                            break;
                        case (int)MapTile.Wall:
                            //Rectangle WallRect = new Rectangle()
                            int wallX = 0;
                            int wallY = 0;
                            int imagePixelX = (wallX) * Game.tileSize;
                            int imagePixelY = (wallY) * Game.tileSize;
                            Rectangle imageRectangle = new Rectangle(imagePixelX, imagePixelY, Game.tileSize, Game.tileSize);
                            Vector2 pixelPosition = new Vector2(tileX, tileY);
                            Console.Write("#");
                            Raylib.DrawRectangle(tileX, tileY, Game.tileSize, Game.tileSize, Raylib.GRAY);
                            Raylib.DrawText("#", tileX, tileY, Game.tileSize, Raylib.WHITE);
                            Raylib.DrawTextureRec(WallImage, imageRectangle, pixelPosition, Raylib.WHITE);
                            break;
                        default:
                            Console.Write(" ");
                            break;
                           /* for (int i = 0; i < enemies.Count; i++)
                            {

                            }*/
                    }
                   /* for (int i = 0; i < items.Count; i++)
                    {
                        Item currentItem = items[i];
                        currentItem.draw
                    }*/

                }
            }
        }
        public void LoadEnemiesAndIems(Texture enemyImage, Texture itemImage)
        {
            enemies = new List<Enemy>();


            MapLayer enemyLayer = layers[1];

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
                        case (int)EnemyTile.Demon:
                            Enemy e = new Enemy("Demon", position, EnemyImage);
                            e.SetImageAndIndex(enemyImage, 8,2);
                             enemies.Add(e);
                                
                            break;
                        case 1:
                            // jne...
                            break;
                    }
                }
                
            }

           


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
                        case (int)ItemTile.Amulet:
                            Item i = new Item("Amulet", position, ItemImage);
                            i.SetImageAndIndex(itemImage, 8, 1);
                            items.Add(i);
                            break;
                        case 2:
                            // jne...
                            break;
                    }
                }
            }
        }
        public void SetImages(Texture Wall, Texture Tile, Texture EnemyI, Texture ItemI)
        {
            WallImage = Wall;
            TileImage = Tile;
            EnemyImage = EnemyI;
            ItemImage = ItemI;
        }
        public MapLayer GetLayer(string layerName)
        {
            for (int i = 0; i < layers.Length; i++)
            {
                if (layers[i].name == layerName)
                {
                    return layers[i];
                }

            }
            Console.WriteLine($"Error: No layer with name: {layerName}");
            return null;
        }
        public void DrawEnemies()
        {
            foreach(Enemy e in enemies)
            {
                e.Draw();
            }
        }
        public void DrawItems()
        {
            foreach(Item i in items)
            {
                i.Draw();
            }
        }
    }
}
