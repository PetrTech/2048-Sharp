using System;
using Raylib_cs;

namespace _2048
{
    class Program
    {
        public Board gameBoard = new Board(new int[4] { 0, 0, 0, 0 }, new int[4] { 0, 0, 0, 0 }, new int[4] { 0, 0, 0, 0 }, new int[4] { 0, 0, 0, 0 });

        public void NewGame()
        {
            Settings settings = new Settings();

            for (int i = 0; i < settings.spawnedTiles; i++)
            {
                GenerateTile();
            }
        }

        public void GenerateTile()
        {
            Random rand = new Random();
            int pickedRow = rand.Next(0, 3);
            int pickedCol = rand.Next(0, 3);

            int tile = rand.Next(0,1);

            if(tile == 1)
            {
                tile = 4;
            }
            else
            {
                tile = 2;
            }

            switch (pickedRow)
            {
                case 0:
                    if(gameBoard.firstLine[pickedCol+1] == 0)
                    {
                        gameBoard.firstLine[pickedCol+1] = tile;
                    }
                    else
                    {
                        GenerateTile();
                    }
                    break;
                case 1:
                    if (gameBoard.secondLine[pickedCol+1] == 0)
                    {
                        gameBoard.secondLine[pickedCol+1] = tile;
                    }
                    else
                    {
                        GenerateTile();
                    }
                    break;
                case 2:
                    if (gameBoard.thirdLine[pickedCol+1] == 0)
                    {
                        gameBoard.thirdLine[pickedCol+1] = tile;
                    }
                    else
                    {
                        GenerateTile();
                    }
                    break;
                case 3:
                    if (gameBoard.fourthLine[pickedCol+1] == 0)
                    {
                        gameBoard.fourthLine[pickedCol+1] = tile;
                    }
                    else
                    {
                        GenerateTile();
                    }
                    break;
            }
        }

        static void Main(string[] args)
        {
            new Program().Render();
        }

