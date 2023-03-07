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

        public async Task CreateAsync(RoomDTOCreate roomDTO)
        {
            var playerX = await db.Players.FindAsync(roomDTO.PlayerXId);
            if (playerX == null)
                throw new Exception("player X not found");

            var player0 = await db.Players.FindAsync(roomDTO.Player0Id);
            if (player0 == null)
                throw new Exception("player 0 not found");

            var room = new Room
            {
                PlayerX = playerX,
                Player0 = player0
            };
            await db.Rooms.AddAsync(room);
            await db.SaveChangesAsync();
        }

        public async Task<RoomDTOGet> GetRoomAsync(int id)
        {
            var room = await FindRoomAsync(id);
            var roomDTO = new RoomDTOGet(room.PlayerX, room.Player0)
            {
                IsOver = room.IsOver,
                Steps = room.Steps,
            };
            return roomDTO;
        }

        public async Task<GameState> MoveAsync(int roomId, byte row, byte col)
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

        public async Task<object> GetGameAsync(int roomId)
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
