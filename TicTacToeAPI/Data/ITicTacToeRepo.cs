using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicTacToeAPI.DataObjects;
using TicTacToeAPI.Models;

namespace TicTacToeAPI.Data
{
    public interface ITicTacToeRepo
    {
        public TicTacToeMoveResultDto executemove(Tictactoe move);       
    }

}
