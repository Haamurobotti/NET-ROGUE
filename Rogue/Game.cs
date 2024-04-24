using System.Numerics;
using ZeroElectric.Vinculum;
namespace Rogue
{
    internal class Game
    {
        Map level101;
        PlayerCharacter player;
        public static readonly int tileSize = 16;

        int screen_Width = 1280;
        int screen_Height = 720;

        int game_width;
        int game_Height;

        RenderTexture game_screen;
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

        private PlayerCharacter CreateCharacter()
        {
            PlayerCharacter player = new PlayerCharacter();

            player.name = AskName();
            player.rotu = AskRace();
            player.luokka = AskClass();
            return player;
        }

        public void Run()
        {
           
            Init();
            GameLoop();
            Raylib.CloseWindow();
            Raylib.UnloadRenderTexture(game_screen);

        }
        private void Init()
        {

            player = CreateCharacter();
            player.sijainti = new Vector2(1, 1);
            MapLoader loader = new MapLoader();
            level101 = loader.LoadFromFile("Maps/mapfile.json");
            Raylib.InitWindow(screen_Width, screen_Height, "Rogue");
            Raylib.SetWindowState(ConfigFlags.FLAG_WINDOW_RESIZABLE);
            Raylib.SetTargetFPS(30);

            game_width = 480;
            game_Height = 270;

            game_screen = Raylib.LoadRenderTexture(game_width, game_Height);
            Raylib.SetTextureFilter(game_screen.texture, TextureFilter.TEXTURE_FILTER_BILINEAR);
            Raylib.SetWindowMinSize(game_width, game_Height);

            Texture playerTexture = Raylib.LoadTexture("Images/Paladin.png");
            Texture mapWallTexture = Raylib.LoadTexture("Images/Wall.png");
            Texture mapTileTexture = Raylib.LoadTexture("Images/Tile.png");
            player.SetImageAndIndex(playerTexture, 4, 0);
            //player.SetImageAndIndex(mapTileTexture, 1, 0);
            
        }

        

        private void DrawGameToTexture()
        {

            Raylib.BeginTextureMode(game_screen);

            Draw();

            Raylib.EndTextureMode();
            DrawGameScaled();
        }

        private void DrawGameScaled()
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Raylib.DARKGRAY);

            int draw_width = Raylib.GetScreenWidth();
            int draw_height = Raylib.GetScreenHeight();
            float scale = Math.Min((float)draw_width / game_width, (float)draw_height / game_Height);

            Rectangle source = new Rectangle(0.0f, 0.0f,
                game_screen.texture.width,
                game_screen.texture.height * -1.0f);

            Rectangle destination = new Rectangle(
                (draw_width - (float)game_width * scale) * 0.5f,
                (draw_height) - (float)game_Height * scale * 0.5f,
                game_width * scale,
                game_Height * scale);

            Raylib.DrawTexturePro(game_screen.texture, source, destination, new Vector2(0,0), 0.0f, Raylib.WHITE);
            Raylib.EndDrawing();
        }

        private void Draw()
        {
            
            Console.Clear();
            Raylib.ClearBackground(Raylib.BLACK);
            
            level101.DrawMap();
            player.Draw();
            
        }

        private void UpdateGame()
        {


            //level101 = loader.LoadTestmap();


            int moveX = 0;
            int moveY = 0;

            //ConsoleKeyInfo key = Console.ReadKey();
            
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_UP))
            {
                moveY = -1;
            }
            else if (Raylib.IsKeyPressed(KeyboardKey.KEY_DOWN))
            {
                moveY = 1;
            }
            else if (Raylib.IsKeyPressed(KeyboardKey.KEY_LEFT))
            {
                moveX = -1;
            }
            else if (Raylib.IsKeyPressed(KeyboardKey.KEY_RIGHT))
            {
                moveX = 1;
            }
            int newX = (int)player.sijainti.X + moveX;
            int newY = (int)player.sijainti.Y + moveY;
            MapTile tileId = level101.getTileId(newX, newY);

            
            if (tileId == MapTile.Floor)
            {
                player.Move(moveX, moveY);
            }







        }
        private void GameLoop()
        {
            while (Raylib.WindowShouldClose() == false)
            {
                UpdateGame();
                DrawGameToTexture();
            }
        }
    }
}
