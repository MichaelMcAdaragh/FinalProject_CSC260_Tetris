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
            CellPos[0].x = 3; CellPos[0].y = 0;
            CellPos[1].x = 3; CellPos[1].y = 1;
            CellPos[2].x = 4; CellPos[2].y = 1;
            CellPos[3].x = 5; CellPos[3].y = 1;

            // set pivot coordinates:
            PiecePivot.x = 4; PiecePivot.y = 1;

            // set piece color
            PieceColor = CellColor.Blue;

            // activate piece cells
            FillPieceCells(matrix, PieceColor);
        }
    }
}
