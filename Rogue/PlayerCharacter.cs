﻿using System.Numerics;
using ZeroElectric.Vinculum;

namespace Rogue
{
    public enum Race
    {
        Human,
        Elf,
        Orc
    }
    public enum Class
    {
        Warrior,
        Mage,
        Rogue
    }
    internal class PlayerCharacter
    {
        public Race rotu;
        public string name;
        public Class luokka;

        public Vector2 sijainti;

        Texture image;
        int imagePixelX;
        int imagePixelY;
        public void Draw()
        {
            Console.SetCursorPosition((int)sijainti.X, (int)sijainti.Y);
            Color color;
            int pixelX = (int)(sijainti.X * Game.tileSize);
            int pixelY = (int)(sijainti.Y * Game.tileSize);
            //Raylib.DrawRectangle(pixelX, pixelY, Game.tileSize, Game.tileSize, Raylib.RED);
            //Raylib.DrawText("@", pixelX, pixelY, Game.tileSize, Raylib.WHITE);
            //Raylib.DrawTextureRec()
        }
        public void Move(int X, int Y)
        {
            sijainti.X += X;
            sijainti.Y += Y;

            if (sijainti.X < 0)
            {
                sijainti.X = 0;
            }
            else if (sijainti.X > Console.WindowWidth - 1)
            {
                sijainti.X -= Console.WindowWidth - 1;
            }
            if (sijainti.Y < 0)
            {
                sijainti.Y = 0;
            }
            else if (sijainti.Y > Console.WindowWidth - 1)
            {
                sijainti.Y -= Console.WindowWidth - 1;
            }
        }
        private void SetImageAndIndex(Texture atlasImage, int imagesPerRow, int index)
        {
            image = atlasImage;
            imagePixelX= (index % imagesPerRow) * Game.tileSize;
            imagePixelY= (int)(index / imagesPerRow) * Game.tileSize;
        }
    }

}
