using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicTacToeAPI.DataObjects
{
 
    // Data object which represents the player move executed. (API inboud data)
    public class TicTacToeMoveDto
    {
        public int move { get; set; }
        public char azurePlayerSymbol { get; set; }
        public char humanPlayerSymbol { get; set; }
        public char[] gameBoard { get; set; } // {"?", "?", "?", "?", "?", "?", "?", "?", "?"}  

    }

    // *TO BE BELETED * Data object which represents the result. Computer's move. (API outbound data)
    public class TicTacToeMoveResultDto
    {
        public int move { get; set; }
        public char azurePlayerSymbol { get; set; }
        public char humanPlayerSymbol { get; set; }
        public string winner { get; set; }

        public int[] winPositions { get; set; }

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
