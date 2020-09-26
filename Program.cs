using System;

namespace GomokuDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var myGame = new Game();   // instance of the game is created

            // From my observations it usually lasts about 30 moves, but longer games are possible
            // Player 1 wins more often, since he moves first 
            // Game ends in a draw, when no empty cells are left on the board, it happens rarely.
           
            while (!myGame.EndGame()) // the game is being played unless end game condition is met
            {
                myGame.PlayGame();
            }
        }
    }
}
