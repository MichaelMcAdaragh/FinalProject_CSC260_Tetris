using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using Raylib_cs;

namespace FinalProject_CSC260_Tetris
{
    internal class Game
    {
        // Game Fields & Properties:
        internal CellColor[,] TetrisGameBoard;      // Declare Matrix for TetrisGameBoard
        private TetrominoPiece _currentPiece;       // Declare private variable for the current piece.
        private Random _rng;                        // Declare variable for private Random Number Object.
        private double _timer;
        private double _timeAllowed;

        public int Score { get; private set; }      // Score and Level can be read from anywhere,
        public int Level { get; private set; }      // but can only be modified by Game Class.


        // Default Game Constructor
        public Game()
        {
            Score = 0;                                                                  // Score and Level start at Zero.
            Level = 0;

            TetrisGameBoard = new CellColor[GameSettings.Width, GameSettings.Height];   // Tetris Game Board is an CellColor matrix.

            _rng = new Random();                                                        // Instantiate pseudorandom number generator.
            _timer = 0; // secs                                                         // game loop move timer starts at 0
            _timeAllowed = 2; // secs                                                  // game loop interval starts at 2 secs
            _spawnRandomTetromino();
        }

        // Game Methods

        // private void _spawnRandomTetromino(CellColor[,] matrix)
        // Spawns a Random New Tetromino at the Center of the Game Board and stores it's class object type to _currentPiece.
        private void _spawnRandomTetromino()
        {
            switch ((TetrominoType)_rng.Next(0, 7))  // tetromino types instantiated by switch with psuedorandom TetrominoType parameter.
            {                                        // saved as _currentPiece
                case TetrominoType.I:
                    var i_piece = new I_Piece(TetrisGameBoard);
                    _currentPiece = i_piece;
                    break;

                case TetrominoType.O:
                    var o_piece = new O_Piece(TetrisGameBoard);
                    _currentPiece = o_piece;
                    break;

                case TetrominoType.T:
                    var t_piece = new T_Piece(TetrisGameBoard);
                    _currentPiece = t_piece;
                    break;

                case TetrominoType.S:
                    var s_piece = new S_Piece(TetrisGameBoard);
                    _currentPiece = s_piece;
                    break;

                case TetrominoType.Z:
                    var z_piece = new Z_Piece(TetrisGameBoard);
                    _currentPiece = z_piece;
                    break;

                case TetrominoType.J:
                    var j_piece = new J_Piece(TetrisGameBoard);
                    _currentPiece = j_piece;
                    break;

                case TetrominoType.L:
                    var l_piece = new L_Piece(TetrisGameBoard);
                    _currentPiece = l_piece;
                    break;
            }
        }

        // private Color _toRayLibColor(Cellcolor color)
        // convert from CellColor to RayLib Color.
        private Color _toRayLibColor(CellColor color)   // This Conversion Allows abstraction between Raylib and my Underlying Logic.
        {
            return color switch                         // Cool switch syntax!!! outputs corresponding Color (Raylib) from CellColor inputs.
            {
                CellColor.Empty => Color.Black,
                CellColor.Cyan => new Color(0, 255, 255, 255),    // Custom Cyan Color
                CellColor.Yellow => Color.Yellow,
                CellColor.Purple => Color.Purple,
                CellColor.Green => Color.Green,
                CellColor.Red => Color.Red,
                CellColor.Blue => Color.Blue,
                CellColor.Orange => Color.Orange,
                _ => Color.Black
            };
        }

        // void DisplayGameBoard(CellColor[,] matrix, int score, int level)
        // Display Game Board.
        public void DisplayGameBoard()   // Runs inside the window's while loop.
        {
            for (int row = 0; row < GameSettings.Height; row++)                           // nested for loop is drawn same as terminal output.
            {
                for (int col = 0; col < GameSettings.Width; col++)                        // but pixel math instead of ASCII characters.
                {
                    // My Adaptation of the following Claude Snippet:
                    Raylib.DrawRectangle(                                               // fills each cell with the correct cell color.
                    col * GameSettings.CellSize,                                        // calculates current pos X in pixels
                    row * GameSettings.CellSize,                                        // calculates current pos Y in pixels
                    GameSettings.CellSize,                                              // rectangle width in pixels
                    GameSettings.CellSize,                                              // rectangle height in pixels
                    _toRayLibColor(TetrisGameBoard[col, row])); // !!!! Fills cell in with the correct cell color for its game board position. !!!!

                    // This Snippet from Claude:
                    Raylib.DrawRectangleLines(                                          // draws a gray rectangle around each cell.
                    col * GameSettings.CellSize,                                         // calculates current pos X in pixels
                    row * GameSettings.CellSize,                                         // calculates current pos Y in pixels
                    GameSettings.CellSize,                                              // rectangle width in pixels
                    GameSettings.CellSize,                                              // rectangle height in pixels
                    Color.DarkGray);                                                    // rectangle color DarkGray
                }                                                                       // This snippet makes the Game Board Cells into a Gridiron.
            }
        }

