using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GomokuDemo
{
    class Gameboard : Game
    {
        public static void DisplayBoard() 
        {
            Console.WriteLine($"\n\nMove №{MoveCount} {(char)(CurrentMove % 100 + 'a')}{15 - CurrentMove / 100}\n");

            for (char columnIndex = '`'; columnIndex <= 'o'; columnIndex++)
            {
                Console.Write("{0, -3}", columnIndex);
            }

            for (int index = 0; index < 15; index++)
            {
                Console.WriteLine();
                Console.Write("{0, -3}", 15 - index);
                for (int runner = 0; runner < 15; runner++)
                {
                    Console.Write("{0, -3}", Board[index, runner]);
                }
            }
        }

        // CheckBoard method returns true if the board is still playable and
        // returns false if exactly 5 in a row of the same stone is found
        // Analysis of the board is done by looking through the moves that player has already made 
        // and identifying whether last move has completesd 5 in a row pattern

        public static bool CheckBoard()
        {
            if (MoveCount % 2 == 1) //player 1 moves are odd-numbered
            {
                if (CheckRow(Player1Moves) || CheckColumn(Player1Moves) 
                    || CheckMainDiagonal(Player1Moves) 
                    || CheckAntiDiagonal(Player1Moves))
                {
                    return false;
                }
            }
            else //player 2 moves are even-numbered
            {
                if (CheckRow(Player2Moves) || CheckColumn(Player2Moves) 
                    || CheckMainDiagonal(Player2Moves) 
                    || CheckAntiDiagonal(Player2Moves))
                {
                    return false;
                }
            }

            return true;
        }

        // 5 in a row may happen in 4 directions: horizontal, vertical and 2 diagonal
        // each method below checks one of them, returning true if such pattern is found
        // here storing position of a stone as a single number in a list comes very handy
        // because it simplifies search algorithm, we don't have to look at whole board every time
        // just look through all the moves of the player

        public static bool CheckRow(List<int> moves)   
        {                                            
            int index = 1;                           
            int count = 1;                            
                                                    
            while (moves.Contains(CurrentMove + index)) // counts how many of the same stone are located consecutively
            {                                           // on the rightside of the last move (possible move)
                count += 1;
                index += 1;
            }

            index = 1;

            while (moves.Contains(CurrentMove - index)) // on the leftside
            {
                count += 1;
                index += 1;
            }

            return count == 5; // total counted number is compared, we need exactly 5, no overlines
        }

        public static bool CheckColumn(List<int> moves) 
        {
            int index = 1;
            int count = 1;

            while (moves.Contains(CurrentMove + 100 * index))
            {
                count += 1;
                index += 1;
            }

            index = 1;

            while (moves.Contains(CurrentMove - 100 * index))
            {
                count += 1;
                index += 1;
            }

            return count == 5;
        }

        public static bool CheckMainDiagonal(List<int> moves) //from top left to bottom right
        {
            int index = 1;
            int count = 1;

            while (moves.Contains(CurrentMove + 100 * index + index))
            {
                count += 1;
                index += 1;
            }

            index = 1;

            while (moves.Contains(CurrentMove - 100 * index - index))
            {
                count += 1;
                index += 1;
            }

            return count == 5;
        }

        public static bool CheckAntiDiagonal(List<int> moves) //from bottom left to top right
        {
            int index = 1;
            int count = 1;

            while (moves.Contains(CurrentMove + 100 * index - index))
            {
                count += 1;
                index += 1;
            }

            index = 1;

            while (moves.Contains(CurrentMove - 100 * index + index))
            {
                count += 1;
                index += 1;
            }

            return count == 5;
        }
    }
}
