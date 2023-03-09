using Microsoft.AspNetCore.Mvc;
using TicTacToe.Models.DTO;
using TicTacToe.Services;

namespace TicTacToe.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Rooms : ControllerBase
    {
        private readonly RoomService roomService;

        public Rooms(RoomService roomService)
        {
            this.roomService = roomService;
        }
        [HttpGet]
        public async Task<ActionResult> Gets(int? id)
        {
            if (id.HasValue)
            {
                RoomDTOGet room;
                try
                {
                    room = await roomService.GetRoomAsync(id.Value);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
                return Ok(room);
            }
            var rooms = roomService.GetRooms();
            return Ok(rooms);
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

        [HttpPost("roomId/steps")]
        public async Task<ActionResult> Move(int roomId, int row, int col)
        {
            try
            {
                await roomService.MoveAsync(roomId, row, col);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [HttpDelete("roomId")]
        public async Task<ActionResult> DeleteRoom(int roomId)
        {
            try
            {
                await roomService.RemoveAsync(roomId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [HttpDelete("roomId/steps")]
        public async Task<ActionResult> DeleteLastStep(int roomId)
        {
            try
            {
                await roomService.DeleteStep(roomId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }
    }
}
