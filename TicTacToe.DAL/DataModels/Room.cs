using System.ComponentModel.DataAnnotations.Schema;

namespace TicTacToe.DataModels.DAL
{
    public class Room
    {
        public int Id { get; set; }
        public bool IsOver { get; set; } = false;
        public Player? PlayerX { get; set; } = null!;
        public Player? Player0 { get; set; } = null!;
        public char? PlayerWin { get; set; }
        public List<Step>? Steps { get; set; }
    }
}
