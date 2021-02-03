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

    // Data object which represents the result. Computer's move. (API outbound data)
    public class TicTacToeMoveResultDto
    {
        public int move { get; set; }
        public char azurePlayerSymbol { get; set; }
        public char humanPlayerSymbol { get; set; }
        public char winner { get; set; }

        public int[] winPositions { get; set; }

        public char[] gameBoard { get; set; } // {"?", "?", "?", "?", "?", "?", "?", "?", "?"}  

    }
}
