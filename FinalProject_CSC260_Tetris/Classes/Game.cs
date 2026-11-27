using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;

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
        public int TotalRowsCleared { get; private set; }
        public int Level
        {
            get { return TotalRowsCleared / 10; }   // Level is based on rows cleared. calculates every time value is needed.
        }
        private int _highestLevel;   // tracks highest level year achieved.

        // Default Game Constructor
        public Game()
        {
            Score = 0;                                                                  // Score and Level start at Zero.
            TotalRowsCleared = 0;
            _highestLevel = 0;

            TetrisGameBoard = new CellColor[GameSettings.Width, GameSettings.Height];   // Tetris Game Board is an CellColor matrix.

            _rng = new Random();                                                        // Instantiate pseudorandom number generator.
            _timer = 0; // secs                                                         // game loop move timer starts at 0
            _timeAllowed = 1; // secs                                                  // game loop interval starts at 2 secs
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
        // Tetris Guidelines:   (I currently have L & Z spawning rounded to the right in defiance of the guidelines.)
        //All tetrominoes spawn horizontally.
        //The I and O tetrominoes spawn centrally, and the other, 3-cell wide tetrominoes spawn rounded to the left.
        //The J, L and T spawn flat-side first, while I, S and Z spawn in their upper horizontal orientation.
        }
        

        // private Color _toRayLibColor(Cellcolor color)
        // convert from CellColor to RayLib Color.
        private Raylib_cs.Color _toRayLibColor(CellColor color)   // This Conversion Allows abstraction between Raylib and my Underlying Logic.
        {
            return color switch                         // Cool switch syntax!!! outputs corresponding Color (Raylib) from CellColor inputs.
            {
                CellColor.Empty => Raylib_cs.Color.Black,
                CellColor.Cyan => new Raylib_cs.Color(0, 255, 255, 255),    // Custom Cyan Color
                CellColor.Yellow => Raylib_cs.Color.Yellow,
                CellColor.Purple => Raylib_cs.Color.Purple,
                CellColor.Green => Raylib_cs.Color.Green,
                CellColor.Red => Raylib_cs.Color.Red,
                CellColor.Blue => Raylib_cs.Color.Blue,
                CellColor.Orange => Raylib_cs.Color.Orange,
                _ => Raylib_cs.Color.Black
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
                    col * GameSettings.CellSize + (GameSettings.CellSize),              // calculates current pos X in pixels
                    row * GameSettings.CellSize + (2 * GameSettings.CellSize),          // calculates current pos Y in pixels   //lower the print out
                    GameSettings.CellSize,                                              // rectangle width in pixels
                    GameSettings.CellSize,                                              // rectangle height in pixels
                    _toRayLibColor(TetrisGameBoard[col, row])); // !!!! Fills cell in with the correct cell color for its game board position. !!!!

                    // This Snippet from Claude:
                    Raylib.DrawRectangleLines(                                          // draws a gray rectangle around each cell.
                    col * GameSettings.CellSize + (GameSettings.CellSize),              // calculates current pos X in pixels
                    row * GameSettings.CellSize + (2 * GameSettings.CellSize),          // calculates current pos Y in pixels
                    GameSettings.CellSize,                                              // rectangle width in pixels
                    GameSettings.CellSize,                                              // rectangle height in pixels
                    Raylib_cs.Color.DarkGray);                                                    // rectangle color DarkGray
                }                                                                       // This snippet makes the Game Board Cells into a Gridiron.
            }
            Raylib.DrawText($"Level: {Level}",
                10 + GameSettings.CellSize,
                10 + GameSettings.CellSize,
                18,
                Raylib_cs.Color.White);

            Raylib.DrawText($"Rows Cleared: {TotalRowsCleared}",
                30 + (3 * GameSettings.CellSize),
                10 + GameSettings.CellSize,
                18,
                Raylib_cs.Color.White);

            Raylib.DrawText($"Score: {Score}", 
                40 + (7 * GameSettings.CellSize),
                10 + GameSettings.CellSize,
                18,
                Raylib_cs.Color.White);
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

                    // PEFORM TETROMINO'S END OF LIFE OPERATIONS:               // This chunck of code is where most
                    int rowsCompleted = _clearRowsAndReportQuantity();          // of the operations occure.
                    _addToScore(rowsCompleted);
                    TotalRowsCleared += rowsCompleted;
                    _checkForSpeedUp();
                    // _gameIsOver() !!!!!!!!!!!!!!!!!!!!!!!!!!!
                    _spawnRandomTetromino();
                }
            }
        }

        // private void _lowerCells(int row)
        // lower the cells above the given row
        private void _lowerCells(int givenRow)
        {
            for (int row = givenRow - 1; row > 0; row--)    //lower every cell above the cleared row.
            {                                               //will want to start 1 row above cleared row hence row = givenRow - 1, then work backward towards top hence row--
                for (int col = 0; col < GameSettings.Width; col++)       // work left to right
                {
                    CellColor cellColor = TetrisGameBoard[col,row];      // store the cells color
                    TetrisGameBoard[col, row] = CellColor.Empty;         // turn off the cell
                    if(row < GameSettings.Height)
                        TetrisGameBoard[col, row + 1] = cellColor;       // fill in the cell below it.
                }
            }
        }

        // private void _clearRow()
        // Clear a row. Move Blocks Above Down.
        private void _clearRow(int givenRow)
        {
            for (int col = 0; col < GameSettings.Width; col++)          // work left to right
            {
                TetrisGameBoard[col,givenRow] = CellColor.Empty;
            }
        }

        // private int _rowsCompleted(CellColor[,] matrix)
        // return the number of rows completed.
        // Check if row is completed, clear completed rows, return count of completed rows.
        private int _clearRowsAndReportQuantity()
        {
            int rowsCompleted = 0;

            for (int row = 0; row < GameSettings.Height; row++)         // Go thru every row
            {
                int cellsOccupied = 0;

                for (int col = 0; col < GameSettings.Width; col++)      // Go thru every column
                {
                    if (!GameUtilities.CellIsEmpty(TetrisGameBoard, col, row))   // if the cells at the given point are Not Empty:
                    {
                        cellsOccupied++;    // increment cells occupied.
                    }
                }

                if (cellsOccupied == 10)
                {
                    _clearRow(row);
                    _lowerCells(row);
                    rowsCompleted++;
                }
            }
            return rowsCompleted;
        }
       
        // private void _addToScore(int rows_completed)
        // add to score based on number of rows completed
        private void _addToScore(int rowsCompleted)     // Base on standard tetris rules.
        {
            switch (rowsCompleted)
            {
                case 1:
                    Score += (40 * (Level + 1));    break;

                case 2:
                    Score += (100 * (Level + 1));   break;

                case 3:
                    Score += (300 * (Level + 1));   break;

                case 4:
                    Score += (1200 * (Level + 1));  break;

                default: break;            
            }
        }

        // private void _checkScoreForLevelUp(int score)
        // check score and increment level if eligible.
        private void _checkForSpeedUp()
        {
            if (Level > _highestLevel) // if a level up occured
            {
                _timeAllowed *= .95;    // 5% faster per level?     alternately: /*Math.Pow(0.90, Level);*/ exponential decay
                _highestLevel = Level;  // update new high level
                   
             //  Would like to get some LevelUp Congrats to Display Here???
             //  
             //   Raylib.DrawText("Level Up!",
             //  10 + (4 * GameSettings.CellSize),
             //  10 + (6 * GameSettings.CellSize),
             //  30,
             //  Color.White);
            }
        }

        // private void _checkIfGameIsOver(CellColor[,] matrix)     !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        // end game if block locks in on the top row.               !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    }
}