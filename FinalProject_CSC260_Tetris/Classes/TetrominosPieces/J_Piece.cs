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
            PiecePos[0].x = 3; PiecePos[0].y = 0;
            PiecePos[1].x = 3; PiecePos[1].y = 1;
            PiecePos[2].x = 4; PiecePos[2].y = 1;
            PiecePos[3].x = 5; PiecePos[3].y = 1;

            // set pivot coordinates:
            PiecePivot.x = 4; PiecePivot.y = 1;

            // set piece color
            PieceColor = CellColor.Blue;

            // activate piece cells
            GameUtilities.FillPieceCells(matrix, PiecePos, PieceColor);
        }
    }
}
