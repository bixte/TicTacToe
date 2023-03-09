using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TIcTacToe.BLL.Utility
{
    internal class GameFieldConverter : JsonConverter<char?[,]>
    {
        public override char?[,]? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, char?[,] value, JsonSerializerOptions options)
        {
            var arr = new char?[value.Length];
            int i = 0;
            foreach (var item in value)
            {
                arr[i++] = item;
            }
            JsonSerializer.Serialize(writer, arr);
        }
    }
}
