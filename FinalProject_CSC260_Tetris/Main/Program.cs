using Raylib_cs;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;    // Added to project via Visual Studio Communities Manage Nuget Feature. I Chose top result for raylib-cs.

namespace FinalProject_CSC260_Tetris.Main           // Credit Claude on the basics of the Raylib Game Loop.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Raylib.InitWindow(GameSettings.WindowWidth, GameSettings.WindowHeight, "Tetris");   // This creates the window.
            Raylib.SetTargetFPS(60);    // I assume this limits to 60 Frames per second.

            Game game = new Game();

            while (!Raylib.WindowShouldClose())                                                 // while user attempt close window == false.
            {
                // game.HandleInput();                                                          // (a key down moves piece)
                // game.Update(Raylib.GetFrameTime());                                          // ("the timer ticks" / drop piece)

                Raylib.BeginDrawing();                                                          // Start Drawing.
                Raylib.ClearBackground(Color.Black);
                /////////////////////////////////////////////////////////////////////////////////////////////


                game.DisplayGameBoard(game.TetrisGameBoard);                                    // Draw Current Game State.

        
                /////////////////////////////////////////////////////////////////////////////////////////////
                Raylib.EndDrawing();                                                            // End Drawing.
            }
            Raylib.CloseWindow();                                                               // Terminate the window.           
        }
    }
}
