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
            CellPos[0].x = 6; CellPos[0].y = 0;
            CellPos[1].x = 4; CellPos[1].y = 1;
            CellPos[2].x = 5; CellPos[2].y = 1;
            CellPos[3].x = 6; CellPos[3].y = 1;

            // set pivot coordinates:
            PiecePivot.x = 4; PiecePivot.y = 1;

            // set piece color
            PieceColor = CellColor.Orange;

            // activate piece cells
            FillPieceCells(matrix, PieceColor);
        }
    }
}
