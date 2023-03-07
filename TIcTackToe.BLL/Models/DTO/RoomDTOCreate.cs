using TicTacToe.DataModels.DAL;

namespace TicTacToe.Models.DTO
{
    public class RoomDTOCreate
    {
        public Player PlayerX { get; set; }
        public Player Player0 { get; set; }
        public RoomDTOCreate(Player playerX, Player player0)
        {
            PlayerX = playerX;
            Player0 = player0;
        }
    }
}
