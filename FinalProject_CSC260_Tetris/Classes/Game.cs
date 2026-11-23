using System;
using System.Collections.Generic;
using System.Text;

namespace FinalProject_CSC260_Tetris
{
    class Game
    {
        // Game Fields & Properties:
        public CellColor[,] TetrisGameBoard;   // Declare Matrix for TetrisGameBoard
        public int Score { get; private set; } // Score and Level can be read from anywhere,
        public int Level { get; private set; } // but can only be modified by Game Class.

        // Default Game Constructor
        Game()
        {
            Score = 0;
            Level = 0;
            TetrisGameBoard = new CellColor[GameSettings.Width, GameSettings.Height];
            // Create New Piece and Start Game Loop???
        }

        // Game Methods
        
        // void ClearGameBoard(CellColor[,] matrix)
        // Clear Game Board for New Game.

        // void DisplayGameBoard(CellColor[,] matrix, int score, int level)
        // Display Game Board.

        // private void _spawnRandomTetromino(CellColor[,] matrix)
        // Spawn a Random New Tetromino at the Center of the Game Board.

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