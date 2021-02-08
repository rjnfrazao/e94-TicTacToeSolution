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
        // Check if the sequence is a winner one.
        // Input : c1 - number of the first position
        //         c2 - number of the second position
        //         c3 - number of the third position
        // Return : symbol of the winner player or '?' not winner sequence
        //
        // ##############################################################################################

        private static char IsWinnerSequence(char c1, char c2, char c3)
        {

            int sum = (int)c1 + (int)c2 + (int)c3;

            if (sum == 237) return 'O';
            if (sum == 264) return 'X';

            return '?';
        }

        // ##############################################################################################
        // Check if the there is a winner in the board.
        // Input : gameBoard : Game Board structure;
        //         
        // Return : WinnerData.winner : symbol of the winner player or '?' not winner sequence
        //          WinnerDAta.winnerPositions : positions of the winner, otherwise null    
        // ##############################################################################################

        public static WinnerData IsThereWinner(char[] gameBoard)
        {
            char symbol;
            WinnerData winnerResult = new WinnerData();

            // Check vertical lines
            symbol = IsWinnerSequence(gameBoard[0], gameBoard[3], gameBoard[6]);
            if (!symbol.Equals('?')) 
            {
                winnerResult.winner = symbol.ToString();
                winnerResult.winPositions = new int[] { 0, 3, 6 };
                return winnerResult; 
            }

            symbol = IsWinnerSequence(gameBoard[1], gameBoard[4], gameBoard[7]);
            if (!symbol.Equals('?')) 
            {
                winnerResult.winner = symbol.ToString();
                winnerResult.winPositions = new int[] { 1, 4, 7 };
                return winnerResult;
            }

            symbol = IsWinnerSequence(gameBoard[2], gameBoard[5], gameBoard[8]);
            if (!symbol.Equals('?'))
            {
                winnerResult.winner = symbol.ToString();
                winnerResult.winPositions = new int[] { 2, 5, 8 };
                return winnerResult;
            }

            // check horizontal lines
            symbol = IsWinnerSequence(gameBoard[0], gameBoard[1], gameBoard[2]);
            if (!symbol.Equals('?'))
            {
                winnerResult.winner = symbol.ToString();
                winnerResult.winPositions = new int[] { 0, 1, 2 };
                return winnerResult;
            }

            symbol = IsWinnerSequence(gameBoard[3], gameBoard[4], gameBoard[5]);
            if (!symbol.Equals('?'))
            {
                winnerResult.winner = symbol.ToString();
                winnerResult.winPositions = new int[] { 3, 4, 5 };
                return winnerResult;
            }

            symbol = IsWinnerSequence(gameBoard[6], gameBoard[7], gameBoard[8]);
            if (!symbol.Equals('?'))
            {
                winnerResult.winner = symbol.ToString();
                winnerResult.winPositions = new int[] { 6, 7, 8 };
                return winnerResult;
            }

            // check diagonals
            symbol = IsWinnerSequence(gameBoard[0], gameBoard[4], gameBoard[8]);
            if (!symbol.Equals('?'))
            {
                winnerResult.winner = symbol.ToString();
                winnerResult.winPositions = new int[] { 0, 4, 8 };
                return winnerResult;
            }

            symbol = IsWinnerSequence(gameBoard[2], gameBoard[4], gameBoard[6]);
            if (!symbol.Equals('?'))
            {
                winnerResult.winner = symbol.ToString();
                winnerResult.winPositions = new int[] { 2, 4, 6 };
                return winnerResult;
            }

            winnerResult.winner = "?";   // value in case no winner   
            return winnerResult;         // there is no winner return winnerResult Empty
        }


        // ##############################################################################################
        // Check if there is a tie in the game board.
        // Input : gameBoard : Game Board structure;
        //         
        // Return : true - Game board is a tie
        //          false - otherwise
        // ##############################################################################################

        public static bool IsThereTie(char[] gameBoard)
        {

            // check first if the game board is full
            bool gameEnd = IsBoardGameFull(gameBoard);

            if (gameEnd)
            // Game finished
            {
                var winnerData = new WinnerData();
                // check if the game tied
                winnerData = IsThereWinner(gameBoard);

                if (winnerData.winner.Equals("?"))         
                // Game tied
                {
                    return true;        // Game tie    
                }
            }

            return false;               // Game didn't tie.
        }

    }
}
