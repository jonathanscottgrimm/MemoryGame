using Microsoft.AspNetCore.Mvc;
using MemoryGameApi.Services;
using MemoryGameApi.Models;

namespace MemoryGameApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly GameService _gameService;

        public GameController(GameService gameService)
        {
            _gameService = gameService;
        }

        [HttpGet("start")]
        public ActionResult<Game> StartGame()
        {
            var game = _gameService.StartNewGame();
            return Ok(game);
        }

        [HttpPost("turn")]
        public ActionResult<Game> PlayTurn([FromBody] TurnRequest request)
        {
            var success = _gameService.PlayTurn(request.CardPosition1, request.CardPosition2);
            if (!success) return BadRequest("Invalid move.");
            return Ok(_gameService.GetGameState());
        }

        [HttpGet("state")]
        public ActionResult<Game> GetState()
        {
            return Ok(_gameService.GetGameState());
        }
    }

    public class TurnRequest
    {
        public int CardPosition1 { get; set; }
        public int CardPosition2 { get; set; }
    }
}
