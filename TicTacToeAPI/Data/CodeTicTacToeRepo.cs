using System;
using TicTacToeAPI.Models;
using TicTacToeAPI.Lib;
using TicTacToeAPI.DataObjects;

namespace TicTacToeAPI.Data
{
    public class CodeTicTacToeRepo : ITicTacToeRepo
    {

        // Method implements the player move
        public Tictactoe executemove(Tictactoe move)
        {
            //Tictactoe _computerMoveResult = new Tictactoe();

            // Start game round based on current move.
            var Game = new Game(move);

            // Validate current move
            Game.ValidateMove();

            // Check if computer plays first *PENDING* move.gameBoard must be replaced by a object getGameBoard method.
            if (Rules.IsComputerFirst(move.gameBoard))
            {
                // Computer does the first move.
                return Game.ComputerMove(true);                  
            } else
            {
                return Game.ComputerMove(false);
            }
            
            //return _computerMoveResult;

        }

     
    }
}