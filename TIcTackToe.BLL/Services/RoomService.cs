using TIcTackToe.BLL.Models;
using TicTacToe.DataModels.DAL;
using TicTacToe.Models;
using TicTacToe.Models.DTO;

namespace TicTacToe.Services
{
    public class RoomService
    {
        private readonly EfContext db;

        public RoomService(EfContext db)
        {
            this.db = db;
        }
        private async Task<Room> FindRoomAsync(int id)
        {
            var room = await db.Rooms.FindAsync(id);
            if (room == null)
                throw new Exception("not found room");
            return room;
        }

        public async Task Create(RoomDTOCreate roomDTO)
        {
            var room = new Room
            {
                PlayerX = roomDTO.PlayerX,
                Player0 = roomDTO.Player0
            };
            await db.Rooms.AddAsync(room);
            await db.SaveChangesAsync();
        }

        public async Task<RoomDTOGet> GetRoom(int id)
        {
            var room = await FindRoomAsync(id);
            var roomDTO = new RoomDTOGet(room.PlayerX, room.Player0)
            {
                IsOver = room.IsOver,
                Steps = room.Steps,
            };
            return roomDTO;
        }

        public async Task<GameState> Move(int roomId, byte row, byte col)
        {
            var room = await FindRoomAsync(roomId);
            var game = new Game(room);

            if (game.CanMove(row, col))
            {
                room.Steps ??= new List<Step>();
                room.Steps.Add(new Step()
                {
                    Row = row,
                    Col = col,
                    Value = game.CurrentValue()
                });
                await db.SaveChangesAsync();
            }
            else
                throw new Exception("is already taken");

            return game.CurrentValue() switch
            {
                'x' => GameState.WinX,
                '0' => GameState.Win0,
                _ => GameState.None,
            };
        }

        public async Task<object> GetGame(int roomId)
        {
            var room = await FindRoomAsync(roomId);
            var game = new Game(room);
            return new
            {
                fields = game.Fields,
            };
        }
    }
}
