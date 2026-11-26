using System;
using System.Collections.Generic;
using System.Text;

namespace FinalProject_CSC260_Tetris
{
    public static class GameUtilities
    {
        public static void FillPieceCells(CellColor[,] matrix, CellCoords[] piecePos, CellColor color)
        {
            for (int i = 0; i < 4; i++)
            {
                matrix[piecePos[i].x, piecePos[i].y] = color; // set current cells to given cell color
            }
        }

        // protected void ClearCells(CellColor[,] matrix, CellCoords[] piece)
        public static void ClearPieceCells(CellColor[,] matrix, CellCoords[] piece)
        {
            FillPieceCells(matrix, piece, CellColor.Empty);   // turn off current cells
        }

        // private bool CellsAreInBounds(int width, int height)
        // test if cells are in bounds
        public static bool CellsAreInBounds(int x_pos, int y_pos)    // Takes the width and height of an array.
        {
            bool inBounds = true;

            if (x_pos < 0 || x_pos >= GameSettings.Width)
                inBounds = false;

            if (y_pos < 0 || y_pos >= GameSettings.Height)
                inBounds = false;

            return inBounds;
        }

        // private bool CellsAreEmpty(CellColor[,] matrix)
        // test if cells are empty
        public static bool CellsAreEmpty(CellColor[,] matrix, int x_pos, int y_pos)    // used by class Game method int _rowsCompleted(). 
        {
            if (matrix[x_pos, y_pos] > 0)           // All Cellcolor Enums other than empty have values greater than zero.
                return false;                       // Thus if value stored at specific Tetrisboard cell is > 0 then it is !empty

            else
                return true;
        }

        // private bool MoveIsAllowed(CellColor[,] matrix, int width, int height)
        // return true or false if area piece is attempting to move to is both in inbound and empty.
        public static bool MoveIsAllowed(CellColor[,] matrix, int x_pos, int y_pos)
        {
            bool isAllowed = true;

            if (!CellsAreInBounds(x_pos, y_pos))
                isAllowed = false;       // if cells are out of bounds return false.

            else if (!CellsAreEmpty(matrix, x_pos, y_pos))  // _cellsAreEmpty() requires a bounds check so that it doesn't attempt to read an out of bounds location from the game matrix.
                isAllowed = false;   // if cells were in bounds, check if their position is already occupied.

            else                    // both checks passed. _moveIsAllowed() returns true.
                isAllowed = true;

            return isAllowed;
        }
    }
}
