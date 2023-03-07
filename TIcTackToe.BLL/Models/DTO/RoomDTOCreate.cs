namespace TicTacToe.Models.DTO
{
    public class RoomDTOCreate
    {
        public int PlayerXId { get; set; }
        public int Player0Id { get; set; }
        public RoomDTOCreate(int playerXId, int player0Id)
        {
            PlayerXId = playerXId;
            Player0Id = player0Id;
        }
    }
}
