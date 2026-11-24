using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

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
        }

        // Game Methods

        // void ClearGameBoard(CellColor[,] matrix)
        // Clear Game Board for New Game.

        // void DisplayGameBoard(CellColor[,] matrix, int score, int level)
        // Display Game Board.
        // DisplayGameBoard() method. (TERMINAL TEST PRINT)
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

        // private void _spawnRandomTetromino(CellColor[,] matrix)
        // Spawn a Random New Tetromino at the Center of the Game Board.
        private TetrominoType _getRandomTetraminoType(Random rng)
        { 
            return (TetrominoType)_rng.Next(0,7);  // 0 is inclusive, 7 is exclusive  // Currently random nums 0-6 pieces I - L
        }
        // private void _spawnRandomTetromino(CellColor[,] matrix)
        // Spawn a Random New Tetromino at the Center of the Game Board.
        internal void SpawnRandomTetromino(CellColor[,] matrix)     // NEED TO FINISH //
        {
            switch (_getRandomTetraminoType(_rng))
            {
                case TetrominoType.I:
                    var i_piece = new I_Piece(matrix);
                    break;

                case TetrominoType.O:
                    var o_piece = new O_Piece(matrix);
                    break;

                case TetrominoType.T:
                    var t_piece = new T_Piece(matrix);
                    break;

                case TetrominoType.S:
                    var s_piece = new S_Piece(matrix);
                    break;

                case TetrominoType.Z:
                    var z_piece = new Z_Piece(matrix);
                    break;

                case TetrominoType.J:
                    var j_piece = new J_Piece(matrix);
                    break;

                case TetrominoType.L:
                    var l_piece = new L_Piece(matrix);
                    break;

                default:    // bad input do nothing.
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