using System;
using System.Collections.Generic;
using System.Data;
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
        private Random _rng;                        // Declare variable for local private Random Number Object.
        private double _moveTimer;                  // Game Loop Move Timer
        private double _timerInterval;               // Game Loop Interval time.

        public int Score { get; private set; } // Score and Level can be read from anywhere,
        public int Level { get; private set; } // but can only be modified by Game Class.
        

        // Default Game Constructor
        public Game()
        {
            Score = 0;                                                                  // Score and Level start at Zero.
            Level = 0;
            
            TetrisGameBoard = new CellColor[GameSettings.Width, GameSettings.Height];   // Tetris Game Board is an CellColor matrix.
            
            _rng = new Random();                                                        // Instantiate pseudorandom number generator.
            _moveTimer = 0;                                                             // game loop move timer starts at 0
            _timerInterval = 10;                                                         // game loop interval starts at 10
            _spawnRandomTetromino();
        }

        // Game Methods

        // private Color _toRayLibColor(Cellcolor color)
        // convert enum color from the logic into a raylib compatable color.
        private Color _toRayLibColor(CellColor color)             // This Conversion Allows abstraction between Raylib and my Underlying Logic.
        {
            return color switch                                   // Cool switch syntax!!!
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
        public void DisplayGameBoard(CellColor[,] matrix /*, int score, int level*/)  // Runs inside the window's while loop.
        {
            for (int row = 0; row<GameSettings.Height; row++)                           // nested for loop is same as terminal print out
            {
                for (int col = 0; col<GameSettings.Width; col++)                        // but pixel math instead of ASCII characters.
                {
                    Raylib.DrawRectangle(                                               // fills each cell with the correct cell color.
                    col * GameSettings.CellSize,                                        // calculates current pos X in pixels
                    row * GameSettings.CellSize,                                        // calculates current pos Y in pixels
                    GameSettings.CellSize,                                              // rectangle width in pixels
                    GameSettings.CellSize,                                              // rectangle height in pixels
                    _toRayLibColor(matrix[col,row]));            // !!!! Fills cell with the correct cell color for its position. !!!!

                    Raylib.DrawRectangleLines(                                          // draws a gray rectangle around each cell.
                    col* GameSettings.CellSize,                                         // calculates current pos X in pixels
                    row* GameSettings.CellSize,                                         // calculates current pos Y in pixels
                    GameSettings.CellSize,                                              // rectangle width in pixels
                    GameSettings.CellSize,                                              // rectangle height in pixels
                    Color.DarkGray);                                                    // rectangle color DarkGray
                }
            }
        }

        // DisplayGameBoard() method. (TERMINAL TEST PRINT)
        /*
        internal void DisplayGameBoard(CellColor[,] matrix)    // Displays Current Game Matrix On Terminal
                {
                    Console.WriteLine("\n\n\n----------SCORE: 000000----------");
                    Console.WriteLine("-.--.--.--.--.--.--.--.--.--.--.-");
                    for (int i = 0; i < GameSettings.Height; i++)
                    {
                        Console.Write("|");
                        for (int j = 0; j < GameSettings.Width; j++)
                        {
                            switch ((CellColor)matrix[j, i])
                            {
                                case CellColor.Empty:
                                    Console.Write(".  ");
                                    break;
                                case CellColor.Cyan:
                                    Console.Write(".CC");
                                    break;
                                case CellColor.Yellow:
                                    Console.Write(".YY");
                                    break;
                                case CellColor.Purple:
                                    Console.Write(".PP");
                                    break;
                                case CellColor.Green:
                                    Console.Write(".GG");
                                    break;
                                case CellColor.Red:
                                    Console.Write(".RR");
                                    break;
                                case CellColor.Blue:
                                    Console.Write(".BB");
                                    break;
                                case CellColor.Orange:
                                    Console.Write(".OO");
                                    break;
                            }
                        }
                        Console.WriteLine(".|");
                    }
                    Console.WriteLine("---------------------------------");
                }
                */
        // public void HandleUserInput()
        // take raw input from the user
        public void HandleUserInput()
        {
            KeyboardKey key = (KeyboardKey)Raylib.GetKeyPressed();  // gets the key the player pressed and stores it in key variable
            bool moved = true;  // boolean flag to track if the player made a move.

            switch (key)
            {
                case KeyboardKey.Left:
                    _currentPiece.Move(TetrisGameBoard, Direction.Left);
                    break;

                case KeyboardKey.Right:
                    _currentPiece.Move(TetrisGameBoard, Direction.Right);
                    break;

                case KeyboardKey.Down:
                    _currentPiece.Move(TetrisGameBoard, Direction.Down);
                    break;

                case KeyboardKey.J:
                    _currentPiece.Move(TetrisGameBoard, RotationalDirection.Clockwise);
                    break;

                case KeyboardKey.F:
                    _currentPiece.Move(TetrisGameBoard, RotationalDirection.CounterClockwise);
                    break;

                default:
                    moved = false;  // player made no move
                    break;
            }
            if (moved == true)
            {
                _moveTimer = 0;     // reset the timer if the player makes a move.
            }
        }

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


        // private void _lockTetromino(CellColor[,] matrix)
        // Lock Tetromino to the Bottom of the Board or to the Block Beneath it.

        // private bool _rowIsCompleted(CellColor[,] matrix)
        // Check if row is completed

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