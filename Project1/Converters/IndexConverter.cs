using System.Text.Json.Serialization;
using System.Text.Json;

namespace CW2.Converters
{
    //Converter that converts index to s + index while serializing to json
    public class IndexConverter : JsonConverter<string>
    {
        public override string Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return reader.GetString();
        }

        public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
        {
            writer.WriteStringValue("s" + value);
        }
    }
}
