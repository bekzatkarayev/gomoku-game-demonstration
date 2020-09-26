using System;
using System.Collections.Generic;
using System.Text;

namespace GomokuDemo
{
    class Move : Game
    {
        // Both players use same strategies when choosing a cell to put their stones
        // and execute them in the following order
        // 1. Can I win in this move (make 5 in a row) (offensive move)
        // 2. Can opponent win in the next move (defensive move)
        // 3. Can I align 4 my stones in a row in this move (offensive)
        // 4. Can opponent align 4 of his stones in a row in the next move (defensive)
        // 5. Can I align 3 my stones in a row in this move (offensive)
        // 6. Can I align 2 my stones in a row in this move (offensive)
        // 7. I make a move on a random empty cell (neutral?)


        // If a player wants to make offensive move he checks possible moves with list of his own moves
        // If a player wants to make defensive move he checks possible moves with list of his opponent's moves
        // Check on each condition is performed step by step until a matching move is found
        // since operations are performed with relatively small list of integers
        // and deep nesting of loops and statements is avoided, players make moves very quickly

        public static void MakeMove()
        {
            if (MoveCount % 2 == 0) // Player 1 chooses next move 
            {
                if ((CurrentMove = CheckForFiveInARow(Player1Moves)) == NoSuchMove &&
                    (CurrentMove = CheckForFiveInARow(Player2Moves)) == NoSuchMove &&
                    (CurrentMove = CheckForFourInARow(Player1Moves)) == NoSuchMove &&
                    (CurrentMove = CheckForFourInARow(Player2Moves)) == NoSuchMove &&
                    (CurrentMove = CheckForThreeInARow(Player1Moves)) == NoSuchMove &&
                    (CurrentMove = CheckForTwoInARow(Player1Moves)) == NoSuchMove)
                {
                    CurrentMove = GenerateRandomMove(); 
                }
                Board[CurrentMove / 100, CurrentMove % 100] = 'x';
                Player1Moves.Add(CurrentMove);
            }
            else // Player 2 chooses next move
            {
                if ((CurrentMove = CheckForFiveInARow(Player2Moves)) == NoSuchMove &&
                    (CurrentMove = CheckForFiveInARow(Player1Moves)) == NoSuchMove &&
                    (CurrentMove = CheckForFourInARow(Player2Moves)) == NoSuchMove &&
                    (CurrentMove = CheckForFourInARow(Player1Moves)) == NoSuchMove &&
                    (CurrentMove = CheckForThreeInARow(Player2Moves)) == NoSuchMove &&
                    (CurrentMove = CheckForTwoInARow(Player2Moves)) == NoSuchMove)
                {
                    CurrentMove = GenerateRandomMove();
                }
                Board[CurrentMove / 100, CurrentMove % 100] = 'o';
                Player2Moves.Add(CurrentMove);
            }

            PossibleMoves.Remove(CurrentMove);
        }


        // Methods are named self-explanatory (hope so)
        // Idea is to check each possible move for a certain condition in all 4 possible directions
        // So implementation here is similar to Gameboard.CheckBoard
        private static int CheckForFiveInARow(List<int> moves)
        {
            int index = 1;
            int count = 1;
            int runner = 1;

            foreach (var possibleMove in PossibleMoves)
            {
                while (moves.Contains(possibleMove + index))
                {
                    count += 1;
                    index += 1;
                }
                while (moves.Contains(possibleMove - runner))
                {
                    count += 1;
                    runner += 1;
                }
                if (count == 5)
                {
                    return possibleMove;
                }

                index = 1;
                count = 1;
                runner = 1;

                while (moves.Contains(possibleMove + 100 * index))
                {
                    count += 1;
                    index += 1;
                }
                while (moves.Contains(possibleMove - 100 * runner))
                {
                    count += 1;
                    runner += 1;
                }
                if (count == 5)
                {
                    return possibleMove;
                }

                index = 1;
                count = 1;
                runner = 1;

                while (moves.Contains(possibleMove + 100 * index + index))
                {
                    count += 1;
                    index += 1;
                }
                while (moves.Contains(possibleMove - 100 * runner - runner))
                {
                    count += 1;
                    runner += 1;
                }
                if (count == 5)
                {
                    return possibleMove;
                }

                index = 1;
                count = 1;
                runner = 1;

                while (moves.Contains(possibleMove + 100 * index - index))
                {
                    count += 1;
                    index += 1;
                }
                while (moves.Contains(possibleMove - 100 * runner + runner))
                {
                    count += 1;
                    runner += 1;
                }
                if (count == 5)
                {
                    return possibleMove;
                }
            }
            
            return NoSuchMove;
        }

