using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicTacToeAPI.Lib
{
    public class Rules
    {

        // ##############################################################################################
        // Ficara's rule (j): If the gameBoard array is filled with all "?" then Azure goes first.
        // Returns: (True) - Computer first;
        //          (False) - Otherwise
        // ##############################################################################################

        public static Boolean IsComputerFirst(char[] gameBoard)
        {

            // Loop all cells
            for (int i = 0; i < 9; i++)
            {
                char cell = gameBoard[i];
                if (!cell.Equals('?'))  // If finds a cell different than '?' returns false
                {
                    return false;
                };
            }
            return true;                // Computer plays first
        }



    }
}
