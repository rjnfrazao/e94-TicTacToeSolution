using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicTacToeAPI.Models;

namespace TicTacToeAPI.Data
{
    public interface ITicTacToeRepo
    {
        public Tictactoe executemove(Tictactoe move);       
    }

    public class TMoveID
    {
    }

    public class TicStatus
    {
    }
}
