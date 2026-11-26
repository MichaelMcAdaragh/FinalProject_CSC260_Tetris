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
            PiecePos[0].x = 3; PiecePos[0].y = 1;
            PiecePos[1].x = 4; PiecePos[1].y = 1;
            PiecePos[2].x = 5; PiecePos[2].y = 1;
            PiecePos[3].x = 6; PiecePos[3].y = 1;

            // set pivot coordinates:
            PiecePivot.x = 4.5; PiecePivot.y = 0.5;

            // set piece color
            PieceColor = CellColor.Cyan;

            // activate piece cells
            GameUtilities.FillPieceCells(matrix, PiecePos, PieceColor);
        }
    }
}
    

