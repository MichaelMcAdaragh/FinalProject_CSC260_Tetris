using System;
using System.Collections.Generic;
using System.Text;

namespace FinalProject_CSC260_Tetris
{
    abstract class TetrominoPiece
    {
        // TetrominoPiece Fields:
        protected CellCoords[] PiecePos = new CellCoords[4]; // Array of xy int coord structs that represents each of the piece's cell positions.
        protected PivotCoords PiecePivot;  // xy double coord struct that represents the exact radial pivot of the piece's rotation.
        protected CellColor PieceColor;    // stores the enumerated cell color of the piece.
        protected RotationState PieceRotationState = RotationState.Deg000; // store rotation state. initialized at zero degrees.



        // TetrominoPiece Properties



        // TetrominoPiece Base Constructor???



        // TetrominoPiece Methods



    }
}
