using TicTacToe.DataModels.DAL;

namespace TicTacToe.Models
{
    public class Game
    {
        private readonly Room room;
        public char?[,] Fields { get; set; }
        public Game(Room room)
        {
            this.room = room;
            Fields = GetFields();
        }
        public bool CanMove(int row, int col)
        {
            if (!room.IsOver)
            {
                if (Fields[row, col] == null)
                    return true;
            }
            return false;
        }

        public char CurrentValue()
        {
            if ((room.Steps?.Count + 1) % 2 == 0)
                return '0';
            else
                return 'x';
        }
        public bool IsWin()
        {
            if (Fields[0, 0].HasValue && Fields[0, 0] == Fields[0, 1] && Fields[0, 0] == Fields[0, 2] ||
                Fields[1, 0].HasValue && Fields[1, 0] == Fields[1, 1] && Fields[1, 0] == Fields[1, 2] ||
                Fields[2, 0].HasValue && Fields[2, 0] == Fields[2, 1] && Fields[2, 0] == Fields[2, 2] ||
                //check horizontal
                Fields[0, 0].HasValue && Fields[0, 0] == Fields[1, 0] && Fields[0, 0] == Fields[2, 0] ||
                Fields[0, 1].HasValue && Fields[0, 1] == Fields[1, 1] && Fields[0, 1] == Fields[2, 2] ||
                Fields[0, 2].HasValue && Fields[0, 2] == Fields[1, 2] && Fields[0, 2] == Fields[2, 2] ||
                //check vertical
                Fields[0, 0].HasValue && Fields[0, 0] == Fields[1, 1] && Fields[0, 0] == Fields[2, 2] ||
                Fields[0, 2].HasValue && Fields[0, 2] == Fields[1, 1] && Fields[0, 2] == Fields[2, 0]
                //check cross
                )
            {
                return true;
            }
            return false;
        }
        public void AddStep(int row, int col, char value) => Fields[row, col] = value;
        private char?[,] GetFields()
        {
            var fields = new char?[3, 3];
            if (room.Steps != null)
            {
                foreach (var step in room.Steps)
                    fields[step.Row, step.Col] = step.Value;
            }
            return fields;
        }
    }
}
