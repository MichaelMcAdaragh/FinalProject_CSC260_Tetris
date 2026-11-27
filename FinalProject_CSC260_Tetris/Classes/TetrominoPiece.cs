using System;
using System.Collections.Generic;
using System.Text;

namespace FinalProject_CSC260_Tetris
{
    abstract class TetrominoPiece
    {
        // TetrominoPiece Fields & Properties:
        protected CellCoords[] PiecePos = new CellCoords[4]; // Array of xy int coord structs that represents each of the piece's cell positions.
        protected PivotCoords PiecePivot;  // xy double coord struct that represents the exact radial pivot of the piece's rotation.
        public CellColor PieceColor { get; protected set; }    // stores the enumerated cell color of the piece.
        protected RotationState PieceRotationState = RotationState.Deg000; // store rotation state. initialized at zero degrees.

        // TetrominoPiece METHODS:
        // PIECE MOVEMENT AND MANIPULATION //

        // protected void FillCells(CellColor[,] matrix, Cell Coords[] piece)
        public static void FillPieceCells(CellColor[,] matrix, CellCoords[] piecePos, CellColor color)
        {
            for (int i = 0; i < 4; i++)
            {
                matrix[piecePos[i].x, piecePos[i].y] = color; // set current cells to given cell color
            }
        }

        // protected void ClearCells(CellColor[,] matrix, CellCoords[] piece)
        public static void ClearPieceCells(CellColor[,] matrix, CellCoords[] piece)
        {
            FillPieceCells(matrix, piece, CellColor.Empty);   // turn off current cells
        }

        // protected void ShiftPos(Direction direction)
        // move tetromino left, right, and down. returns status of its success as a bool.
        private bool _pieceShiftedSuccessfully(CellColor[,] matrix, int change_x, int change_y)  // change in x: -- = left, ++ = right. change in y: ++ = down, -- = N/A.
        {
            if (PieceCanMove(matrix, change_x, change_y))  // Disallows illegal position shifts
            {
                ClearPieceCells(matrix, PiecePos);              // empty Current Cells 

                for (int i = 0; i < 4; i++)      // for each tetramino cell
                {
                    PiecePos[i].x += change_x;    // apply change in x
                    PiecePos[i].y += change_y;    // apply change in y
                }

                PiecePivot.x += change_x;        // since move was allowed, update the x-y pos of pivot to reflect the new positions of the piece's cells
                PiecePivot.y += change_y;

                FillPieceCells(matrix, PiecePos, PieceColor);  // fill cells In at New Position.

                return true;
            }
            else
                return false;
        }

        // public void Move(CellColor[,] matrix, Direction direction)
        // Move tetromino left, right, or down

        public bool Move(CellColor[,] matrix, Direction direction)      // MOVE FUNCTION
        {
            switch (direction)
            {
                case Direction.Left:                        // LEFT                  
                    return _pieceShiftedSuccessfully(matrix, -1, 0); // (x--, y + 0)

                case Direction.Right:                       // RIGHT
                    return _pieceShiftedSuccessfully(matrix, 1, 0); // (x++, y + 0);

                case Direction.Down:                        // DOWN
                    return _pieceShiftedSuccessfully(matrix, 0, 1); // (x + 0, y++)

                default:
                    return false;
            }
        }



        // protected void Kick(CellColor[,] matrix, RotationState rotationState) !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        // implement kick table offsets. (Possible a challenging goal.) !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!



