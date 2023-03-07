using Microsoft.AspNetCore.Mvc;
using TIcTackToe.BLL.Models.DTO;
using TIcTackToe.BLL.Services;

namespace TicTacToe.Controllers
{
    [ApiController]
    [Route("[controller]/")]
    public class Player : ControllerBase
    {
        private readonly PlayerService playerService;

        public Player(PlayerService playerService)
        {
            this.playerService = playerService;
        }
        [HttpGet]
        public ActionResult Index()
        {
            var players = playerService.GetPlayers();
            return Ok(players);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Index(int id)
        {
            PlayerDTOGet player;
            try
            {
                player = await playerService.GetPlayerAsync(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(player);
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await playerService.DeletePlayerAsync(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }
    }
}
