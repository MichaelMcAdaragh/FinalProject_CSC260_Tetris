using System;
using System.Collections.Generic;
using System.Text;

namespace FinalProject_CSC260_Tetris
{
    static class GameSettings
    {                                           // Auto-Property makes data read only,
        // GameSettings Properties:
        public static int Width { get; } = 10;         // game board initialized at 10 cells wide.
        public static int Height { get; } = 20;        // game board initialized to 20 cells high.
        public static int WindowWidth { get; } = 400;  // pixels. (!!!possible later upgrade to window size options.!!!)
        public static int WindowHeight { get; } = 800; // pixels.
        public static int CellSize { get; } = WindowWidth / Width; // Cellsize is dynamically calculated within the class,
    }                                                              // but is read-only from outside it
}
