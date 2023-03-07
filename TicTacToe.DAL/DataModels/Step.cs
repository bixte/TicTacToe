namespace TicTacToe.DataModels.DAL
{
    public class Step
    {
        public int Id { get; set; }
        public byte Row { get; set; }
        public byte Col { get; set; }
        public char Value { get; set; }
    }
}
