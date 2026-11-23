namespace FinalProject_CSC260_Tetris.Main
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            game.DisplayGameBoard(game.TetrisGameBoard);
        }
    }
}
