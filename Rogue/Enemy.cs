using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Rogue
{
    internal class Enemy
    {
        public string name;
        public Vector2 position;
        private char graphics;
        private ConsoleColor color;
        public Enemy(string name, Vector2 position, char graphics, ConsoleColor color)
        {
            this.name = name;
            this.position = position;
            this.graphics = graphics;
            this.color = color;
        }
    }
}