        // public void HandleUserInput()
        // take raw input from the user
        public void HandleUserInput()
        {
            //bool moveSucceeded = false;

            KeyboardKey key = (KeyboardKey)Raylib.GetKeyPressed();  // gets the key the player pressed and stores it in key variable
            switch (key)
            {
                case KeyboardKey.Left:
                    /*moveSucceeded =*/
                    _currentPiece.Move(TetrisGameBoard, Direction.Left);
                    break;

                case KeyboardKey.Right:
                    /*moveSucceeded =*/
                    _currentPiece.Move(TetrisGameBoard, Direction.Right);
                    break;

                case KeyboardKey.Down:
                    /*moveSucceeded =*/
                    _currentPiece.Move(TetrisGameBoard, Direction.Down);
                    break;

                case KeyboardKey.J:
                    /*moveSucceeded =*/
                    _currentPiece.Move(TetrisGameBoard, RotationalDirection.Clockwise);
                    break;

                case KeyboardKey.F:
                    /*moveSucceeded =*/
                    _currentPiece.Move(TetrisGameBoard, RotationalDirection.CounterClockwise);
                    break;
            }
        }

        // public void DropTimer(double changeInTime)   - From an altered Claude Snippet. It reset the the _dropTimer if the player made a move. (which is bad tetris.) I fixed.
        // runs in the window's while loop. forces the piece to drop at regular intervals - initially every two seconds.
        public void GameLoopTimer(double changeInTime)
        {
            _timer += changeInTime;         // change in time between frames at 60 FPS. approx 0.01666 secs per frame.

            // if (_timer >= _timeTillDrop)                             // If moveTimeAllowed elapses
            // {
            bool pieceCanMoveDown = _currentPiece.PieceCanMove(TetrisGameBoard, 0, 1);

            if (pieceCanMoveDown)                                                // If Piece has space to move below.
            {
                if (_timer >= _timeAllowed)
                {
                    _timer = 0;                                                 // Reset _dropTimer for next drop
                    _currentPiece.Move(TetrisGameBoard, Direction.Down);        // Move Piece Down Automatically.
                }
            }
            else
            {
                if (_timer >= _timeAllowed)
                {   // "Lock-in piece." No need to actually do anything

                    // PEFORM END OF TETROMINO'S SERVICE LIFE OPERATIONS:

                    // checkCompletedRows().
                    // clearCompletedRows().
                    // countRowsCleared().
                    // addToScore()
                    // checkForLevelUp()
                    _spawnRandomTetromino();
                }
            }
        }
        // private bool _rowIsCompleted(CellColor[,] matrix)
        // Check if row is completed
        private int _rowsCompleted()
        {
            int rowsCompleted = 0;

            for (int row = 0; row < GameSettings.Height; row++)         // Go thru every row
            {
                for (int col = 0; col < GameSettings.Width; col++)      // Go thru every column
                {
                    if (TetrominoPiece.CellsAreEmpty(TetrisGameBoard, col, row))
                    { 
                        
                    }
                }
            }

            return rowsCompleted;
        }

        // private void _clearRow(CellColor[,] matrix)
        // Clear a row. Move Blocks Above Down.

        // private int _rowsCompleted(CellColor[,] matrix)
        // return the number of rows completed.

        // private void _addToScore(int rows_completed)
        // add to score based on number of rows completed

        // private void _checkScoreForLevelUp(int score)
        // check score and increment level if eligible.

        // private void _checkIfGameIsOver(CellColor[,] matrix)
        // end game if block locks in on the top row.
    }
}