using System;
using System.Collections.Generic;
using System.Text;

namespace FinalProject_CSC260_Tetris
{
    internal class I_Piece : TetrominoPiece
    {
        // I_Piece Constructor
        public I_Piece(CellColor[,] matrix)
        {
            // set inital coordinates:
            CellPos[0].x = 3; CellPos[0].y = 0;
            CellPos[1].x = 4; CellPos[1].y = 0;
            CellPos[2].x = 5; CellPos[2].y = 0;
            CellPos[3].x = 6; CellPos[3].y = 0;

            // set pivot coordinates:
            PiecePivot.x = 4.5; PiecePivot.y = 0.5;

            // set piece color
            PieceColor = CellColor.Cyan;

            // activate piece cells
            FillPieceCells(matrix, PieceColor);
        }
    }
}
    

