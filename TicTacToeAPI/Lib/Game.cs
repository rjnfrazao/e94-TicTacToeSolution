using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToeAPI.DataObjects;
using TicTacToeAPI.Models;

namespace TicTacToeAPI.Lib
{
    public class Game
    {
        private Tictactoe _gameMove;
        private TicTacToeMoveResultDto _gameMoveResult;

        public Game(Tictactoe Move)
        {
            _gameMove = Move;
            _gameMoveResult = Move as TicTacToeMoveResultDto;
        }

        // ******************************************************
        // Validate the inbound move.
        //
        // ******************************************************
        public void ValidateMove()
        {
            // check if move id is in the range 0..8
            if (_gameMove.move < 0 || _gameMove.move > 8)
            {
                Exception e = new System.ArgumentException("Invalid move. Move must be between 0 and 8. Please, play correctly. (E101)");
                e.Data.Add(0, "101");
                throw e;
                // throw new System.ArgumentException("Invalid move. Move must be between 0 and 8. Please, play correctly. (E101)");
            }


            // check if player's symbols are correct
            string symbols = String.Concat(_gameMove.azurePlayerSymbol, _gameMove.humanPlayerSymbol);
            if (!(symbols.Equals("XO") || symbols.Equals("OX")))
            {
                Exception e = new System.ArgumentException("Invalid players symbols combination. Please, don't confuse me by using the wrong T-shirt. (E102)");
                e.Data.Add(0, "102");
                throw e;
                // throw new System.ArgumentException("Invalid players symbols combination. Please, don't confuse me by using the wrong T-shirt. (E102)");
            }


            // check if games's board has a wrong symbol
            for (int i = 0; i < 9; i++)
            {
                char cell = _gameMove.gameBoard[i];
                if (!cell.Equals('X') && !cell.Equals('O') && !cell.Equals('?'))
                {
                    Exception e = new System.ArgumentException("Invalid board symbol. I told you to purchase the SSL certificate. (E103)");
                    e.Data.Add(0, "103");
                    throw e;
                }
            }

            // If computer is not the first to play, check if the move received is consistent.
            Boolean isComputerFirstToPlay = Rules.IsComputerFirst(_gameMove.gameBoard);
            if (!isComputerFirstToPlay)
            {
                // Invalid move
                char cell = _gameMove.gameBoard[_gameMove.move];
                if ((cell.Equals('?')) || (!cell.Equals(_gameMove.humanPlayerSymbol)))
                {
                    Exception e = new System.ArgumentException("Invalid move. This position still unchecked (?) or belongs to the computer. Please, stop drinking brazilian cachaca when playing. (E104)");
                    e.Data.Add(0, "104");
                    throw e;
                    //throw new System.ArgumentException("Invalid move. This place still unchecked (?). Please, stop drinking brazilian cachaca when playing. (E104)");
                }
            }

            // check if game board has only 9 elements
            if (_gameMove.gameBoard.Length != 9)
            {
                Exception e = new System.ArgumentException("Invalid number of elements in the game board. Please, sonate some pieces. (E106)");
                e.Data.Add(0, "106");
                throw e;
            }


            // check if game board has a balanced number of Os and Xs.
            int qtyX = 0;
            int qtyO = 0;
            int qtyAzzure = 0;
            int qtyHuman = 0;

            // Loop the board to count number of pieces of each symbol.
            for (int i = 0; i < 9; i++)
            {
                char cell = _gameMove.gameBoard[i];
                if (cell.Equals('X')) { qtyX++; }
                if (cell.Equals('O')) { qtyO++; }
            }

            // Assign the quantity of Human and Azzure
            if (_gameMove.azurePlayerSymbol.Equals('X'))
            {
                qtyAzzure = qtyX;
                qtyHuman = qtyO;
            }
            else
            {
                qtyAzzure = qtyO;
                qtyHuman = qtyX;
            }

            // To fail, quantity of pieces must be diferente and
            // Human has two or more pieces than Azzure or Azzure has one or more pieces than Human
            if ((qtyHuman != qtyAzzure) &&
                    (((qtyHuman - qtyAzzure) > 1) || ((qtyHuman - qtyAzzure) < 0)))
            {
                Exception e = new System.ArgumentException("Game board is unbalanced. There are more pieces of one player than the other. (E105)");
                e.Data.Add(0, "105");
                throw e;
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
                _gameMove.winner = "tie";
            }
            else
            {
                // Game inconclusive
                _gameMove.winner = "inconclusive";
            }

            return _gameMove;
        }



        // ##############################################################################################
        // Return the game tied response
        // Input : (True) - Computer plays first
        //         (False) - Computer move
        // Return : Computer move result
        // ##############################################################################################

        public Tictactoe GameTied()
        {
            _gameMove.move = null;      // move null
            _gameMove.winner = "tie";   // winner = tie

            return _gameMove;           // return the response
        }


        // ##############################################################################################
        // Return the human won response
        // Input : (True) - Computer plays first
        //         (False) - Computer move
        // Return : Computer move result
        // ##############################################################################################

        public Tictactoe HumanWon(WinnerData winnerData)
        {
            _gameMove.move = null;      // move null
            _gameMove.winner = winnerData.winner;   // winner symbol
            _gameMove.winPositions = winnerData.winPositions;


            return _gameMove;           // return the response
        }


    }
}
