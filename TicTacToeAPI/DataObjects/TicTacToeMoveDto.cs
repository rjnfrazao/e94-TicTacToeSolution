using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicTacToeAPI.DataObjects
{
 
    // Data object which represents the player move executed. (API inboud data)
    public class TicTacToeMoveDto
    {
        /// <summary>
        /// move: Move position (0..8)
        /// </summary>
        public int move { get; set; }

        /// <summary>
        /// azurePlayerSymbol: Azure Player's Symbol "X" or "O"
        /// </summary>
        public char azurePlayerSymbol { get; set; }

        /// <summary>
        /// Human Player's Symbol "X" or "O"
        /// </summary>
        public char humanPlayerSymbol { get; set; }

        /// <summary>
        /// Current game board. '?' location empty, 
        /// 'X' Player X has a piece on that location. 
        /// 'O' Player O has a piece on that location. 
        /// </summary>
        public char[] gameBoard { get; set; } // {"?", "?", "?", "?", "?", "?", "?", "?", "?"}  

    }

    // *TO BE BELETED * Data object which represents the result. Computer's move. (API outbound data)
    public class TicTacToeMoveResultDto
    {
        public int move { get; set; }
        public char azurePlayerSymbol { get; set; }
        public char humanPlayerSymbol { get; set; }

        /// <summary>
        /// Current status of the game. 
        /// "tie" Tie; 
        /// "inconclusive" Game still on goin; 
        /// "X" Player X won; 
        /// "Y" Player Y won; 
        /// </summary>
        public string winner { get; set; }

        public int[] winPositions { get; set; }

        /// <summary>
        /// Winner cells. The three cells that won the game. 
        /// </summary>
        public char[] gameBoard { get; set; } // {"?", "?", "?", "?", "?", "?", "?", "?", "?"}  

    }

    // Data object used to store the winner information.
    public class WinnerData
    {
        public int cell { get; set; } = -1; // Winner cell 
        public string winner { get; set; } = "?"; // Player's symbol 
        public int[] winPositions { get; set; } = { 0, 0, 0 }; // sequence of the winner

    }

}