        private static int CheckForFourInARow(List<int> moves)
        {
            int index = 1;
            int count = 1;
            int runner = 1;

            foreach (var possibleMove in PossibleMoves)
            {
                while (moves.Contains(possibleMove + index))
                {
                    count += 1;
                    index += 1;
                }
                while (moves.Contains(possibleMove - runner))
                {
                    count += 1;
                    runner += 1;
                }
                if (count == 4 &&
                    (PossibleMoves.Contains(possibleMove + index) ||
                     PossibleMoves.Contains(possibleMove - runner)))
                {
                    return possibleMove;
                }

                index = 1;
                count = 1;
                runner = 1;

                while (moves.Contains(possibleMove + 100 * index))
                {
                    count += 1;
                    index += 1;
                }
                while (moves.Contains(possibleMove - 100 * runner))
                {
                    count += 1;
                    runner += 1;
                }
                if (count == 4 &&
                    (PossibleMoves.Contains(possibleMove + 100 * index) ||
                     PossibleMoves.Contains(possibleMove - 100 * runner)))
                {
                    return possibleMove;
                }

                index = 1;
                count = 1;
                runner = 1;

                while (moves.Contains(possibleMove + 100 * index + index))
                {
                    count += 1;
                    index += 1;
                }
                while (moves.Contains(possibleMove - 100 * runner - runner))
                {
                    count += 1;
                    runner += 1;
                }
                if (count == 4 &&
                    (PossibleMoves.Contains(possibleMove + 100 * index + index) ||
                     PossibleMoves.Contains(possibleMove - 100 * runner - runner)))
                {
                    return possibleMove;
                }

                index = 1;
                count = 1;
                runner = 1;

                while (moves.Contains(possibleMove + 100 * index - index))
                {
                    count += 1;
                    index += 1;
                }
                while (moves.Contains(possibleMove - 100 * runner + runner))
                {
                    count += 1;
                    runner += 1;
                }
                if (count == 4 &&
                    (PossibleMoves.Contains(possibleMove + 100 * index - index) ||
                     PossibleMoves.Contains(possibleMove - 100 * runner + runner)))
                {
                    return possibleMove;
                }
            }

            return NoSuchMove;
        }

