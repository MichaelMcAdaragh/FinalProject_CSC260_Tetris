using Raylib_cs;    // Added to project via Visual Studio Communities Manage Nuget Feature. I Chose top result for raylib-cs.

namespace FinalProject_CSC260_Tetris.Main           // Credit Claude on the basics of the Raylib Game Loop.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Raylib.InitWindow(GameSettings.WindowWidth, GameSettings.WindowHeight, "Tetris");   // This creates the window.
            Raylib.SetTargetFPS(60);    // I assume this limits to 60 Frames per second.

            Game game = new Game();

            while (!Raylib.WindowShouldClose())                                                 // while user does not close the window
            {
                Raylib.BeginDrawing();                                                          // Start Drawing
                Raylib.ClearBackground(Color.Black);
                ///////////////////////////////////////
                




                for (int row = 0; row < GameSettings.Height; row++)                             // nested for loop is same as terminal print out
                {
                    for (int col = 0; col < GameSettings.Width; col++)                          // but pixel math instead of ASCII characters.
                    {
                        Raylib.DrawRectangleLines(                                              // draws a gray rectangle around each cell.
                            col * GameSettings.CellSize,                                        // calculates current pos X in pixels
                            row * GameSettings.CellSize,                                        // calculates current pos Y in pixels
                            GameSettings.CellSize,                                              // rectangle width in pixels
                            GameSettings.CellSize,                                              // rectangle height in pixels
                            Color.DarkGray);                                                    // rectangle color DarkGray
                    }                
                }




                ///////////////////
                Raylib.EndDrawing();                                                           // End Drawing
            }
            Raylib.CloseWindow();                                                              // This terminates the window           
        }
    }
}
