using System;
using System.Collections.Generic;
using System.Text;

namespace FinalProject_CSC260_Tetris
{
    abstract class TetrominoPiece
    {
        // TetrominoPiece Fields & Properties:
        protected CellCoords[] CellPos = new CellCoords[4]; // Array of xy int coord structs that represents each of the piece's cell positions.
        protected PivotCoords PiecePivot;  // xy double coord struct that represents the exact radial pivot of the piece's rotation.
        public CellColor PieceColor { get; protected set; }    // stores the enumerated cell color of the piece.
        protected RotationState PieceRotationState = RotationState.Deg000; // store rotation state. initialized at zero degrees.

        // TetrominoPiece METHODS:
        // PIECE MOVEMENT AND MANIPULATION //

        // protected void FillCells(CellColor[,] matrix, Cell Coords[] piece)
        protected void FillPieceCells(CellColor[,] matrix, CellColor color)
        {
            for (int i = 0; i < 4; i++)
            {
                matrix[CellPos[i].x, CellPos[i].y] = color; // set current cells to given cell color
            }
        }

        // protected void ClearCells(CellColor[,] matrix, CellCoords[] piece)
        protected void ClearPieceCells(CellColor[,] matrix)
        {
            FillPieceCells(matrix, CellColor.Empty);   // turn off current cells
        }

