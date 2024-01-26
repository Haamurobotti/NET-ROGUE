using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
