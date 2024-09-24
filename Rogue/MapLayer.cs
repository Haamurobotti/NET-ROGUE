using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue
{
    internal class MapLayer
    {
        public string name;
        public int[] mapTiles;
        private int howManyTiles;

        public MapLayer(int howManyTiles)
        {
            this.howManyTiles = howManyTiles;
        }
    }
}
