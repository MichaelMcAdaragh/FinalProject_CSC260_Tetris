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
            CellPos[0].x = 4; CellPos[0].y = 0;
            CellPos[1].x = 5; CellPos[1].y = 0;
            CellPos[2].x = 4; CellPos[2].y = 1;
            CellPos[3].x = 5; CellPos[3].y = 1;

            // set pivot coordinates:
            PiecePivot.x = 4.5; PiecePivot.y = 0.5;

            // set piece color
            PieceColor = CellColor.Yellow;

            // activate piece cells
            FillPieceCells(matrix, PieceColor);
        }
    }
}
