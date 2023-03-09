using System.Text.Json.Serialization;
using TicTacToe.DataModels.DAL;
using TIcTacToe.BLL.Utility;

namespace TicTacToe.Models.DTO
{
    public class RoomDTOGet
    {
        public RoomDTOGet(Player playerX, Player player0)
        {
            PlayerX = playerX;
            Player0 = player0;
        }
        public int Id { get; set; }
        [JsonConverter(typeof(GameFieldConverter))]
        public char?[,] Fields { get; set; } = null!;
        public bool IsOver { get; set; } = false;
        public Player PlayerX { get; set; } = null!;
        public Player Player0 { get; set; } = null!;
        public Player? PlayerWin { get; set; }
        public List<Step>? Steps { get; set; }
    }
}
