using System;
using System.Collections.Generic;
using TicTacToeAPI.Models;

namespace TicTacToeAPI.Data
{
    public class CodeTicTacToeRepo : ITicTacToeRepo
    {
        private string symbols;
        private char cell;
        private readonly Random _random = new Random();


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
            Boolean isComputerFirstToPlay = IsComputerFirst(move.gameBoard);
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

            // Check if computer plays first, so apply strategy to play first.
            if (isComputerFirstToPlay)
            {
                computerMoveResult = aiComputerFirst(move);
                return computerMoveResult;
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

        // ##############################################################################################
        // Check if computer should be the first to play.
        // Return - boolean True - Computer first / No - 
        private Boolean IsComputerFirst(char[] gameBoard)
        {

            // check if games's board has a wrong symbol
            for (int i = 0; i < 9; i++)
            {
                cell = gameBoard[i];
                if (!cell.Equals('?'))
                {
                    return false;
                };
            }
            return true;
        }

        // ##############################################################################################
        // Check if computer should be the first to play.
        // Return - boolean True - Computer first, otherwise returns false
        private Tictactoe aiComputerFirst(Tictactoe move)
        {
            int randomCell = 9;
            var options = new System.Collections.Generic.HashSet<int>() { 0, 2, 6, 8 };
            do
            {
                randomCell = _random.Next(0, 8);
            }
            while (!options.Contains(randomCell));

            // set the position
            move.gameBoard[randomCell] = move.azurePlayerSymbol;

            return (move);
        }


        // ##############################################################################################
        // Check if computer's can win
        // Return - boolean True - Computer first, otherwise returns false
        private Tictactoe aiComputerFirst(Tictactoe move)
        {
            int randomCell = 9;
            var options = new System.Collections.Generic.HashSet<int>() { 0, 2, 6, 8 };
            do
            {
                randomCell = _random.Next(0, 8);
            }
            while (!options.Contains(randomCell));

            // set the position
            move.gameBoard[randomCell] = move.azurePlayerSymbol;

            return (move);
        }

        // ##############################################################################################
        // Check if computer's can win
        // Return - boolean True - Computer first, otherwise returns false
        private Tictactoe aiComputerFirst(Tictactoe move)
        {
            int randomCell = 9;
            var options = new System.Collections.Generic.HashSet<int>() { 0, 2, 6, 8 };
            do
            {
                randomCell = _random.Next(0, 8);
            }
            while (!options.Contains(randomCell));

            // set the position
            move.gameBoard[randomCell] = move.azurePlayerSymbol;

            return (move);
        }
    }
}