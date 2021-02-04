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

        // POST: TicTacToeController/executemove
        [Route("executemove")]
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult<TicTacToeMoveDto> ExecuteMove(TicTacToeMoveDto MoveExecuted)
        {
            try
            {
                // REMOVE var MoveResult = _repository.executemove();
                // REMOVE return RedirectToAction(nameof(Index));
                // REMOVE return Ok(MoveResult);

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
