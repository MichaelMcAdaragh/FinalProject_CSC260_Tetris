using System;
using System.Collections.Generic;
using System.Text;

namespace FinalProject_CSC260_Tetris
{
    abstract class TetrominoPiece
    {
        // TetrominoPiece Fields:
        protected CellCoords[] CellPos = new CellCoords[4]; // Array of xy int coord structs that represents each of the piece's cell positions.
        protected PivotCoords PiecePivot;  // xy double coord struct that represents the exact radial pivot of the piece's rotation.
        protected CellColor PieceColor;    // stores the enumerated cell color of the piece.
        protected RotationState PieceRotationState = RotationState.Deg000; // store rotation state. initialized at zero degrees.

        // TetrominoPiece Properties



        // TetrominoPiece Base Constructor???



        // TetrominoPiece METHODS:

        // PIECE MOVEMENT AND MANIPULATION //


        // protected void FillCells(CellColor[,] matrix, Cell Coords[] piece)
        // fill current tetromino cells with a desired color.
        protected void FillPieceCells(CellColor[,] matrix, CellColor color)
        {
            for (int i = 0; i < 4; i++)
            {
                matrix[CellPos[i].x, CellPos[i].y] = color; // set current cells to given cell color
            }
        }

        // protected void ClearCells(CellColor[,] matrix, CellCoords[] piece)
        // empty current tetromino cells.
        protected void ClearPieceCells(CellColor[,] matrix)
        {
                FillPieceCells(matrix, CellColor.Empty);   // turn off current cells
        }

        // protected void ShiftPos(Direction direction)
        // move tetromino left, right, and down.
        protected void ShiftPiecePos(CellColor[,] matrix, int change_x, int change_y)  // change in x: -- = left, ++ = right. change in y: ++ = down, -- = N/A.
        {
            ClearCells(matrix);              // empty Current Cells 

            for (int i = 0; i < 4; i++)      // for each tetramino cell
            {
                CellPos[i].x += change_x;    // apply change in x
                CellPos[i].y += change_y;    // apply change in y
            }
            PiecePivot.x += change_x;        // update the x-y pos of pivot
            PiecePivot.y += change_y;

            FillCells(matrix, PieceColor);   // fill cells In at New Position.
        }

        // public void Move(CellColor[,] matrix, Direction direction)
        // Move tetromino left, right, or down

        public void Move(CellColor[,] matrix, Direction direction)      // MOVE FUNCTION
        {
            switch (direction)
            {
                case Direction.Left:                        // LEFT                  
                    ShiftPiecePos(matrix, -1, 0); // (x--, y + 0)
                    break;

                case Direction.Right:                       // RIGHT
                    ShiftPiecePos(matrix, 1, 0); // (x++, y + 0)
                    break;

                case Direction.Down:                        // DOWN
                    ShiftPiecePos(matrix, 0, 1); // (x + 0, y++)
                    break;

                default:    // else do nothing
                    break;
            }
        }

        // public virtual void Move(CellColor[,] matrix, RotationalDirection rotationalDirection)
        // Rotate tetromino clockwise or counterclockwise.
        public void Move(CellColor[,] matrix, RotationalDirection rotationalDirection)
        {
            ClearCells(matrix);                                            // Clear Piece From Game Board Matrix.

            for (int i = 0; i < 4; i++)                  
            {   // Trig magic. sin and cos are invisible since they cancel out to 1's and 0's at 90 deg spots on unit circle.
                double X_offsetFromPivot = CellPos[i].x - PiecePivot.x;    // find blocks's X offset from Pivot.
                double Y_offsetFromPivot = CellPos[i].y - PiecePivot.y;    // find block's Y offset from Pivot.

                switch (rotationalDirection)                                       // NEED TO DRAW OUT BY HAND!!!
                {
                    case RotationalDirection.Clockwise:
                        CellPos[i].x = (int)(PiecePivot.x - Y_offsetFromPivot);    // Update Location of block's X 90 degrees clockwise.
                        CellPos[i].y = (int)(PiecePivot.y + X_offsetFromPivot);    // Update Location of block's Y 90 degrees clockwise. 
                        break;

                    case RotationalDirection.CounterClockwise:                      // Flipping the signs on these two lines results in CounterClockWise rotation.
                        CellPos[i].x = (int)(PiecePivot.x + Y_offsetFromPivot);     // Update Location of block's X 90 degrees counter clockwise.
                        CellPos[i].y = (int)(PiecePivot.y - X_offsetFromPivot);     // Update Location of block's Y 90 degrees counter clockwise.
                        break;

                    default:    // else do nothing
                        break;
                }
            }
            FillCells(matrix, PieceColor);                // "Draw" Piece's New Location On Game Board Matrix.
        }

        // protected void Kick(CellColor[,] matrix, RotationState rotationState)
        // implement kick table offsets. (Possible a challenging goal.)




        // BOUNDS CHECKING & COLLISION //

        // protected bool CellsAreEmpty(CellColor[,] matrix)
        // test if cells are empty

        // protected bool CellsAreInBounds(int width, int height)
        // test if cells are in bounds

        // protected bool MoveIsValid(CellColor[,] matrix, int width, int height)
        // test if area piece is attempting to move to in inbound and empty.

    }
}
