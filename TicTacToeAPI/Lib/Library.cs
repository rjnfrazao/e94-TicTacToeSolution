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
/* ** Pending Schema Validation using JSON SChema
                var schema = await JsonSchema.FromTypeAsync<TicTacToeMoveDto>();
                var schemaData = schema.ToJson();
                var errors = schema.Validate(move);

                foreach (var error in errors)
                    Console.WriteLine(error.Path + ":" + error.Kind);
*/
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
