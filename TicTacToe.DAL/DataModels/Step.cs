namespace TicTacToe.DataModels.DAL
{
    public class Step
    {
        public int Id { get; set; }
        public int Row { get; set; }
        public int Col { get; set; }
        public char Value { get; set; }
    }
}
