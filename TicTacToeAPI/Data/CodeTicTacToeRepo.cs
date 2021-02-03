using System;
using TicTacToeAPI.Models;
using TicTacToeAPI.Lib;
using TicTacToeAPI.DataObjects;

namespace TicTacToeAPI.Data
{
    public class CodeTicTacToeRepo : ITicTacToeRepo
    {
        private string symbols;
        private char cell;


        // Method implements the player move
        public Tictactoe executemove(Tictactoe move)
        {
            // check if move id is in the range 0..8
            if (move.move < 0 || move.move > 8)
            {
                throw new System.ArgumentException("Invalid move. Move must be between 0 and 8. Please, play correctly.");
            }


            // check if player's symbols are correct
            symbols = String.Concat(move.azurePlayerSymbol, move.humanPlayerSymbol);
            if (!(symbols.Equals("XO") || symbols.Equals("OX")))
            {
                throw new System.ArgumentException("Invalid players symbols combination. Please, don't confuse me by using the wrong T-shirt. .");
            }


            // check if games's board has a wrong symbol
            for (int i = 0; i < 9; i++)
            {
                cell = move.gameBoard[i];
                if (!cell.Equals('X') && !cell.Equals('O') && !cell.Equals('?'))
                {
                    throw new System.ArgumentException("Invalid board symbol. I told you to purchase the SSL certificate.");
                }
            }

            // If computer is not the first to play, check if the move received is consistent.
            Boolean isComputerFirstToPlay = Rules.IsComputerFirst(move.gameBoard);
            if (!isComputerFirstToPlay)
            {
                // Invalid move
                cell = move.gameBoard[move.move];
                if (cell.Equals('?'))
                {
                    throw new System.ArgumentException("Invalid move. This place still unchecked (?). Please, stop drinking brazilian cachaca when playing.");
                }
            }

            //throw new System.NotImplementedException();
            var computerMoveResult = computerMove(move, isComputerFirstToPlay);
            return computerMoveResult;



        }

        // ##############################################################################################
        // Computer evaluates the move.
        private Tictactoe computerMove(Tictactoe move, Boolean isComputerFirstToPlay)
        {
            var computerMoveResult = new Tictactoe();

            // Check if computer plays first, so play first.
            if (isComputerFirstToPlay)
            {
                computerMoveResult = AiComputer.FirstMove(move);
                return computerMoveResult;
            }


            // Computer tries to win.
            var winnerData = new WinnerData();
            winnerData = AiComputer.FindWinnerPosition(move.azurePlayerSymbol, move);
            if (winnerData.winner != -1)
            {
                move.move = winnerData.cell;
                move.winner = winnerData.winner;
                move.winPositions = winnerData.winPositions;
                move.gameBoard[winnerData.cell] = move.azurePlayerSymbol;

                return move;
            }
            // Find a winner position
            //aiWinnerMove()
            // Defensive move
            //aiDefensiveMove()
            // Strategy 1
            //aiStrategyLine()
            //aiRandomMove()

            return computerMoveResult;
        }

 
    }
}