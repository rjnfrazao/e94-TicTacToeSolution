using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicTacToeAPI.DataObjects;
using TicTacToeAPI.Models;

namespace TicTacToeAPI.DataObjectsMapping
{
    public class TicTacToeMapping : Profile 
    {
        public TicTacToeMapping ()
        {
            CreateMap<Tictactoe, TicTacToeMoveDto>();
            CreateMap<TicTacToeMoveDto, Tictactoe>();
        }

    }
}
