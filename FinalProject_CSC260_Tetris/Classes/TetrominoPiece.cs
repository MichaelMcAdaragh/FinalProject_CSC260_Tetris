using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Text;

namespace FinalProject_CSC260_Tetris
{
    abstract class TetrominoPiece
    {
        // TetrominoPiece Fields & Properties:
        
        protected CellCoords[] PiecePos = new CellCoords[4]; // Array of xy int coord structs that represents each of the piece's cell positions.
        protected PivotCoords PiecePivot;  // xy double coord struct that represents the exact radial pivot of the piece's rotation.
        public CellColor PieceColor { get; protected set; }    // stores the enumerated cell color of the piece.
        protected int PieceRotationState = (int)RotationState.Deg000; // store rotation state. initialized at zero degrees.
        public int MaxBottomRotationsAllowed { get; set; } = 15;        
        // Kick Offset Table:
        private static CellCoords[,] _defaultKickOffsets = new CellCoords[8, 5]    // 8 Rotation states =
        // I piece is exception and will overwrite this data          // 4 Clockwise rotation states + 4 Counterclockwise rotation states.
        
        {   // Test 1             // Test 2                // Test 3               // Test 4               // Test 5
/* 0 -> R */   {new CellCoords(0,0),  new CellCoords(-1, 0),   new CellCoords(-1,-1),  new CellCoords(0,-2),   new CellCoords(-1, 2)},  // 0  // note, y coords must be inverted                                                                                                                   // 
/* 0 -> L */   {new CellCoords(0,0),  new CellCoords( 1, 0),   new CellCoords( 1,-1),  new CellCoords(0,-2),   new CellCoords( 1, 2)},  // 1

/* R -> 0 */   {new CellCoords(0,0),  new CellCoords( 1, 0),   new CellCoords( 1, 1),  new CellCoords(0, 2),   new CellCoords( 1,-2)},  // 2
/* R -> 2 */   {new CellCoords(0,0),  new CellCoords( 1, 0),   new CellCoords( 1, 1),  new CellCoords(0, 2),   new CellCoords( 1,-2)},  // 3
                                                                                                                      // 
/* 2 -> R */   {new CellCoords(0,0),  new CellCoords(-1, 0),   new CellCoords(-1,-1),  new CellCoords(0,-2),   new CellCoords(-1, 2)},  // 4
/* 2 -> L */   {new CellCoords(0,0),  new CellCoords( 1, 0),   new CellCoords( 1,-1),  new CellCoords(0,-2),   new CellCoords( 1, 2)},  // 5                
  
/* L -> 2 */   {new CellCoords(0,0),  new CellCoords(-1, 0),   new CellCoords(-1, 1),  new CellCoords(0, 2),   new CellCoords(-1,-2)},  // 6
/* L -> 0 */   {new CellCoords(0,0),  new CellCoords(-1, 0),   new CellCoords(-1, 1),  new CellCoords(0, 2),   new CellCoords(-1,-2)},  // 7
        };
        // protected virtual CellCoords[,] KickOffSets => _defaultKickOffsets;

        // Legend from tetris.wiki/Super_Rotation_System page:
        // 0 = spawn state
        // R = state resulting from a clockwise rotation("right") from spawn
        // L = state resulting from a counter - clockwise("left") rotation from spawn
        // 2 = state resulting from 2 successive rotations in either direction from spawn.

        // TetrominoPiece METHODS:
        // PIECE MOVEMENT AND MANIPULATION //

