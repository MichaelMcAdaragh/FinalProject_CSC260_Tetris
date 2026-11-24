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
        public CellColor[,] TetrisGameBoard;   // Declare Matrix for TetrisGameBoard
        private Random _rng;                   // Declare variable for local private Random Number Object.

        public int Score { get; private set; } // Score and Level can be read from anywhere,
        public int Level { get; private set; } // but can only be modified by Game Class.
        

        // Default Game Constructor
        public Game()
        {
            Score = 0;                                                                  // Score and Level start at Zero.
            Level = 0;
            TetrisGameBoard = new CellColor[GameSettings.Width, GameSettings.Height];   // Tetris Game Board is an CellColor matrix.
            _rng = new Random();                                                        // Instantiate pseudorandom number generator.
            _spawnRandomTetromino();
        }

        // Game Methods

        // void ClearGameBoard(CellColor[,] matrix)
        // Clear Game Board for New Game.


        // private Color _toRayLibColor(Cellcolor color)
        // convert enum color from the logic into a raylib compatable color.
        private Color _toRayLibColor(CellColor color)
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
        internal void DisplayGameBoard(CellColor[,] matrix /*, int score, int level*/)  // Runs inside the window's while loop.
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
                    _toRayLibColor(matrix[col,row]));                                   // !!!! Fills cell with the correct cell color for its position. !!!!

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

        // private void _spawnRandomTetromino(CellColor[,] matrix)
        // Spawn a Random New Tetromino at the Center of the Game Board.
        private void _spawnRandomTetromino()     // NEED TO FINISH //
        {
            switch ((TetrominoType)_rng.Next(0, 7))
            {
                case TetrominoType.I:
                    var i_piece = new I_Piece(TetrisGameBoard);     break;

                case TetrominoType.O:
                    var o_piece = new O_Piece(TetrisGameBoard);     break;

                case TetrominoType.T:
                    var t_piece = new T_Piece(TetrisGameBoard);     break;

                case TetrominoType.S:
                    var s_piece = new S_Piece(TetrisGameBoard);     break;

                case TetrominoType.Z:
                    var z_piece = new Z_Piece(TetrisGameBoard);     break;

                case TetrominoType.J:
                    var j_piece = new J_Piece(TetrisGameBoard);     break;

                case TetrominoType.L:
                    var l_piece = new L_Piece(TetrisGameBoard);     break;
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