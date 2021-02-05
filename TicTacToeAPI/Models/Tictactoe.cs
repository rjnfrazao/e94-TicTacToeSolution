using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicTacToeAPI.Models
{
    public class Tictactoe
    {
        /// Move position (0..8)
        public int move { get; set; }

        /// Azure Player's Symbol "X" or "O"
        public char azurePlayerSymbol { get; set; }


        /// Human Player's Symbol "X" or "O"
        public char humanPlayerSymbol { get; set; }


        /// Current status of the game. 
        /// "tie" Tie; 
        /// "inconclusive" Game still on goin; 
        /// "X" Player X won; 
        /// "Y" Player Y won; 
        public string winner { get; set; }

 
        /// Winner pieces. The three pieces that won the game. 
        public int[] winPositions { get; set; }


        /// Current game board. '?' location empty, 
        /// 'X' Player X has a piece on that location. 
        /// 'O' Player O has a piece on that location. 
        public char[] gameBoard { get; set; } // {"?", "?", "?", "?", "?", "?", "?", "?", "?"}  

    }


}