        // protected void FillCells(CellColor[,] matrix, Cell Coords[] piece)
        public static void FillPieceCells(CellColor[,] matrix, CellCoords[] piecePos, CellColor color)
        {
            for (int i = 0; i < 4; i++)
            {
                matrix[piecePos[i].X, piecePos[i].Y] = color; // set current cells to given cell color
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
                    PiecePos[i].X += change_x;    // apply change in x
                    PiecePos[i].Y += change_y;    // apply change in y
                }

                PiecePivot.X += change_x;        // since move was allowed, update the x-y pos of pivot to reflect the new positions of the piece's cells
                PiecePivot.Y += change_y;

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

        // private void _rotatedSuccessfully(CellColor[,] matrix, RotationalDirection direction)
        // attempts to apply rotation and returns if it was successful or not.
        private bool _rotatedSuccessfully(CellColor[,] matrix, RotationalDirection direction)
        {
            CellCoords? validKick = _validKick(matrix, direction);
            if (validKick == null)
                return false;

            ClearPieceCells(matrix, PiecePos);

            for (int i = 0; i < 4; i++)
            {
                double x_pivotOffset = PiecePos[i].X - PiecePivot.X;    // find individual cell's X offset from Pivot.
                double y_pivotOffset = PiecePos[i].Y - PiecePivot.Y;    // find individual cell's Y offset from Pivot.

                if (direction == RotationalDirection.Clockwise && validKick != null)
                {
                    PiecePos[i].X = (int)(PiecePivot.X - y_pivotOffset) + validKick.Value.X;    // Update Location of block's X 90 degrees clockwise.
                    PiecePos[i].Y = (int)(PiecePivot.Y + x_pivotOffset) + validKick.Value.Y;    // Update Location of block's Y 90 degrees clockwise.
                }
                else if (direction == RotationalDirection.CounterClockwise && validKick != null)
                {
                    PiecePos[i].X = (int)(PiecePivot.X + y_pivotOffset) + validKick.Value.X;     // Update Location of block's X 90 degrees counter clockwise.
                    PiecePos[i].Y = (int)(PiecePivot.Y - x_pivotOffset) + validKick.Value.Y;     // Update Location of block's Y 90 degrees counter clockwise.
                }                  
            }
            PiecePivot.X += (double)validKick.Value.X;                      // Update Pivot
            PiecePivot.Y += (double)validKick.Value.Y;

            if (direction == RotationalDirection.Clockwise)
                PieceRotationState = (PieceRotationState + (int)RotationState.Deg090) % 4;
            else
                PieceRotationState = (PieceRotationState + 4 - (int)RotationState.Deg090) % 4;

            FillPieceCells(matrix, PiecePos, PieceColor);
            return true;
        }   //RotationDegree.090 == 1. Each Piece has 4 rotation states Deg000 == 0, Deg090 == 1, Deg180 == 2, Deg270 == 3

        // public virtual void Move(CellColor[,] matrix, RotationalDirection rotationalDirection)
        // Rotate tetromino clockwise or counterclockwise.
        public bool Move(CellColor[,] matrix, RotationalDirection direction)
        {
            if (direction == RotationalDirection.Clockwise)                                       // NEED TO DRAW OUT BY HAND!!!
                return _rotatedSuccessfully(matrix, RotationalDirection.Clockwise);
            else
                return _rotatedSuccessfully(matrix, RotationalDirection.CounterClockwise);
        }

        // BOUNDS CHECKING & COLLISION //

        public bool PieceCanMove(CellColor[,] matrix, int change_x, int change_y)    // Used by game loop to determine between droptime and locktime.
        {
            ClearPieceCells(matrix, PiecePos);

            bool isAllowed = true;

            for (int i = 0; i < 4; i++)      // for each tetramino cell
            {
                int testX = PiecePos[i].X + change_x;    // test change in x
                int testY = PiecePos[i].Y + change_y;    // test change in y

                if (!GameUtilities.CellCanMoveTo(matrix, testX, testY))
                    isAllowed = false;       // Check if the move was allowed
            }

            FillPieceCells(matrix, PiecePos, PieceColor);

            return isAllowed;
        }
        
        // KICK OFFSET / ROTATION HELPER METHODS

        // private bool _kick(CellColor[,] matrix, RotationState rotationState) !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        // implement kick table offsets. (Possible a challenging goal.) !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        private CellCoords? _fromKickOffsetMatrix(RotationalDirection direction, int attempt)
        {
            CellCoords? kickOffset = new CellCoords();      // Had to use question mark to make nullable

            if (attempt < 5)
            {
                switch (PieceRotationState)
                {
                    case (int)RotationState.Deg000:
                        if (direction == (int)RotationalDirection.Clockwise)
                            kickOffset = _defaultKickOffsets[(int)RotationAttempt.InitialtoRight, attempt];

                        else if (direction == RotationalDirection.CounterClockwise)
                            kickOffset = _defaultKickOffsets[(int)RotationAttempt.InitialtoLeft, attempt]; break;

                    case (int)RotationState.Deg090:
                        if (direction == RotationalDirection.Clockwise)
                            kickOffset = _defaultKickOffsets[(int)RotationAttempt.RighttoOpposite, attempt];

                        else if (direction == RotationalDirection.CounterClockwise)
                            kickOffset = _defaultKickOffsets[(int)RotationAttempt.RighttoInitial, attempt]; break;

                    case (int)RotationState.Deg180:
                        if (direction == RotationalDirection.Clockwise)
                            kickOffset = _defaultKickOffsets[(int)RotationAttempt.OppositetoLeft, attempt];

                        else if (direction == RotationalDirection.CounterClockwise)
                            kickOffset = _defaultKickOffsets[(int)RotationAttempt.OppositetoRight, attempt]; break;

                    case (int)RotationState.Deg270:
                        if (direction == RotationalDirection.Clockwise)
                            kickOffset = _defaultKickOffsets[(int)RotationAttempt.LefttoInitial, attempt];

                        else if (direction == RotationalDirection.CounterClockwise)
                            kickOffset = _defaultKickOffsets[(int)RotationAttempt.LefttoOpposite, attempt]; break;
                }
                return kickOffset;
            }
            else
                kickOffset = null;
            return kickOffset;
        }

        // _validKick()
        // returns a valid rotation &/ KickOffset, null if no valid rotation/offsets
        private CellCoords? _validKick(CellColor[,] matrix, RotationalDirection direction)
        {  
            CellCoords testPiecePos;                 // xy struct to store test coordinates
            CellCoords? validKick = null;            // if still null after method, then no valid kick. this is returned.

            ClearPieceCells(matrix, PiecePos);       // Clear Piece From Game Board Matrix.            

            int attempts = 0;
            bool? succeeded = false;
            while (attempts < 5 && succeeded == false)
            {
                int timesSucceeded = 4;                                             // starts at 4, if any are deducted, test fails.

                CellCoords? kickOffset = _fromKickOffsetMatrix(direction , attempts);

                for (int i = 0; i < 4; i++)
                {                                                           
                    double X_offsetFromPivot = PiecePos[i].X - PiecePivot.X;         // find blocks's X offset from Pivot.
                    double Y_offsetFromPivot = PiecePos[i].Y - PiecePivot.Y;         // find block's Y offset from Pivot.

                    if (direction == RotationalDirection.Clockwise)
                    {
                        testPiecePos.X = (int)(PiecePivot.X - Y_offsetFromPivot);    // Record test location of each cells's .X 90 degrees clockwise.
                        testPiecePos.Y = (int)(PiecePivot.Y + X_offsetFromPivot);    // Record test location of each cells's Y 90 degrees clockwise.

                        
                        if (kickOffset != null)
                        {
                            testPiecePos.X += kickOffset.Value.X;    // .Value has to come directly after nullable wrapper
                            testPiecePos.Y += kickOffset.Value.Y;    // Apply KickOffset to Test Position
                        }
                        if (!GameUtilities.CellCanMoveTo(matrix, testPiecePos.X, testPiecePos.Y))  // Check if the move was allowed
                            timesSucceeded--;
                    }
                    else if (direction == RotationalDirection.CounterClockwise)     // Flipping the signs on these two lines results in CounterClockWise rotation.
                    {
                        testPiecePos.X = (int)(PiecePivot.X + Y_offsetFromPivot);    // Record test location of each cells's .X 90 degrees counterclockwise.
                        testPiecePos.Y = (int)(PiecePivot.Y - X_offsetFromPivot);    // Record test location of each cells's Y 90 degrees counterclockwise.

                        if (kickOffset != null)
                        {
                            testPiecePos.X += kickOffset.Value.X;    // Apply KickOffset To Test Position
                            testPiecePos.Y += kickOffset.Value.Y;
                        }
                        if (!GameUtilities.CellCanMoveTo(matrix, testPiecePos.X, testPiecePos.Y))
                            timesSucceeded--;
                    }
                }
                if (timesSucceeded < 4)
                    succeeded = false;
                else
                {
                    succeeded = true;
                    validKick = kickOffset;
                }

                attempts++;
            }
            FillPieceCells(matrix, PiecePos, PieceColor);                // "Draw" Piece's New Location On Game Board Matrix.
            return validKick;
        }
    }
}

