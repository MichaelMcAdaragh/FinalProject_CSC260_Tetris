using System;
using System.Collections.Generic;
using System.Text;

namespace FinalProject_CSC260_Tetris
{
    internal class Z_Piece : TetrominoPiece
    {
        // Z_Piece Constructor
        public Z_Piece(CellColor[,] matrix)
        {
            // set inital coordinates:
            PiecePos[0].x = 4; PiecePos[0].y = 0;
            PiecePos[1].x = 5; PiecePos[1].y = 0;
            PiecePos[2].x = 5; PiecePos[2].y = 1;
            PiecePos[3].x = 6; PiecePos[3].y = 1;

            // set pivot coordinates:
            PiecePivot.x = 5; PiecePivot.y = 1;

            // set piece color
            PieceColor = CellColor.Red;

            // activate piece cells
            GameUtilities.FillPieceCells(matrix, PiecePos, PieceColor);
        }
    }
}
