using System;
using System.Collections.Generic;
using System.Text;

namespace FinalProject_CSC260_Tetris
{
    public struct PivotCoords
    {
        public double X;
        public double Y;

        public PivotCoords(int x, int y)    // Added Struct Parameterized Constructor for Kick Table Offset Data Input.
        {
            X = x;
            Y = y;
        }
    }
}