        // protected void ShiftPos(Direction direction)
        // move tetromino left, right, and down. returns status of its success as a bool.
        private bool _pieceShiftedSuccessfully(CellColor[,] matrix, int change_x, int change_y)  // change in x: -- = left, ++ = right. change in y: ++ = down, -- = N/A.
        {
            if (PieceCanMove(matrix, change_x, change_y))  // Disallows illegal position shifts
            {
                ClearPieceCells(matrix);              // empty Current Cells 

                for (int i = 0; i < 4; i++)      // for each tetramino cell
                {
                    CellPos[i].x += change_x;    // apply change in x
                    CellPos[i].y += change_y;    // apply change in y
                }

                PiecePivot.x += change_x;        // since move was allowed, update the x-y pos of pivot to reflect the new positions of the piece's cells
                PiecePivot.y += change_y;

                FillPieceCells(matrix, PieceColor);  // fill cells In at New Position.

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

        // private void _rotateClockWise(double xOffset, yOffset)
        private bool _rotatedClockWiseSuccessfully(CellColor[,] matrix)
        {
            if (PieceCanRotate(matrix, RotationalDirection.Clockwise))
            {
                ClearPieceCells(matrix);

                for (int i = 0; i < 4; i++)
                {
                    double x_pivotOffset = CellPos[i].x - PiecePivot.x;    // find blocks's X offset from Pivot.
                    double y_pivotOffset = CellPos[i].y - PiecePivot.y;    // find block's Y offset from Pivot.

                    CellPos[i].x = (int)(PiecePivot.x - y_pivotOffset);    // Update Location of block's X 90 degrees clockwise.
                    CellPos[i].y = (int)(PiecePivot.y + x_pivotOffset);    // Update Location of block's Y 90 degrees clockwise.
                }

                FillPieceCells(matrix, PieceColor);

                return true;
            }
            else
                return false;
        }
        // private void _rotateCounterClockWise(double xOffset, yOffset)
        private bool _rotatedCounterClockWiseSuccessfully(CellColor[,] matrix)
        {
            if (PieceCanRotate(matrix, RotationalDirection.CounterClockwise))
            {
                ClearPieceCells(matrix);

                for (int i = 0; i < 4; i++)
                {
                    double x_pivotOffset = CellPos[i].x - PiecePivot.x;    // find blocks's X offset from Pivot.
                    double y_pivotOffset = CellPos[i].y - PiecePivot.y;    // find block's Y offset from Pivot.

                    CellPos[i].x = (int)(PiecePivot.x + y_pivotOffset);     // Update Location of block's X 90 degrees counter clockwise.
                    CellPos[i].y = (int)(PiecePivot.y - x_pivotOffset);     // Update Location of block's Y 90 degrees counter clockwise.
                }

                FillPieceCells(matrix, PieceColor);

                return true;
            }
            else
                return false;
        }
        // public virtual void Move(CellColor[,] matrix, RotationalDirection rotationalDirection)
        // Rotate tetromino clockwise or counterclockwise.
        public bool Move(CellColor[,] matrix, RotationalDirection direction)
        {
            switch (direction)                                       // NEED TO DRAW OUT BY HAND!!!
            {
                case RotationalDirection.Clockwise:
                    return _rotatedClockWiseSuccessfully(matrix);

                case RotationalDirection.CounterClockwise:
                    return _rotatedCounterClockWiseSuccessfully(matrix);

                default:
                    return false;
            }
        }



        // protected void Kick(CellColor[,] matrix, RotationState rotationState)
        // implement kick table offsets. (Possible a challenging goal.) !!!!!!!!!!!!!!!!!!!!!!!!!!!!


        // BOUNDS CHECKING & COLLISION //
        // private bool CellsAreInBounds(int width, int height)
        // test if cells are in bounds
        public static bool CellsAreInBounds(int x_pos, int y_pos)    // Takes the width and height of an array.
        {
            bool inBounds = true;

            if (x_pos < 0 || x_pos >= GameSettings.Width)
                inBounds = false;

            if (y_pos < 0 || y_pos >= GameSettings.Height)
                inBounds = false;

            return inBounds;
        }

        // private bool CellsAreEmpty(CellColor[,] matrix)
        // test if cells are empty
        public static bool CellsAreEmpty(CellColor[,] matrix, int x_pos, int y_pos)    // used by class Game method int _rowsCompleted(). 
        {
            if (matrix[x_pos, y_pos] > 0)           // All Cellcolor Enums other than empty have values greater than zero.
                return false;                       // Thus if value stored at specific Tetrisboard cell is > 0 then it is !empty

            else
                return true;
        }

        // private bool MoveIsAllowed(CellColor[,] matrix, int width, int height)
        // return true or false if area piece is attempting to move to is both in inbound and empty.
        public static bool MoveIsAllowed(CellColor[,] matrix, int x_pos, int y_pos)
        {
            bool isAllowed = true;

            if (!CellsAreInBounds(x_pos, y_pos))
                isAllowed = false;       // if cells are out of bounds return false.

            else if (!CellsAreEmpty(matrix, x_pos, y_pos))  // _cellsAreEmpty() requires a bounds check so that it doesn't attempt to read an out of bounds location from the game matrix.
                isAllowed = false;   // if cells were in bounds, check if their position is already occupied.

            else                    // both checks passed. _moveIsAllowed() returns true.
                isAllowed = true;

            return isAllowed;
        }
        public bool PieceCanMove(CellColor[,] matrix, int change_x, int change_y)    // Used by game loop to determine between droptime and locktime.
        {
            ClearPieceCells(matrix);

            bool isAllowed = true;

            for (int i = 0; i < 4; i++)      // for each tetramino cell
            {
                int testX = CellPos[i].x + change_x;    // test change in x
                int testY = CellPos[i].y + change_y;    // test change in y

                if (!MoveIsAllowed(matrix, testX, testY))
                    isAllowed = false;       // Check if the move was allowed
            }

            FillPieceCells(matrix, PieceColor);

            return isAllowed;
        }

        // BOOL CANROTATE() !!!
        // Simulates a rotation and determines whether or not it was in bounds
        public bool PieceCanRotate(CellColor[,] matrix, RotationalDirection direction)
        {
            ClearPieceCells(matrix);                                            // Clear Piece From Game Board Matrix.

            bool isAllowed = true;

            for (int i = 0; i < 4; i++)
            {
                CellCoords testCellPos; // test coordinates

                double X_offsetFromPivot = CellPos[i].x - PiecePivot.x;         // find blocks's X offset from Pivot.
                double Y_offsetFromPivot = CellPos[i].y - PiecePivot.y;         // find block's Y offset from Pivot.

                if (direction == RotationalDirection.Clockwise)
                {
                    testCellPos.x = (int)(PiecePivot.x - Y_offsetFromPivot);    // Record test location of each cells's X 90 degrees clockwise.
                    testCellPos.y = (int)(PiecePivot.y + X_offsetFromPivot);    // Record test location of each cells's Y 90 degrees clockwise.

                    if (!MoveIsAllowed(matrix, testCellPos.x, testCellPos.y))  // Check if the move was allowed
                        isAllowed = false;
                }
                else if (direction == RotationalDirection.CounterClockwise)     // Flipping the signs on these two lines results in CounterClockWise rotation.
                {
                    testCellPos.x = (int)(PiecePivot.x + Y_offsetFromPivot);    // Record test location of each cells's X 90 degrees counterclockwise.
                    testCellPos.y = (int)(PiecePivot.y - X_offsetFromPivot);    // Record test location of each cells's Y 90 degrees counterclockwise.

                    if (!MoveIsAllowed(matrix, testCellPos.x, testCellPos.y))
                        isAllowed = false;
                }
            }
            FillPieceCells(matrix, PieceColor);                // "Draw" Piece's New Location On Game Board Matrix.
            return isAllowed;

        }
    }
}

