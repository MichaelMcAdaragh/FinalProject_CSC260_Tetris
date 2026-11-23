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
        
        // protected void EmptyCells(CellColor[,] matrix, CellCoords[] piece)
        // empty current tetromino cells.

        // protected void FillCells(CellColor[,] matrix, Cell Coords[] piece)
        // fill current tetromino cells.

        // protected void Move(Direction direction)
        // move tetromino left, right, and down.

        // protected void Rotate(RotationalDirection direction)
        // rotate tetromino clockwise and counterclockwise.

        // protected void Kick(CellColor[,] matrix, RotationState rotationState)
        // implement kick table offsets. (Possible a challenging goal.)

        // protected bool CellsAreEmpty(CellColor[,] matrix)
        // test if cells are empty

        // protected bool CellsAreInBounds(int width, int height)
        // test if cells are in bounds

        // protected bool MoveIsValid(CellColor[,] matrix, int width, int height)
        // test if area piece is attempting to move to in inbound and empty.

        // public void Move(CellColor[,] matrix, Direction direction)
        // Move tetromino left, right, or down

        // public virtual void Move(CellColor[,] matrix, RotationalDirection rotationalDirection)
        // Rotate tetromino clockwise or counterclockwise.
    }
}
