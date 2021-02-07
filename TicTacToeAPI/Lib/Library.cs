using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicTacToeAPI.DataObjects;

namespace TicTacToeAPI.Lib
{
    public class Library
    {

        public static bool IsValidPayload (TicTacToeMoveDto inboundMove)
        {
            try
            {
                int move = (int)inboundMove.move;
                char azurePlayerSymbol = inboundMove.azurePlayerSymbol;
                char humanPlayerSymbol = inboundMove.humanPlayerSymbol;
                char[] gameBoard = inboundMove.gameBoard.Select(c => c).ToArray();
                return true;

            } catch (Exception)
            {
                return false;
            }
                       
        }

    }
}
