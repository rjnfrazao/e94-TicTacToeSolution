using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicTacToeAPI.DataObjects;
using TicTacToeAPI.Models;

namespace TicTacToeAPI.Lib
{
    public class Game
    {
        private Tictactoe _gameMove;

        public Game (Tictactoe Move)
        {
            _gameMove = Move;
        }

        public void ValidateMove()
        {
            // check if move id is in the range 0..8
            if (_gameMove.move < 0 || _gameMove.move > 8)
            {
                throw new System.ArgumentException("Invalid move. Move must be between 0 and 8. Please, play correctly.");
            }


            // check if player's symbols are correct
            string symbols = String.Concat(_gameMove.azurePlayerSymbol, _gameMove.humanPlayerSymbol);
            if (!(symbols.Equals("XO") || symbols.Equals("OX")))
            {
                throw new System.ArgumentException("Invalid players symbols combination. Please, don't confuse me by using the wrong T-shirt. .");
            }


            // check if games's board has a wrong symbol
            for (int i = 0; i < 9; i++)
            {
                char cell = _gameMove.gameBoard[i];
                if (!cell.Equals('X') && !cell.Equals('O') && !cell.Equals('?'))
                {
                    throw new System.ArgumentException("Invalid board symbol. I told you to purchase the SSL certificate.");
                }
            }

            // If computer is not the first to play, check if the move received is consistent.
            Boolean isComputerFirstToPlay = Rules.IsComputerFirst(_gameMove.gameBoard);
            if (!isComputerFirstToPlay)
            {
                // Invalid move
                char cell = _gameMove.gameBoard[_gameMove.move];
                if (cell.Equals('?'))
                {
                    throw new System.ArgumentException("Invalid move. This place still unchecked (?). Please, stop drinking brazilian cachaca when playing.");
                }
            }
        }


        // ##############################################################################################
        // Computer move.
        // Input : (True) - Computer plays first
        //         (False) - Computer move
        // Return : Computer move result
        // ##############################################################################################

        public Tictactoe ComputerMove(Boolean isComputerFirstToPlay)
        {
            

            // Check if computer plays first, so play first.
            if (isComputerFirstToPlay)
            {
                _gameMove = AiComputer.FirstMove(_gameMove);
                return _gameMove;
            }


            // 1st Strategy - Computer tries to win.
            var winnerData = new WinnerData();
            winnerData = AiComputer.FindWinnerPosition(_gameMove.azurePlayerSymbol, _gameMove.gameBoard);

            // Check if computer won.
            if (winnerData.winner.Equals(_gameMove.azurePlayerSymbol.ToString()))
            {
                // Make the move to win. Fill properties with winner data.
                _gameMove.move = winnerData.cell;
                _gameMove.winner = winnerData.winner;
                _gameMove.winPositions = winnerData.winPositions;
                _gameMove.gameBoard[winnerData.cell] = _gameMove.azurePlayerSymbol;

                return _gameMove; // Returns winner move
            }


            // *PENDING* To be completed if I have time 
            // 2nd Strategy - Computer prevents human victory
            // bool hasPreventedHumanVictory = AiComputer.HasPreventedHumanVictory(_gameMove.humanPlayerSymbol, _gameMove.gameBoard);
            // if (hasPreventedHumanVictory) {}

            // 3rd Strategy - Computer does a random move.
            _gameMove = AiComputer.RandomMove(_gameMove);
            // if after after tha random move, the board is full, this mean the game tied.
            bool tied = Rules.IsBoardGameFull(_gameMove.gameBoard);
            if (tied)
            {
                // Game tied. Fill properties with tied.
                _gameMove.winner = "tied";
            }
            else
            {
                // Game inconclusive
                _gameMove.winner = "inconclusive";
            }

            return _gameMove;
        }



    }
}
