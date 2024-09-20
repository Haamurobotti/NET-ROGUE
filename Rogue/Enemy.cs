using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using ZeroElectric.Vinculum;
using static System.Net.Mime.MediaTypeNames;

namespace Rogue
{
    internal class Enemy
    {
        public string name;
        public Vector2 position;
        private Texture graphics;
        private ConsoleColor color;

        int imagePixelX;
        int imagePixelY;
        public Enemy(string name, Vector2 position, Texture graphics )
        {
            this.name = name;
            this.position = position;
            this.graphics = graphics;
            
        }
        public void Draw()
        {
            //Console.SetCursorPosition((int)position.X, (int)position.Y);
            Color color = Raylib.WHITE;
            int pixelX = (int)(position.X * Game.tileSize);
            int pixelY = (int)(position.Y * Game.tileSize);
            //Raylib.DrawRectangle(pixelX, pixelY, Game.tileSize, Game.tileSize, Raylib.RED);
            //Raylib.DrawText("@", pixelX, pixelY, Game.tileSize, Raylib.WHITE);
            Rectangle imageRect = new Rectangle(imagePixelX, imagePixelY, Game.tileSize, Game.tileSize);

            float pixelPositionX = position.X * Game.tileSize;
            float pixelPositionY = position.Y * Game.tileSize;
            Vector2 pixelPosition = new Vector2(pixelPositionX, pixelPositionY);
            Raylib.DrawTextureRec(graphics, imageRect, pixelPosition, color);
        }
        public void SetImageAndIndex(Texture atlasImage, int imagesPerRow, int index)
        {
            graphics = atlasImage;
            imagePixelX = (index % imagesPerRow) * Game.tileSize;
            imagePixelY = (int)(index / imagesPerRow) * Game.tileSize;



        }
    }
}
