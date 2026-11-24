using System;
using System.Collections.Generic;
using System.Text;

namespace FinalProject_CSC260_Tetris
{
    internal class S_Piece : TetrominoPiece
    {
        // S_Piece Constructor
        public S_Piece(CellColor[,] matrix)
        {
            // set inital coordinates:
            CellPos[0].x = 4; CellPos[0].y = 0;
            CellPos[1].x = 5; CellPos[1].y = 0;
            CellPos[2].x = 3; CellPos[2].y = 1;
            CellPos[3].x = 4; CellPos[3].y = 1;

            // set pivot coordinates:
            PiecePivot.x = 4; PiecePivot.y = 1;

            // set piece color
            PieceColor = CellColor.Green;

            // activate piece cells
            FillPieceCells(matrix, PieceColor);
        }
    }
}
