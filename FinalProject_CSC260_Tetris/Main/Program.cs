// Michael McAdaragh 11/27/26
// CSC 260 Object Orientated Design
// Proffesor Nicholas Handberg
// Final Project: Tetris

//////////////////////////////////////////////////////////////////////////////////

// CREDITS AND ATTRIBUTIONS:

// Human Collaborators:

// Alexey Pajitov:
// Thanks to the father of Tetris(1984) for developing the Tetris game concept.

// https://tetris.wiki:
// Tetris.wiki was used to obtain Kick Offset Tables and to review Scoring Systems.

// Ramon Santamaria and Raylib Community:
// Developers of Raylib. Used for GUI and user input.

// Limited AI use disclosure:

// Code obtained from AI:
// Claude was used to provide Raylib code snippets and help with the standard layout of the game loop.
// It was used to obtain Raylib Methods: Raylib.InitWindow(), Raylib.SetTargetFPS(), Raylib.BeginDrawing, Raylib.Draw.Rectangle() ect.
// It was used to templete out the operational order of a Raylib game loop.
// It was used to templete out an accumulator timer working off of frames per second to control when game loop operations occur.

// Concepts obtained from AI:
// Claude was used to obtain rotational concepts such as the pivot, the pivot offset, and the 90 degree case of the trigonometric rotation formula.
// Rotational Kick offsets were explained at a high level.

// All other code not specified was developed by me, Michael McAdaragh(2026).

//////////////////////////////////////////////////////////////////////////////////

// Added to project via Visual Studio Community's Manage Nuget Feature. I Chose top result for raylib-cs.
// Raylib is a free open source library that abstracts away connecting your code's logic to your hardware.
using Raylib_cs;    // As a beginner, I found it very useful for this project as well an amateur Conway's Game of Life.
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FinalProject_CSC260_Tetris.Main           // Credit Claude on the basics of the Raylib Game Loop.
{
    internal class Program
    {
        static void Main(string[] args)
        {   //////////////////////////////////////////////////////////////////////////////////////////////// RAYLIB METHODS - SETUP WINDOW AND RUN SPEED
            Raylib.InitWindow(GameSettings.WindowWidth + (2 * GameSettings.CellSize),
                GameSettings.WindowHeight + (4 * GameSettings.CellSize), "Tetris");   // This creates the window.
            Raylib.SetTargetFPS(60);                                                  // limits game loop to 60 Frames per second.
            //////////////////////////////////////////////////////////////////////////////////////////////// MY CODE / METHODS


            Game game = new Game();


            //////////////////////////////////////////////////////////////////////////////////////////////// RAYLIB METHODS / GAME LOOP SETUP
            while (!Raylib.WindowShouldClose())             // while user attempt to close window == false.
            {
            //////////////////////////////////////////////////////////////////////////////////////////////// MY CODE - HANDLE INPUT & HANDLE GAME LOOP EVENTS


                game.HandleUserInput();                     // (handles user inputs to move a piece)
                game.GameLoopTimer(Raylib.GetFrameTime());  // GetFrameTime() is currently 1/60 of a sec (Because thats the same as 60FPS)
                                                            // auto-drops or "locks" piece if no move is made by end of _timeAllowed


            /////////////////////////////////////////////////////////////////////////////////////////////// RAYLIB METHODS - ENABLE DRAWING METHODS
                Raylib.BeginDrawing();                                          // Start Drawing.
                Raylib.ClearBackground(Color.Black);
            /////////////////////////////////////////////////////////////////////////////////////////////// MY CODE - DISPLAY CURRENT STATE OF TETRISBOARD


                game.DisplayGameBoard();                                        // Draw Current Game State.

        
            /////////////////////////////////////////////////////////////////////////////////////////////// RAYLIB METHODS - DISABLE DRAWING METHODS
                Raylib.EndDrawing();                                            // End Drawing.             RAYLIB METHODS - CLOSE WINDOW
            }
            Raylib.CloseWindow();                                               // Terminate the window.           
        }   ///////////////////////////////////////////////////////////////////////////////////////////////
    }
}
