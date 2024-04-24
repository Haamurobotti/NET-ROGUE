using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using ZeroElectric.Vinculum;
namespace RaylibTest
{
    internal class Test
    {
        const int screen_width = 500;
        const int screen_Height = 500;

        private float circleX = 50;
        private float circleY = 50;
       
        private float clearCircleX = 50;
        private float clearCircleY = 100;
       
        private float HeadCircleX = 50;
        private float HeadCircleY = 5;

        private float eyeDot1X = 40;
        private float eyeDot1Y = -5;

        private float eyeDot2X = 60;
        private float eyeDot2Y = -5;

        private float carrotX1 = 50;
        private float carrotY1 = 30;
        
        private float carrotX2 = -50;
        private float carrotY2 = 50;

        private float carrotX3 = 40;
        private float carrotY3 = 50;
        public void Run()
        {
            Raylib.InitWindow(screen_width, screen_Height, "testi");

            while (Raylib.WindowShouldClose() == false)
            {
                Update();
                Draw();
                
            }
        }
        void Draw()
        {
            Raylib.BeginDrawing();
            
            Raylib.ClearBackground(Raylib.BLACK);
            
            Raylib.DrawCircle((int)circleX, (int)circleY, 40, Raylib.WHITE);
            
            Raylib.DrawCircle((int)HeadCircleX, (int)HeadCircleY, 30, Raylib.WHITE);
            
            Raylib.DrawCircle((int)clearCircleX, (int)clearCircleY, 50, Raylib.WHITE);
            
            Raylib.DrawCircle((int)eyeDot1X, (int)eyeDot1Y, 5, Raylib.BLACK);

            Raylib.DrawCircle((int)eyeDot2X, (int)eyeDot2Y, 5, Raylib.BLACK);
           
            Raylib.DrawTriangle((new Vector2(carrotX1, carrotY1)),(new Vector2(carrotX2, carrotY2)),(new Vector2(carrotX3, carrotY3)), Raylib.ORANGE);
            

            Raylib.EndDrawing();
        }
        void Update()
        {
            circleX += 10 * Raylib.GetFrameTime();
            clearCircleX += 10 * Raylib.GetFrameTime();
            
            circleY += 10 * Raylib.GetFrameTime();
            clearCircleY += 10 * Raylib.GetFrameTime();
            
            HeadCircleY += 10 * Raylib.GetFrameTime();
            HeadCircleX += 10 * Raylib.GetFrameTime();

            eyeDot1X += 10 * Raylib.GetFrameTime();
            eyeDot1Y += 10 * Raylib.GetFrameTime();

            eyeDot2X += 10 * Raylib.GetFrameTime();
            eyeDot2Y += 10 * Raylib.GetFrameTime();

            carrotX1 = HeadCircleX + 2;
            carrotY1 = HeadCircleY + 2;

            carrotX2 = carrotX1 - 12;
            carrotY2 = carrotY1 - 12;

            carrotX3 = carrotX2 - 10;
            carrotY3 = carrotY2 - 10;
        }
    }
}
