using System;
using System.Collections.Generic;
using System.Text;

namespace FinalProject_CSC260_Tetris
{
    internal class L_Piece : TetrominoPiece
    {
        // L_Piece Constructor
        public L_Piece(CellColor[,] matrix)
        {
            // set inital coordinates:
            PiecePos[0].x = 6; PiecePos[0].y = 0;
            PiecePos[1].x = 4; PiecePos[1].y = 1;
            PiecePos[2].x = 5; PiecePos[2].y = 1;
            PiecePos[3].x = 6; PiecePos[3].y = 1;

            // set pivot coordinates:
            PiecePivot.x = 5; PiecePivot.y = 1;

            // set piece color
            PieceColor = CellColor.Orange;

            // activate piece cells
            GameUtilities.FillPieceCells(matrix, PiecePos, PieceColor);
        }
    }
}