        public void Render()
        {
            // Setup Window
            Raylib.InitWindow(1280, 720, "Raylib-cs -=- 2048");
            Raylib.InitAudioDevice();

            Raylib.SetTargetFPS(120);

            NewGame();

            // Load Assets
            Texture2D bg = Raylib.LoadTexture("Background.png");
            Settings settings = new Settings();

            while (!Raylib.WindowShouldClose())
            {
                if (Raylib.IsKeyPressed(KeyboardKey.KEY_F11))
                {
                    Raylib.ToggleFullscreen();
                }

                if (Raylib.IsKeyPressed(KeyboardKey.KEY_R))
                {
                    new Program().NewGame();
                }

                #region gameKeys
                if (Raylib.IsKeyPressed(KeyboardKey.KEY_W) || Raylib.IsKeyPressed(KeyboardKey.KEY_UP))
                {
                    Compress(new Direction().up());
                }
                if (Raylib.IsKeyPressed(KeyboardKey.KEY_A) || Raylib.IsKeyPressed(KeyboardKey.KEY_LEFT))
                {
                    Compress(new Direction().left());
                }
                if (Raylib.IsKeyPressed(KeyboardKey.KEY_D) || Raylib.IsKeyPressed(KeyboardKey.KEY_RIGHT))
                {
                    Compress(new Direction().right());
                }

                if (Raylib.IsKeyPressed(KeyboardKey.KEY_S) || Raylib.IsKeyPressed(KeyboardKey.KEY_DOWN))
                {
                    Compress(new Direction().down());
                }
                #endregion

                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.BLACK);

                // Background and decorations
                Raylib.DrawTexture(bg, 0, 0, Color.WHITE);

                // Game Base
                int dimensions = ((settings.tileSize + settings.padding) * settings.tileAmount) + settings.padding * 2;
                Raylib.DrawRectangle((1280 / 2) - (dimensions / 2), 185, dimensions, dimensions, Raylib.Fade(Color.BLACK, 0.2f));

                // Empty Number Squares
                for (int x = 0; x < settings.tileAmount; x++)
                {
                    for (int y = 0; y < settings.tileAmount; y++)
                    {
                        int positionX = (settings.padding + settings.tileSize) * x;
                        int positionY = (settings.padding + settings.tileSize) * y;
                        Raylib.DrawRectangle(positionX + (1280 / 2) - (dimensions / 2) + (int)(settings.padding * 1.5f) - 1, positionY + (720 / 2) - (dimensions / 2) + settings.padding + 90, settings.tileSize, settings.tileSize, Raylib.Fade(Color.BLACK, 0.3f));
                    }
                }

                // Render Tiles
                for (int x = 0; x < 4; x++)
                {
                    for (int y = 0; y < 4; y++)
                    {
                        Color col = Raylib.Fade(Color.MAGENTA,0.6f);
                        int tileVal = 0;

                        switch (y)
                        {
                            case 1:
                                tileVal = gameBoard.firstLine[x];
                                break;
                            case 2:
                                tileVal = gameBoard.secondLine[x];
                                break;
                            case 3:
                                tileVal = gameBoard.thirdLine[x];
                                break;
                            case 4:
                                tileVal = gameBoard.fourthLine[x];
                                break;
                        }

                        // dont forget to collapse
                        switch (tileVal)
                        {
                            case 2:
                                col = Raylib.Fade(Color.BLACK, 0.4f);
                                break;
                            case 4:
                                col = new Color(34, 34, 34, 40);
                                break;
                            case 8:
                                col = new Color(49, 0, 88, 40);
                                break;
                            case 16:
                                col = new Color(55, 0, 98, 255);
                                break;
                            case 32:
                                col = new Color(93, 0, 167, 255);
                                break;
                            case 64:
                                col = new Color(143, 0, 255, 255);
                                break;
                            case 128:
                                col = new Color(185, 0, 215, 255);
                                break;
                            case 256:
                                col = new Color(251, 33, 255, 255);
                                break;
                            case 512:
                                col = new Color(255, 95, 210, 255);
                                break;
                            case 1024:
                                col = new Color(255, 0, 184, 255);
                                break;
                            case 2048:
                                col = new Color(255, 0, 122, 255);
                                break;
                            case 4096:
                                col = new Color(255, 0, 77, 255);
                                break;
                            case 8192:
                                col = new Color(204, 0, 0, 255);
                                break;
                            case 16384:
                                col = new Color(219, 61, 26, 255);
                                break;
                            case 32768:
                                col = new Color(228, 125, 30, 255);
                                break;
                            case 65536:
                                col = new Color(255, 184, 0, 255);
                                break;
                            case 131072:
                                col = new Color(0, 159, 194, 255);
                                break;
                        }

                        if(tileVal != 0)
                        {
                            int positionX = (settings.padding + settings.tileSize) * x;
                            int positionY = (settings.padding + settings.tileSize) * y;

                            int renderX = positionX + (1280 / 2) - (dimensions / 2) + (int)(settings.padding * 1.5f) - 1;
                            int renderY = positionY + (720 / 2) - (dimensions / 2) + settings.padding + 90;

                            Raylib.DrawRectangle(renderX, renderY, settings.tileSize, settings.tileSize, col);

                            float w = Raylib.MeasureTextEx(Raylib.GetFontDefault(), tileVal.ToString(), settings.tileSize/3, 10).X;
                            float h = Raylib.MeasureTextEx(Raylib.GetFontDefault(), tileVal.ToString(), settings.tileSize / 3, 10).Y;
                            Raylib.DrawText(tileVal.ToString(), (int)w*2 + renderX + settings.tileSize/10, (int)h + renderY, settings.tileSize/3, Color.WHITE);
                        }
                    }
                }

                Raylib.EndDrawing();
            }

            Raylib.CloseAudioDevice();
            Raylib.CloseWindow();
        }

        public void Compress(int direction)
        {
            switch (direction)
            {
                case 0:
                    foreach (int tile in gameBoard.firstLine)
                    {
                        if (gameBoard.firstLine[tile - 1] == 0)
                        {

                        }
                    }
                    break;
            }
        }

        public void Merge()
        {

        }
    }

    class Direction
    {
        public int left()
        {
            return 0;
        }

        public int right()
        {
            return 1;
        }

        public int up()
        {
            return 2;
        }

        public int down()
        {
            return 3;
        }
    }

    class Settings
    {
        /* Summary: Amount of tiles rendered, game doesn't accustom to those however */
        public int tileAmount = 4;
        /* Summary: Pixel size of each tile */
        public int tileSize = 115;


        /* Summary: Ammount of spawned tiles */
        public int spawnedTiles = 2;


        /* Summary: Tile in-between space*/
        public int padding = 10;

        /* Summary: Chance of spawning a 2 */
        public int twoChance = 90;
        /* Summary: Chance of spawning a 4 */
        public int fourChance = 10;

        /* Summary: Chance of spawning an 8 */
        public int eightChance = 0;
        /* Summary: Chance of spawning a 16 */
        public int sixteenChance = 0;
    }
    
    class Board
    {
        public int[] firstLine, secondLine, thirdLine, fourthLine = new int[4];

        public Board(int[] first, int[] second, int[] third, int[] fourth)
        {
            firstLine = first;
            secondLine = second;
            thirdLine = third;
            fourthLine = fourth;
        }
    }
}
