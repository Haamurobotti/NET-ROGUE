using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Threading;

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
       public void Draw()
        {
            Console.SetCursorPosition((int)sijainti.X, (int)sijainti.Y);
            Console.Write("@");
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
    }
    
}
