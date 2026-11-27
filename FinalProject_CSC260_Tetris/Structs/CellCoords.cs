using System;
using System.Collections.Generic;
using System.Text;

namespace FinalProject_CSC260_Tetris
{
    public struct CellCoords
    {
        public int X;
        public int Y;

        public CellCoords(int x, int y)    // Added Struct Parameterized Constructor for Kick Table Offset Data Input.
        {
            X = x;
            Y = y;
        }
        public CellCoords(CellCoords cellcoords)    // Added Struct Copy Constructor for Kick Table Offset Data Input.
        {
            X = cellcoords.X;
            Y = cellcoords.Y;
        }
    }
}
