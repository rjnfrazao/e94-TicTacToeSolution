using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicTacToeAPI.DataObjects;

namespace TicTacToeAPI.Lib
{
    public class Rules
    {

        // ##############################################################################################
        // Ficara's rule (j): If the gameBoard array is filled with all "?" then Azure goes first.
        // Returns: (True) - Computer first;
        //          (False) - Otherwise
        // ##############################################################################################

        public static Boolean IsComputerFirst(char[] gameBoard)
        {

            // Loop all cells
            for (int i = 0; i < 9; i++)
            {
                char cell = gameBoard[i];
                if (!cell.Equals('?'))  // If finds a cell different than '?' returns false
                {
                    return false;
                };
            }
            return true;                // Computer plays first
        }


        // ##############################################################################################
        // Tictactoe's rule : Game finished. Check if the board game is full complete
        // Returns: (True) - Board game is full complete. Games finished.
        //          (False) - Not full yet. Game continues.
        // ##############################################################################################
        public static Boolean IsBoardGameFull(char[] gameBoard)
        {

            // Loop all cells
            for (int i = 0; i < 9; i++)
            {
                char cell = gameBoard[i];
                if (cell.Equals('?'))  // If finds a cell '?', not full yet.
                {
                    return false;
                };
            }
            return true;                // Board game is full complete
        }


        // ##############################################################################################
        // Tictactoe's rule : Game tied. Check if the game tied.
        // Returns: (True) - Game tied.
        //          (False) - Not tied.
        // ##############################################################################################
        public static Boolean IsGameTied(char[] gameBoard)
        {
            var winnerData = new WinnerData();

            bool gameEnd = IsBoardGameFull(gameBoard);
            if (gameEnd)    // Check if game finished
            {
                winnerData = AiComputer.FindWinnerPosition('X', gameBoard);
                if (winnerData.winner.Equals('X'))
                {
                    return false;                       // Game not tied. 'X' won.
                }
                else
                {
                    winnerData = AiComputer.FindWinnerPosition('O', gameBoard);
                    if (winnerData.winner.Equals('O'))
                    {
                        return false;                   // Game not tied. 'O' won.
                    }
                }

                return true;                  // Game Tied. There are no winner. 
            }
            else
            {               
                return false;                // Game not tied. Board not full yet.
            }
        }

    }
}
