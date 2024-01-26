using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Rogue
{
    internal class Game
    {
   
        char num = 'm';
        public string AskName() 
        {
            while (true)
            {
                Console.WriteLine("Anna nimi: ");

                string nimi = Console.ReadLine();
                if (string.IsNullOrEmpty(nimi))
                {
                    Console.WriteLine("Ei kelpaa");
                }
                bool nameOk = true;
                for (int i = 0; i < nimi.Length; i++)
                {
                    char kirjain = nimi[i];
                    if (char.IsLetter(kirjain))
                    {

                    }
                    else
                    {
                        nameOk = false;
                        break;
                    }
                }
                if (nameOk) {  return nimi; }
            }

        }
        public Race AskRace()
        {
            while (true)
            {
                Console.WriteLine("Select race: ");
                Console.WriteLine("1: Human");
                Console.WriteLine("2: Elf");
                Console.WriteLine("3: Orc");
                string raceAnswer = Console.ReadLine();
                int rotunumero = System.Convert.ToInt32(raceAnswer);
                Console.WriteLine("Input a number 1-10");
                string input = Console.ReadLine();
                int number = 0;
                if (Int32.TryParse(input, out number))
                {
                    Console.WriteLine("Got number: " + number);
                    if (number < 1 || number > 10)
                    {
                        Console.WriteLine("The number is not in requested range.");
                    }
                }
                else
                {
                    Console.WriteLine("Cannot convert text \"" + input + "\" to integer.");
                }
                if (rotunumero == 1)
                {
                    return Race.Human;

                }
                if (rotunumero == 2)
                {
                    return Race.Elf;

                }
                if (rotunumero == 3)
                {
                    return Race.Orc;

                }
            }
        }
        public Class AskClass()
        {
            while (true) {
                Console.WriteLine("Select class:");
                Console.WriteLine("1: Warrior");
                Console.WriteLine("2: Mage");
                Console.WriteLine("3: Rogue");
                string classAnswer = Console.ReadLine();
                int classnumber = System.Convert.ToInt32(classAnswer);
                if (classnumber == 1)
                {
                   return Class.Warrior;
                    
                }
                if (classnumber == 2)
                {
                    return Class.Mage;
                    
                }
                if (classnumber == 3)
                {
                    return Class.Rogue;
                }
            }
        }
        public void Run() 
        {
            new PlayerCharacter();
            while (true)
            {
                PlayerCharacter player = new PlayerCharacter();
                
                player.name = AskName();
                AskRace();
                AskClass();
                

                
                
                
            }
        }
    }
}