        // private void _rotatedSuccessfully(CellColor[,] matrix, RotationalDirection direction)
        // attempts to apply rotation and returns if it was successful or not.
        private bool _rotatedSuccessfully(CellColor[,] matrix, RotationalDirection direction)
        {
            bool rotatedSuccessfully = false;

            for (int i = 0; i < 4; i++)
            {
                double x_pivotOffset = PiecePos[i].x - PiecePivot.x;    // find individual cell's X offset from Pivot.
                double y_pivotOffset = PiecePos[i].y - PiecePivot.y;    // find individual cell's Y offset from Pivot.
                
                switch (direction)
                {
                    case RotationalDirection.Clockwise:
                        if (_pieceCanRotate(matrix, RotationalDirection.Clockwise))  // make sure rotate does not cause out of bound or collision.
                        {
                            ClearPieceCells(matrix, PiecePos);                            

                            PiecePos[i].x = (int)(PiecePivot.x - y_pivotOffset);    // Update Location of block's X 90 degrees clockwise.
                            PiecePos[i].y = (int)(PiecePivot.y + x_pivotOffset);    // Update Location of block's Y 90 degrees clockwise.

                            FillPieceCells(matrix, PiecePos, PieceColor);
                            rotatedSuccessfully = true;                            
                        }
                        break;

                    case RotationalDirection.CounterClockwise:
                        if (_pieceCanRotate(matrix, RotationalDirection.CounterClockwise))
                        {
                            ClearPieceCells(matrix, PiecePos);                  

                            PiecePos[i].x = (int)(PiecePivot.x + y_pivotOffset);     // Update Location of block's X 90 degrees counter clockwise.
                            PiecePos[i].y = (int)(PiecePivot.y - x_pivotOffset);     // Update Location of block's Y 90 degrees counter clockwise.

                            FillPieceCells(matrix, PiecePos, PieceColor);
                            rotatedSuccessfully = true;
                        }
                        break;
                }                
            }
            return rotatedSuccessfully;
        }

        // public virtual void Move(CellColor[,] matrix, RotationalDirection rotationalDirection)
        // Rotate tetromino clockwise or counterclockwise.
        public bool Move(CellColor[,] matrix, RotationalDirection direction)
        {
            switch (direction)                                       // NEED TO DRAW OUT BY HAND!!!
            {
                case RotationalDirection.Clockwise:
                    return _rotatedSuccessfully(matrix, RotationalDirection.Clockwise);

                case RotationalDirection.CounterClockwise:
                    return _rotatedSuccessfully(matrix, RotationalDirection.CounterClockwise);

                default:
                    return false;
            }
        }

        // BOUNDS CHECKING & COLLISION //
        
        public bool PieceCanMove(CellColor[,] matrix, int change_x, int change_y)    // Used by game loop to determine between droptime and locktime.
        {
            ClearPieceCells(matrix, PiecePos);

            bool isAllowed = true;

            for (int i = 0; i < 4; i++)      // for each tetramino cell
            {
                int testX = PiecePos[i].x + change_x;    // test change in x
                int testY = PiecePos[i].y + change_y;    // test change in y

                if (!GameUtilities.CellCanMove(matrix, testX, testY))
                    isAllowed = false;       // Check if the move was allowed
            }

            FillPieceCells(matrix, PiecePos, PieceColor);

            return isAllowed;
        }

        // BOOL CANROTATE() !!!
        // Simulates a rotation and determines whether or not it was in bounds
        private bool _pieceCanRotate(CellColor[,] matrix, RotationalDirection direction)
        {
            ClearPieceCells(matrix, PiecePos);                                            // Clear Piece From Game Board Matrix.

            bool isAllowed = true;

            for (int i = 0; i < 4; i++)
            {
                CellCoords testPiecePos; // test coordinates

                double X_offsetFromPivot = PiecePos[i].x - PiecePivot.x;         // find blocks's X offset from Pivot.
                double Y_offsetFromPivot = PiecePos[i].y - PiecePivot.y;         // find block's Y offset from Pivot.

                if (direction == RotationalDirection.Clockwise)
                {
                    testPiecePos.x = (int)(PiecePivot.x - Y_offsetFromPivot);    // Record test location of each cells's X 90 degrees clockwise.
                    testPiecePos.y = (int)(PiecePivot.y + X_offsetFromPivot);    // Record test location of each cells's Y 90 degrees clockwise.

                    if (!GameUtilities.CellCanMove(matrix, testPiecePos.x, testPiecePos.y))  // Check if the move was allowed
                        isAllowed = false;
                }
                else if (direction == RotationalDirection.CounterClockwise)     // Flipping the signs on these two lines results in CounterClockWise rotation.
                {
                    testPiecePos.x = (int)(PiecePivot.x + Y_offsetFromPivot);    // Record test location of each cells's X 90 degrees counterclockwise.
                    testPiecePos.y = (int)(PiecePivot.y - X_offsetFromPivot);    // Record test location of each cells's Y 90 degrees counterclockwise.

                    if (!GameUtilities.CellCanMove(matrix, testPiecePos.x, testPiecePos.y))
                        isAllowed = false;
                }
            }
            FillPieceCells(matrix, PiecePos, PieceColor);                // "Draw" Piece's New Location On Game Board Matrix.
            return isAllowed;
        }
    }
}

