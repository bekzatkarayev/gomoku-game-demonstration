using System;
using System.Collections.Generic;

namespace GomokuDemo
{
    class Game 
    {
        //any instance of the game tracks moves that has been made and state of the board
        //each move is stored in a list of moves of either player as a 4 digit number
        //first two digits represent row number, last two - column number in the Board array

        public static char[,] Board;
        public static int MoveCount;  
        public static int CurrentMove;
        public static int NoSuchMove;
        public static List<int> Player1Moves; 
        public static List<int> Player2Moves;
        public static List<int> PossibleMoves;

        public Game() 
        {
            Board = new char[15, 15];
            MoveCount = 0;
            NoSuchMove = 1515;      // just a dummy value that is returned if a certain strategy has not found any move
            Player1Moves = new List<int>();  
            Player2Moves = new List<int>();
            PossibleMoves = new List<int>(); //all 'empty' cells are stored in this list
            InitializeBoard();
        }

        public static void InitializeBoard() //fills initial board with '_'s 
        {
            for (int index = 0; index < 15; index++)
            {
                for (int runner = 0; runner < 15; runner++)
                {
                    PossibleMoves.Add(100 * index + runner); 
                    Board[index, runner] = '_';
                }
            }
        }

        public void PlayGame()
        {
            Move.MakeMove();
            MoveCount += 1;
            //Gameboard.DisplayBoard(); // uncomment this line to see the board after each move
        }

        public bool EndGame() //returns whether the game has finished or not
        {
            if (MoveCount <= 8) //does not have to check board when too few moves are made
            {
                return false;
            }
            else if (!Gameboard.CheckBoard()) //calls special method to look for 5 in a rows on the board
            {
                Gameboard.DisplayBoard();
                Console.WriteLine($"\nPlayer {(MoveCount + 1) % 2 + 1} won in {MoveCount} moves.");
                return true;
            }
            else if (MoveCount == 225) //if the board is full, game ends, there is no winner
            {
                Gameboard.DisplayBoard();
                Console.WriteLine("Game ended in a draw.");
                return true;
            }

            return false;
        }
    }
}
