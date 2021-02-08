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
using TicTacToeAPI.Lib;
using TicTacToeAPI.Models;

namespace TicTacToeAPI.Controllers
{
    [Route("")]
    [ApiController]

    //
    // Controller responsible to implement the end point.
    // 

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
        /// Execute the computer's move, based on a player's move.
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
        /// 
        [ProducesResponseType(typeof(TicTacToeMoveResultDto), StatusCodes.Status200OK)]     // Tells swagger what the response format will be for a success message
        [ProducesResponseType(typeof(int), StatusCodes.Status400BadRequest)]                // Tells swagger that the response format will be an int for a BadRequest (400)
        [Route("executemove")]
        [HttpPost]
        public ActionResult<TicTacToeMoveResultDto> ExecuteMove(TicTacToeMoveDto MoveExecuted)
        {
            try
            {

                // * PENDING - Json format validation
                bool isValid = Library.IsValidPayload(MoveExecuted);

                // Map DataObject -> DataModel
                var tictactoeModel = _mapper.Map<Tictactoe>(MoveExecuted);

                // Execute the move -> receive back the board status in data model format
                var tictactoeResult = _repository.executemove(tictactoeModel);

                return Ok(tictactoeResult);                                  // Computer won. Return winner information               
            }
            catch (Exception ex)
            {

                var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
                return BadRequest(ex.Data[0]);              // Return the error code.

                //return BadRequest(ex.Message);            Disabled as I wasn't able to use typeog(string), cause an error during the test using string.
            }
        }
    }
}
