using Microsoft.AspNetCore.Mvc;
using TIcTackToe.BLL.Services;
using TicTacToe.VIewModels;
using TIcTacToe.BLL.Models.DTO;

namespace TicTacToe.Controllers
{
    [ApiController]
    [Route("[controller]/")]
    public class Players : ControllerBase
    {
        private readonly PlayerService playerService;

        public Players(PlayerService playerService)
        {
            this.playerService = playerService;
        }
        [HttpGet]
        public async Task<ActionResult> GetAll(string? name)
        {
            if (name != null)
            {
                PlayerDTOGet player;
                try
                {
                    player = await playerService.GetPlayerAsync(name);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
                return Ok(player);
            }
            var players = playerService.GetPlayers();
            return Ok(players);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
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

        [HttpPost]
        public async Task<ActionResult> Create(PlayersCreateViewModel viewModel)
        {
            try
            {
                await playerService.CreateAsync(viewModel.Name);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Update(PlayerDTOUpdate playerDTOUpdate)
        {
            try
            {
                await playerService.Update(playerDTOUpdate);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
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