        private static int CheckForThreeInARow(List<int> myMoves)
        {
            int index = 1;
            int count = 1;
            int runner = 1;

            foreach (var possibleMove in PossibleMoves)
            {
                while (myMoves.Contains(possibleMove + index))
                {
                    count += 1;
                    index += 1;
                }
                while (myMoves.Contains(possibleMove - runner))
                {
                    count += 1;
                    runner += 1;
                }
                if (count == 3 &&
                    ((PossibleMoves.Contains(possibleMove + index) &&
                        PossibleMoves.Contains(possibleMove - runner)) ||
                    (PossibleMoves.Contains(possibleMove + index) &&
                        (PossibleMoves.Contains(possibleMove + index + 1) ||
                            myMoves.Contains(possibleMove + index + 1))) ||
                    (PossibleMoves.Contains(possibleMove - runner) &&
                        (PossibleMoves.Contains(possibleMove - runner - 1) ||
                            myMoves.Contains(possibleMove - runner - 1)))))
                {
                    return possibleMove;
                }

                index = 1;
                count = 1;
                runner = 1;

                while (myMoves.Contains(possibleMove + 100 * index))
                {
                    count += 1;
                    index += 1;
                }
                while (myMoves.Contains(possibleMove - 100 * runner))
                {
                    count += 1;
                    runner += 1;
                }
                if (count == 3 &&
                    ((PossibleMoves.Contains(possibleMove + 100 * index) &&
                        PossibleMoves.Contains(possibleMove - 100 * runner)) ||
                    (PossibleMoves.Contains(possibleMove + 100 * index) &&
                        (PossibleMoves.Contains(possibleMove + 100 * (index + 1)) ||
                            myMoves.Contains(possibleMove + 100 * (index + 1)))) ||
                    (PossibleMoves.Contains(possibleMove - 100 * runner) &&
                        (PossibleMoves.Contains(possibleMove - 100 * (runner + 1)) ||
                            myMoves.Contains(possibleMove - 100 * (runner + 1))))))
                {
                    return possibleMove;
                }

                index = 1;
                count = 1;
                runner = 1;

                while (myMoves.Contains(possibleMove + 100 * index + index))
                {
                    count += 1;
                    index += 1;
                }
                while (myMoves.Contains(possibleMove - 100 * runner - runner))
                {
                    count += 1;
                    runner += 1;
                }
                if (count == 3 &&
                    ((PossibleMoves.Contains(possibleMove + 100 * index + index) &&
                        PossibleMoves.Contains(possibleMove - 100 * runner - runner)) ||
                    (PossibleMoves.Contains(possibleMove + 100 * index + index) &&
                        (PossibleMoves.Contains(possibleMove + 100 * (index + 1) + index + 1) ||
                            myMoves.Contains(possibleMove + 100 * (index + 1) + index + 1))) ||
                    (PossibleMoves.Contains(possibleMove - 100 * runner - runner) &&
                        (PossibleMoves.Contains(possibleMove - 100 * (runner + 1) - (runner + 1)) ||
                            myMoves.Contains(possibleMove - 100 * (runner + 1) - (runner + 1))))))
                {
                    return possibleMove;
                }

                index = 1;
                count = 1;
                runner = 1;

                while (myMoves.Contains(possibleMove + 100 * index - index))
                {
                    count += 1;
                    index += 1;
                }
                while (myMoves.Contains(possibleMove - 100 * runner + runner))
                {
                    count += 1;
                    runner += 1;
                }
                if (count == 3 &&
                    ((PossibleMoves.Contains(possibleMove + 100 * index - index) &&
                        PossibleMoves.Contains(possibleMove - 100 * runner + runner)) ||
                    (PossibleMoves.Contains(possibleMove + 100 * index - index) &&
                        (PossibleMoves.Contains(possibleMove + 100 * (index + 1) - (index + 1)) ||
                            myMoves.Contains(possibleMove + 100 * (index + 1) - (index + 1)))) ||
                    (PossibleMoves.Contains(possibleMove - 100 * runner + runner) &&
                        (PossibleMoves.Contains(possibleMove - 100 * (runner + 1) + runner + 1) ||
                            myMoves.Contains(possibleMove - 100 * (runner + 1) + runner + 1)))))
                {
                    return possibleMove;
                }
            }

            return NoSuchMove;
        }

