using Raylib_cs;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;    // Added to project via Visual Studio Communities Manage Nuget Feature. I Chose top result for raylib-cs.

namespace FinalProject_CSC260_Tetris.Main           // Credit Claude on the basics of the Raylib Game Loop.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Raylib.InitWindow(GameSettings.WindowWidth + (2 * GameSettings.CellSize),
                GameSettings.WindowHeight + (4 * GameSettings.CellSize),
                "Tetris");   // This creates the window. // +GameSttings.Cellsize creates an area for score and level. Next piece would be neat someday.
            Raylib.SetTargetFPS(60);                        // limits to 60 Frames per second.

            Game game = new Game();

            while (!Raylib.WindowShouldClose())             // while user attempt to close window == false.
            {
                game.HandleUserInput();                     // (handles user inputs to move a piece)
                game.GameLoopTimer(Raylib.GetFrameTime());      // GetFrameTime() is currently 1/60 of a sec (Because thats the same as 60FPS)
                                                            // auto-drops piece if no move is made by end of _moveTimeAllowed


                ////////////////////////////////////////////////////////////////////////////////////////////
                Raylib.BeginDrawing();                                          // Start Drawing.
                Raylib.ClearBackground(Color.Black);
                /////////////////////////////////////////////////////////////////////////////////////////////


                game.DisplayGameBoard();                    // Draw Current Game State.

        
                /////////////////////////////////////////////////////////////////////////////////////////////
                Raylib.EndDrawing();                                            // End Drawing.
            }
            Raylib.CloseWindow();                                               // Terminate the window.           
        }
    }
}
