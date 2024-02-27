using System.Diagnostics;
using System.Numerics;

namespace Rogue
{
    internal class Game
    {
        Map level101;

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
                if (nameOk) { return nimi; }
            }

        }
        public Race AskRace()
        {
            while (true)
            {
                Console.WriteLine("Select race: ");
                Console.WriteLine("Input a number 1-3");
                Console.WriteLine("1: Human");
                Console.WriteLine("2: Elf");
                Console.WriteLine("3: Orc");
                string raceAnswer = Console.ReadLine();



                int number = 0;
                if (Int32.TryParse(raceAnswer, out number))
                {
                    Console.WriteLine("Got number: " + number);
                    if (number < 1 || number > 3)
                    {
                        Console.WriteLine("The number is not in requested range.");
                    }
                }
                else
                {
                    Console.WriteLine("Cannot convert text \"" + raceAnswer + "\" to integer.");
                }
                if (number == 1)
                {
                    return Race.Human;

                }
                if (number == 2)
                {
                    return Race.Elf;

                }
                if (number == 3)
                {
                    return Race.Orc;

                }
            }
        }
        public Class AskClass()
        {
            while (true)
            {
                Console.WriteLine("Select class:");
                Console.WriteLine("Input a number 1-3");
                Console.WriteLine("1: Warrior");
                Console.WriteLine("2: Mage");
                Console.WriteLine("3: Rogue");
                string classAnswer = Console.ReadLine();

                int number2 = 0;
                if (Int32.TryParse(classAnswer, out number2))
                {
                    Console.WriteLine("Got number: " + number2);
                    if (number2 < 1 || number2 > 3)
                    {
                        Console.WriteLine("The number is not in requested range.");
                    }
                }
                else
                {
                    Console.WriteLine("Cannot convert text \"" + classAnswer + "\" to integer.");
                }


                if (number2 == 1)
                {
                    return Class.Warrior;

                }
                if (number2 == 2)
                {
                    return Class.Mage;

                }
                if (number2 == 3)
                {
                    return Class.Rogue;
                }
            }
        }


        public void Run()
        {


            MapLoader reader = new MapLoader();
            level101 =  reader.ReadMapFromFileTest("Maps/mapfile.json");

            PlayerCharacter player = new PlayerCharacter();

            player.name = AskName();
            player.rotu = AskRace();
            player.luokka = AskClass();


            Console.WriteLine(player.name);
            Console.WriteLine(player.rotu);
            Console.WriteLine(player.luokka);
            player.sijainti = new Vector2(1, 1);
            MapLoader loader = new MapLoader();
            //level101 = loader.LoadTestmap();
            Console.Clear();
            level101.DrawMap();
            player.Draw();
            while (true)
            {
                int moveX = 0;
                int moveY = 0;

                ConsoleKeyInfo key = Console.ReadKey();
                if (key.Key == ConsoleKey.UpArrow)
                {
                    moveY = -1;
                }
                else if (key.Key == ConsoleKey.DownArrow)
                {
                    moveY = 1;
                }
                else if (key.Key == ConsoleKey.LeftArrow)
                {
                    moveX = -1;
                }
                else if (key.Key == ConsoleKey.RightArrow)
                {
                    moveX = 1;
                }
                int newX = (int)player.sijainti.X + moveX;
                int newY = (int)player.sijainti.Y + moveY;
                int tileId = level101.getTileId(newX, newY);
                if (tileId == 2)
                {
                    continue;
                }
                if (tileId == 1)
                {
                    player.Move(moveX, moveY);
                }
                

                
                

                Console.Clear();
                level101.DrawMap();
                player.Draw();
            }


        }

    }
}
