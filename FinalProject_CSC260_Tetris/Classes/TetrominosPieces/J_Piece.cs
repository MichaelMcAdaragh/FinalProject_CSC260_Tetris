using System;
using System.Collections.Generic;
using System.Text;

namespace FinalProject_CSC260_Tetris
{
    internal class J_Piece : TetrominoPiece
    {
        // J_Piece Constructor
        public J_Piece(CellColor[,] matrix)
        {
            // set inital coordinates:
            PiecePos[0].X = 3; PiecePos[0].Y = 0;
            PiecePos[1].X = 3; PiecePos[1].Y = 1;
            PiecePos[2].X = 4; PiecePos[2].Y = 1;
            PiecePos[3].X = 5; PiecePos[3].Y = 1;

            // set pivot coordinates:
            PiecePivot.X = 4; PiecePivot.Y = 1;

            // set piece color
            PieceColor = CellColor.Blue;

            // activate piece cells
            FillPieceCells(matrix, PiecePos, PieceColor);
        }
    }
}
