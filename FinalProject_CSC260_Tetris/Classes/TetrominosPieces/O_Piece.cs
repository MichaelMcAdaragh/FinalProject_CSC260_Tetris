using System;
using System.Collections.Generic;
using System.Text;

namespace FinalProject_CSC260_Tetris
{
    internal class O_Piece : TetrominoPiece
    {
        // O_Piece Constructor
        public O_Piece(CellColor[,] matrix)
        {
            // set inital coordinates:
            PiecePos[0].X = 4; PiecePos[0].Y = 0;
            PiecePos[1].X = 5; PiecePos[1].Y = 0;
            PiecePos[2].X = 4; PiecePos[2].Y = 1;
            PiecePos[3].X = 5; PiecePos[3].Y = 1;

            // set pivot coordinates:
            PiecePivot.X = 4.5; PiecePivot.Y = 0.5;

            // set piece color
            PieceColor = CellColor.Yellow;

            // activate piece cells
            FillPieceCells(matrix, PiecePos, PieceColor);
        }
    }
}