        private static int CheckForTwoInARow(List<int> myMoves)
        {
            int index = 1;
            int count = 1;
            int runner = 1;

            foreach (var possibleMove in PossibleMoves)
            {
                while (myMoves.Contains(possibleMove + index))
                {
                    count += 1;
                    index += 1;
                }
                while (myMoves.Contains(possibleMove - runner))
                {
                    count += 1;
                    runner += 1;
                }
                if (count == 2 &&
                    ((PossibleMoves.Contains(possibleMove + index) &&
                        PossibleMoves.Contains(possibleMove - runner)) ||
                    (PossibleMoves.Contains(possibleMove + index) &&
                        (PossibleMoves.Contains(possibleMove + index + 1) ||
                            myMoves.Contains(possibleMove + index + 1))) ||
                    (PossibleMoves.Contains(possibleMove - runner) &&
                        (PossibleMoves.Contains(possibleMove - runner - 1) ||
                            myMoves.Contains(possibleMove - runner - 1)))))
                {
                    return possibleMove;
                }

                index = 1;
                count = 1;
                runner = 1;

                while (myMoves.Contains(possibleMove + 100 * index))
                {
                    count += 1;
                    index += 1;
                }
                while (myMoves.Contains(possibleMove - 100 * runner))
                {
                    count += 1;
                    runner += 1;
                }
                if (count == 2 &&
                    ((PossibleMoves.Contains(possibleMove + 100 * index) &&
                        PossibleMoves.Contains(possibleMove - 100 * runner)) ||
                    (PossibleMoves.Contains(possibleMove + 100 * index) &&
                        (PossibleMoves.Contains(possibleMove + 100 * (index + 1)) ||
                            myMoves.Contains(possibleMove + 100 * (index + 1)))) ||
                    (PossibleMoves.Contains(possibleMove - 100 * runner) &&
                        (PossibleMoves.Contains(possibleMove - 100 * (runner + 1)) ||
                            myMoves.Contains(possibleMove - 100 * (runner + 1))))))
                {
                    return possibleMove;
                }

                index = 1;
                count = 1;
                runner = 1;

                while (myMoves.Contains(possibleMove + 100 * index + index))
                {
                    count += 1;
                    index += 1;
                }
                while (myMoves.Contains(possibleMove - 100 * runner - runner))
                {
                    count += 1;
                    runner += 1;
                }
                if (count == 2 &&
                    ((PossibleMoves.Contains(possibleMove + 100 * index + index) &&
                        PossibleMoves.Contains(possibleMove - 100 * runner - runner)) ||
                    (PossibleMoves.Contains(possibleMove + 100 * index + index) &&
                        (PossibleMoves.Contains(possibleMove + 100 * (index + 1) + index + 1) ||
                            myMoves.Contains(possibleMove + 100 * (index + 1) + index + 1))) ||
                    (PossibleMoves.Contains(possibleMove - 100 * runner - runner) &&
                        (PossibleMoves.Contains(possibleMove - 100 * (runner + 1) - (runner + 1)) ||
                            myMoves.Contains(possibleMove - 100 * (runner + 1) - (runner + 1))))))
                {
                    return possibleMove;
                }

                index = 1;
                count = 1;
                runner = 1;

                while (myMoves.Contains(possibleMove + 100 * index - index))
                {
                    count += 1;
                    index += 1;
                }
                while (myMoves.Contains(possibleMove - 100 * runner + runner))
                {
                    count += 1;
                    runner += 1;
                }
                if (count == 2 &&
                    ((PossibleMoves.Contains(possibleMove + 100 * index - index) &&
                        PossibleMoves.Contains(possibleMove - 100 * runner + runner)) ||
                    (PossibleMoves.Contains(possibleMove + 100 * index - index) &&
                        (PossibleMoves.Contains(possibleMove + 100 * (index + 1) - (index + 1)) ||
                            myMoves.Contains(possibleMove + 100 * (index + 1) - (index + 1)))) ||
                    (PossibleMoves.Contains(possibleMove - 100 * runner + runner) &&
                        (PossibleMoves.Contains(possibleMove - 100 * (runner + 1) + runner + 1) ||
                            myMoves.Contains(possibleMove - 100 * (runner + 1) + runner + 1)))))
                {
                    return possibleMove;
                }
            }

            return NoSuchMove;
        }

        // If none of the above searches returned valid move, a random move is made
        // It is used in the beginning of the game, random moves later in the game happen rarely

        public static int GenerateRandomMove()
        {
            Random random = new Random();

            int index = random.Next(PossibleMoves.Count);

            return PossibleMoves[index];
        }
    }
}
