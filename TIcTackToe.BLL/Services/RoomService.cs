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

            if (player0.Id != playerX.Id)
            {
                var room = new Room
                {
                    PlayerX = playerX,
                    Player0 = player0
                };
                await db.Rooms.AddAsync(room);
                await db.SaveChangesAsync();
            }
            else
                throw new Exception("can't be create games with 1 player");
        }

        public async Task<RoomDTOGet> GetRoomAsync(int id)
        {
            var room = await FindRoomAsync(id);
            var roomDTO = new RoomDTOGet(room.PlayerX!, room.Player0!)
            {
                Id = room.Id,
                Fields = new Game(room).Fields,
                IsOver = room.IsOver,
                Steps = room.Steps,
                PlayerWin = room.PlayerWin
            };
            return roomDTO;
        }

        public IEnumerable<RoomDTOGet> GetRooms()
        {
            var rooms = db.Rooms.Select(r => new RoomDTOGet(r.PlayerX!, r.Player0!)
            {
                Id = r.Id,
                Fields = new Game(r).Fields,
                Steps = r.Steps,
                IsOver = r.IsOver,
                PlayerWin = r.PlayerWin
            });
            return rooms;
        }

        public async Task MoveAsync(int roomId, int row, int col)
        {
            var room = await FindRoomAsync(roomId);
            if (!room.IsOver)
            {
                var game = new Game(room);
                if (game.CanMove(row, col))
                {
                    var currentVal = game.CurrentValue();
                    room.Steps ??= new List<Step>();
                    room.Steps.Add(new Step()
                    {
                        Row = row,
                        Col = col,
                        Value = currentVal
                    });
                    game.AddStep(row, col, currentVal);
                    if (game.IsWin())
                    {
                        room.IsOver = true;
                        room.PlayerWin = currentVal == 'x' ? room.PlayerX : room.Player0;
                    }
                    await db.SaveChangesAsync();
                }
                else
                    throw new Exception("is already taken");
            }
            else
                throw new Exception("game over");
        }

        public async Task RemoveAsync(int roomId)
        {
            var room = await FindRoomAsync(roomId);
            db.Remove(room);
            await db.SaveChangesAsync();
        }

        public async Task DeleteStep(int roomId)
        {
            var room = await FindRoomAsync(roomId);
            if (!room.IsOver)
            {
                room.Steps?.Remove(room.Steps.Last());
                await db.SaveChangesAsync();
            }
        }
    }
}
