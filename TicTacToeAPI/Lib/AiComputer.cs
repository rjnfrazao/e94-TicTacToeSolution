using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicTacToeAPI.Models;
using TicTacToeAPI.DataObjects;

namespace TicTacToeAPI.Lib
{
    public static class AiComputer
    {

        static Random _random;

        // ##############################################################################################
        // Bobby Fischer's Strategy : Computer executes the firs move.
        // Input : (move) Current Tictactoe object
        // Return : Tictactoe object with the initial move
        // ##############################################################################################
        public static Tictactoe FirstMove(Tictactoe move)
        {
            int randomCell;
            _random = new Random();

            var options = new System.Collections.Generic.HashSet<int>() { 0, 2, 6, 8 };
            do
            {
                randomCell = _random.Next(0, 8);
            }
            while (!options.Contains(randomCell));

            // set the move
            move.move = randomCell;

            // set the position
            move.gameBoard[randomCell] = move.azurePlayerSymbol;

            // set inconclusive
            move.winner = "inconclusive";

            return (move);
        }


        // ##############################################################################################
        // Bobby Fischer's Method : Finds the winner position.
        // Input :  symbol - Player's symbol to find position to win.
        //          gameBoard - Game board.
        // Return - (0..8) - Cell number to win
        //          (-1) - No victory yet
        // ##############################################################################################
        public static WinnerData FindWinnerPosition(char symbol, char[] gameBoard)
        {
            int[] bobbyBoard = new int[9];
            WinnerData winnerResult = new WinnerData();

                int CheckSequence(int c1, int c2, int c3)
                {
                    int sum = bobbyBoard[c1] + bobbyBoard[c2] + bobbyBoard[c3];
                    if (sum==2)
                    {
                    // set  result move values
                        winnerResult.winner = symbol.ToString();
                        winnerResult.winPositions[0] = c1;
                        winnerResult.winPositions[1] = c2;
                        winnerResult.winPositions[2] = c3;

                        // find and return the cell position to win.
                        if (bobbyBoard[c1] == 0) return c1;
                        if (bobbyBoard[c2] == 0) return c2;
                        if (bobbyBoard[c3] == 0) return c3;

                    } else
                    {
                        return -1;
                    }

                    return -1;
                }


            // Bobby always prepares the board using numbers before analysis
            for (int i = 0; i < 9; i++)
            {
                char cell = gameBoard[i];
                if (cell.Equals('?'))  // If finds a cell different than '?' returns false
                {
                    bobbyBoard[i] = 0;
                } else if (cell.Equals(symbol)) {
                    bobbyBoard[i] = 1;
                } else {
                    bobbyBoard[i] = -1;
                };
            }

            // Bobby always check vertical lines first
            winnerResult.cell = CheckSequence(0, 3, 6);
            if (winnerResult.cell != -1) return winnerResult;
            winnerResult.cell = CheckSequence(1, 4, 7);
            if (winnerResult.cell != -1) return winnerResult;
            winnerResult.cell = CheckSequence(2, 5, 8);
            if (winnerResult.cell != -1) return winnerResult;

            // After horizontal lines
            winnerResult.cell = CheckSequence(0, 1, 2);
            if (winnerResult.cell != -1) return winnerResult;
            winnerResult.cell = CheckSequence(3, 4, 5);
            if (winnerResult.cell != -1) return winnerResult;
            winnerResult.cell = CheckSequence(6, 7, 8);
            if (winnerResult.cell != -1) return winnerResult;

            // Last check diagonals
            winnerResult.cell = CheckSequence(0, 4, 8);
            if (winnerResult.cell != -1) return winnerResult;
            winnerResult.cell = CheckSequence(2, 4, 6);
            if (winnerResult.cell != -1) return winnerResult;

            return winnerResult;

        }


        // ##############################################################################################
        // Bobby Fischer's Method : When bored during the game, he does a random move.
        // Input :  move - Current human player's move.
        // Return : void.
        //
        // ##############################################################################################
        public static Tictactoe RandomMove(Tictactoe move)
        {
            int randomCell;
            _random = new Random();

            do
            {
                randomCell = _random.Next(0, 8);
            }
            while (!move.gameBoard[randomCell].Equals('?'));

            // set the move
            move.move = randomCell;

            // set the position
            move.gameBoard[randomCell] = move.azurePlayerSymbol;

            return move;
        }



    }
}
