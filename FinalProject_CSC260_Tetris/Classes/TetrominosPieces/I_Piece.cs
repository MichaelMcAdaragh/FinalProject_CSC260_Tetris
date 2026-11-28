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
            PiecePos[0].X = 3; PiecePos[0].Y = 1;
            PiecePos[1].X = 4; PiecePos[1].Y = 1;
            PiecePos[2].X = 5; PiecePos[2].Y = 1;
            PiecePos[3].X = 6; PiecePos[3].Y = 1;

            // set pivot coordinates:
            PiecePivot.X = 4.5; PiecePivot.Y = 0.5;

            // set piece color
            PieceColor = CellColor.Cyan;

            // activate piece cells
            FillPieceCells(matrix, PiecePos, PieceColor);


        }
    }
}
    

