using System;
using Raylib_cs;

namespace _2048
{
    class Program
    {
        static void Main(string[] args)
        {
            // Setup Window
            Raylib.InitWindow(1280, 720, "Raylib-cs -=- 2048");
            Raylib.InitAudioDevice();

            Raylib.SetTargetFPS(120);

            // Load Assets
            Texture2D bg = Raylib.LoadTexture("Background.png");
            Settings settings = new Settings();

            while (!Raylib.WindowShouldClose())
            {
                if (Raylib.IsKeyPressed(KeyboardKey.KEY_F11))
                {
                    Raylib.ToggleFullscreen();
                }

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
                        Raylib.DrawRectangle(positionX, positionY, settings.tileSize, settings.tileSize, Raylib.Fade(Color.BLACK, 0.3f));
                    }
                }

                Raylib.EndDrawing();
            }

            Raylib.CloseAudioDevice();
            Raylib.CloseWindow();
        }
    }

    class Settings
    {
        /* Summary: Amount of tiles rendered */
        public int tileAmount = 4;
        /* Summary: Pixel size of each tile */
        public int tileSize = 115;

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
}
