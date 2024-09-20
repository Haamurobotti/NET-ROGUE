using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using RayGuiCreator;
using ZeroElectric.Vinculum;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Rogue
{
    internal class Game
    {
      public  enum GameState
        {
            MainMenu,
            GameLoop,
            CharacterCreation,
            PauseMenu,
            OptionsMenu
        }

       public Stack<GameState> stateStack = new Stack<GameState>();
        

        OptionsMenu myOptionsMenu;
        PauseMenu myPauseMenu;

        Map level101;
        PlayerCharacter player;
        
        public static readonly int tileSize = 16;

        int screen_Width = 1280;
        int screen_Height = 720;

        int game_width;
        int game_Height;

        RenderTexture game_screen;

        Texture playerTexture;
        Texture mapWallTexture;
        Texture mapTileTexture;
        Texture EnemyTexture;
        Texture ItemTexture;

        GameState currentGameState;
        TextBoxEntry playerNameEntry;
        public MultipleChoiceEntry race = new MultipleChoiceEntry(new string[] { "Human", "Elf", "Orc" });
        public MultipleChoiceEntry characterClass = new MultipleChoiceEntry(new string[] { "Warrior", "Mage", "Rogue" });
        

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

           // player.name = AskName();
            //player.rotu = AskRace();
            //player.luokka = AskClass();
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
            myOptionsMenu = new OptionsMenu(this);
            myOptionsMenu.BackButtonPressedEvent += this.OnOptionsBackButtonPressed;
            myPauseMenu = new PauseMenu(this);
            myPauseMenu.BackButtonPressedEvent += this.OnOptionsBackButtonPressed;
            myPauseMenu.OptionsButtonPressedEvent += this.OnOptionsButtonPressed;
            myPauseMenu.MainMenuButtonPressedEvent += this.OnMainMenuButtonPressed;
            Raylib.InitWindow(screen_Width, screen_Height, "Rogue");
            EnemyTexture = Raylib.LoadTexture("Images/Demon0.png");
            ItemTexture = Raylib.LoadTexture("Images/Amulet.png");
            level101.LoadEnemiesAndIems(EnemyTexture, ItemTexture);
            
            Raylib.SetWindowState(ConfigFlags.FLAG_WINDOW_RESIZABLE);
            Raylib.SetTargetFPS(30);

            game_width = 480;
            game_Height = 270;

            game_screen = Raylib.LoadRenderTexture(game_width, game_Height);
            Raylib.SetTextureFilter(game_screen.texture, TextureFilter.TEXTURE_FILTER_BILINEAR);
            Raylib.SetWindowMinSize(game_width, game_Height);

            playerTexture = Raylib.LoadTexture("Images/Paladin.png");
            mapWallTexture = Raylib.LoadTexture("Images/Tile.png");
            mapTileTexture = Raylib.LoadTexture("Images/Tile.png");
            player.SetImageAndIndex(playerTexture, 4, 0);
            
            //Enemy.SetImageAndIndex(EnemyTexture,0,0);
            level101.SetImages(mapWallTexture, mapTileTexture, EnemyTexture, ItemTexture);
            //player.SetImageAndIndex(mapTileTexture, 1, 0);
            currentGameState = GameState.MainMenu;
            playerNameEntry = new TextBoxEntry(12);

           /* stateStack.Push(GameState.GameLoop);
            stateStack.Push(GameState.MainMenu);
         
            stateStack.Push(GameState.PauseMenu);
            stateStack.Push(GameState.OptionsMenu);*/


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

            Raylib.DrawTexturePro(game_screen.texture, source, destination, new Vector2(0, 0), 0.0f, Raylib.WHITE);
            
            Raylib.EndDrawing();
        }

        private void Draw()
        {

            Console.Clear();
            Raylib.ClearBackground(Raylib.BLACK);

            level101.DrawMap();
            level101.DrawEnemies();
            level101.DrawItems();
            player.Draw();

            int moveX = 0;
            int moveY = 0;
            int newX = (int)player.sijainti.X + moveX;
            int newY = (int)player.sijainti.Y + moveY;
            
            Enemy eTile = level101.getEnemyTileId(newX,newY);
            if (eTile != null)
            {
                
                Raylib.DrawText($"Törmäsit: {eTile.name}iin", 50, 330, 20, Raylib.WHITE);
              
            }
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

            MapTile tile = level101.getTileId(newX, newY);
           
            Item iTile = level101.getItemTileId(newX, newY);
            Enemy eTile = level101.getEnemyTileId(newX, newY);
            if (iTile != null)
            {
                Raylib.BeginDrawing();
                Raylib.DrawText($"Löysit: {iTile.name}in", 50, 330, 20, Raylib.WHITE);
                Raylib.EndDrawing();
            }
            if (tile == MapTile.Floor && eTile == null && iTile == null)
            {
                player.Move(moveX, moveY);
            }


            if (Raylib.IsKeyPressed(KeyboardKey.KEY_F))
            {
                stateStack.Push(GameState.PauseMenu);
            }
            






        }

        void OnOptionsBackButtonPressed(object sender, EventArgs args)
        {
            currentGameState = GameState.MainMenu;
        }
        void OnOptionsButtonPressed(object sender, EventArgs args)
        {
            currentGameState = GameState.OptionsMenu;
        }
        void OnMainMenuButtonPressed(object sender, EventArgs args)
        {
            currentGameState = GameState.OptionsMenu;
        }
        void DrawMainMenu(int x, int y, int width)
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Raylib.GetColor(((uint)RayGui.GuiGetStyle(((int)GuiControl.DEFAULT), ((int)GuiDefaultProperty.BACKGROUND_COLOR)))));
            MenuCreator c = new MenuCreator(x, y, Raylib.GetScreenHeight() / 20, width);
            // int button_width = 100;
            //int button_height = 20;
            // int button_x = Raylib.GetScreenWidth() / 2 - button_width / 2;
            // int button_y = Raylib.GetScreenHeight() / 2 - button_height / 2;
            // RayGui.GuiLabel(new Rectangle(button_x, button_y - button_height * 2, button_width, button_height), "Rogue");
            c.Label("Rogue");
            c.Label("Ohjeet:");
            c.Label("Liiku nuolinäppäimillä");
            if (c.Button("Start game"))
            {
                currentGameState = GameState.CharacterCreation;
                stateStack.Push(GameState.CharacterCreation);
            }
            if (c.Button("Options"))
            {
                currentGameState = GameState.OptionsMenu;
                stateStack.Push(GameState.OptionsMenu);
            }
            if (c.Button("Quit"))
            {
                System.Environment.Exit(0);
            }
            //RayGui.GuiLabel(new Rectangle(button_x, button_y , button_width, button_height), "Ohjeet:");
            //RayGui.GuiLabel(new Rectangle(button_x, button_y + button_height*1, button_width, button_height), "Liiku: wasd tai ");
            //RayGui.GuiLabel(new Rectangle(button_x, button_y + button_height*2, button_width, button_height), "nuoli näppäimet");
            /* button_y += button_height * 3;
             if (RayGui.GuiButton(new Rectangle(button_x, button_y
                 , button_width, button_height), "Start Game") == 1)
             {
                 // Start the game
                 currentGameState = GameState.CharacterCreation;
                 stateStack.Push(GameState.CharacterCreation);
             }*/
            // Piirrä seuraava nappula edellisen alapuolelle
            /* button_y += button_height * 2;
             if (RayGui.GuiButton(new Rectangle(button_x, button_y
                 , button_width, button_height), "Options") == 1)
             {
                 // Start the game
                 currentGameState = GameState.OptionsMenu;
                 stateStack.Push(GameState.OptionsMenu);

             }*/
            /* button_y += button_height * 2;
             if (RayGui.GuiButton(new Rectangle(button_x, button_y
                 , button_width, button_height), "Pause") == 1)
             {
                 // Start the game
                 currentGameState = GameState.PauseMenu;
                 stateStack.Push(GameState.PauseMenu);
             }*/

            /*button_y += button_height * 2;

            if (RayGui.GuiButton(new Rectangle(button_x,
                button_y,
                button_width, button_height), "Quit") == 1)
            {
                // Quit the game
                System.Environment.Exit(0);
            }*/
            Raylib.EndDrawing();
        }
        void DrawCharacterMenu(int x, int y, int width)
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Raylib.GetColor(((uint)RayGui.GuiGetStyle(((int)GuiControl.DEFAULT), ((int)GuiDefaultProperty.BACKGROUND_COLOR)))));
            MenuCreator c = new MenuCreator(x, y,Raylib.GetScreenHeight() / 20 ,width);
            c.Label("Create character");
            c.Label("Name Character");
          
            c.TextBox(playerNameEntry);
            c.Label("Select class");
            c.DropDown(characterClass);
            c.Label("Select race");
            c.DropDown(race);

            if (c.Button("Start Game"))
            {
                bool nameOk = true;
                var nimi = playerNameEntry.ToString();
                if (string.IsNullOrEmpty(nimi))
                {

                    Console.WriteLine("Ei kelpaa");
                    nameOk = false;
                }
                
                

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
                string raceAnswer = race.ToString();
                string classAnswer = characterClass.ToString();

                bool raceSelect = false;
                bool classSelect = false;
              
                if (raceAnswer == "Human")
                {
                    raceSelect = true;
                    player.rotu = Race.Human;
                }
                if (raceAnswer == "Elf")
                {
                    raceSelect = true;
                    player.rotu = Race.Elf;
                }
                if (raceAnswer == "Orc")
                {
                    player.rotu = Race.Orc;
                    raceSelect = true;
                }
                if (classAnswer == "Warrior")
                {
                    player.luokka = Class.Warrior;
                   classSelect  = true;
                }
                if (classAnswer == "Mage")
                {
                    player.luokka = Class.Mage;
                    classSelect = true;

                }
                if (classAnswer == "Rogue")
                {
                    player.luokka = Class.Rogue;
                    classSelect = true;

                }

                if (nameOk && raceSelect == true && classSelect == true) { stateStack.Push(GameState.GameLoop); }
                
            }
            if (c.Button("Back"))
            {
                stateStack.Pop();
            }
            c.EndMenu();
            Raylib.EndDrawing();
        }
      /*  public void PrintValues()
        {
            Console.WriteLine(">>>>>> Menu values:");
            Console.WriteLine("\nPlayer name: \"{playerNameEntry.ToString()}\"\nPlayer class {race.GetSelected()}\nDifficulty: {characterClass.GetSelected()}");
        }*/
        private void GameLoop()
        {
            stateStack.Push(GameState.MainMenu);
            int x = 0;
            int y = 40;
            int width = 300;
            while (Raylib.WindowShouldClose() == false)
            {
                switch (stateStack.Peek())
                {
                    case  GameState.MainMenu:
                        DrawMainMenu(x + Raylib.GetScreenWidth() / 2 - width / 2, y, width);
                        break;
                    case GameState.GameLoop:
                        stateStack.Push(GameState.GameLoop);
                        UpdateGame();
                        DrawGameToTexture();
                        break;
                    case GameState.CharacterCreation:
                        
                        DrawCharacterMenu(x + Raylib.GetScreenWidth()/2 - width/2, y, width);
                        break;
                    case GameState.OptionsMenu:
    
                        myOptionsMenu.DrawMenu(x + Raylib.GetScreenWidth() / 2 - width / 2, y, width);
                            break;
                    case GameState.PauseMenu:
                       
                        myPauseMenu.DrawMenu(x + Raylib.GetScreenWidth() / 2 - width / 2, y, width);
                        break;  

                    default:
                        break;
                }
                
            }
        }
    }
}
