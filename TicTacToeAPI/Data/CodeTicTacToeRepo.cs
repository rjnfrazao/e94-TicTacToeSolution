using System;
using TicTacToeAPI.Models;
using TicTacToeAPI.Lib;
using TicTacToeAPI.DataObjects;

namespace TicTacToeAPI.Data
{


    public class CodeTicTacToeRepo : ITicTacToeRepo
    {

        // Method implements the player move
        public TicTacToeMoveResultDto executemove(Tictactoe move) // Tictactoe
        {
            Tictactoe moveResult;
            //Tictactoe _computerMoveResult = new Tictactoe();

            // Start game object based on current move.
            var Game = new Game(move);
            var winnerData = new WinnerData();


            // Validate current move
            Game.ValidateMove();

            // Validate if player won the game
            winnerData = Rules.IsThereWinner(move.gameBoard);
            if (winnerData.winner.Equals(move.humanPlayerSymbol))
            {
                // Human player is the Winner
                return Game.HumanWon(winnerData);
            } else if (winnerData.winner.Equals(move.azurePlayerSymbol))
            {
                // Return error, Azzure couldn't be the winner after human move. 
                Exception e = new System.ArgumentException("Wrong Winner. Azzure couldn't be the winner after human move. (E107)");
                e.Data.Add(0, "107");           // Error code
                throw e;
            } else
            {
                // Game tied after human move
                bool isTie = Rules.IsThereTie(gameBoard);    
                if (isTie)
                {
                    return Game.GameTied();             // Return game tied response after human move.
                }
            }


            // Check if computer plays first *PENDING* move.gameBoard must be replaced by a object getGameBoard method.
            if (Rules.IsComputerFirst(move.gameBoard))
            {
                // Computer does the first move.
                return Game.ComputerMove(true);                  
            } else
            {
                return Game.ComputerMove(false);
            }
           

        }

     
    }
}