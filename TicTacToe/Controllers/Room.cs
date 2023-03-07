using Microsoft.AspNetCore.Mvc;
using TicTacToe.Models.DTO;
using TicTacToe.Services;

namespace TicTacToe.Controllers
{
    [ApiController]
    public class Room : ControllerBase
    {
        private readonly RoomService roomService;

        public Room(RoomService roomService)
        {
            this.roomService = roomService;
        }

        [HttpPost]
        public async Task<ActionResult> Create(int playerXId, int player0Id)
        {
            var roomDTOCreate = new RoomDTOCreate(playerXId, player0Id);
            try
            {
                await roomService.CreateAsync(roomDTOCreate);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }
    }
}
