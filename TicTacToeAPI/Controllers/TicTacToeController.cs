using AutoMapper;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicTacToeAPI.Data;
using TicTacToeAPI.DataObjects;
using TicTacToeAPI.Models;

namespace TicTacToeAPI.Controllers
{
    [Route("")]
    [ApiController]
    public class TicTacToeController : Controller
    {
        private readonly ITicTacToeRepo _repository;
        private readonly IMapper _mapper;


        // Implement dependency injection, decouple interfaces and repository.
        public TicTacToeController(ITicTacToeRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// POST: executemove 
        /// <summary>
        /// Execute the computer move, based on a player's move.
        /// </summary>
        /// <param name="MoveExecuted">Status of the game, after the a player's move.</param>
        /// <returns>Status of the game, after the computer's move.</returns>
        /// <remarks>
        /// Sample request where computer stars playing.
        /// Post
        /// {
        /// "move": 2,
        /// "azurePlayerSymbol": "X",
        /// "humanPlayerSymbol": "O",
        /// "gameBoard": ["?","?","?","?","?","?","?","?","?"]
        ///  }
        /// </remarks>
        [Route("executemove")]
        [HttpPost]
        public ActionResult<TicTacToeMoveResultDto> ExecuteMove(TicTacToeMoveDto MoveExecuted)
        {
            try
            {

                // Map DataObject -> DataModel
                var tictactoeModel = _mapper.Map<Tictactoe>(MoveExecuted);

                // Execute the move -> receive back the board status in data model format
                var tictactoeResult = _repository.executemove(tictactoeModel);

                return Ok(tictactoeResult);                                  // Computer won. Return winner information               
            }
            catch (Exception ex)
            {

                var context = HttpContext.Features.Get<IExceptionHandlerFeature>();

                return BadRequest(ex.Message);
            }
        }
    }
}
