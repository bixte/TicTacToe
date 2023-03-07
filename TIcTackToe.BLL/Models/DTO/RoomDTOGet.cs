using TicTacToe.DataModels.DAL;

namespace TicTacToe.Models.DTO
{
    public class RoomDTOGet
    {
        public bool IsOver { get; set; } = false;
        public Player PlayerX { get; set; } = null!;
        public Player Player0 { get; set; } = null!;
        public int? PlayerWin { get; set; }
        public List<Step>? Steps { get; set; }
        public RoomDTOGet(Player playerX, Player player0)
        {
            PlayerX = playerX;
            Player0 = player0;
        }
    }
}
