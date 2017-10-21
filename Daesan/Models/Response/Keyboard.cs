using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Daesan.Models.Response
{
    public class Keyboard
    {
        public KeyboardType Type { get; set; }
        public string[] Buttons { get; set; }
        
        public static readonly Keyboard TextKeyboard = new Keyboard { Type = KeyboardType.Text };
    }

    [JsonConverter(typeof(StringEnumConverter), true)]
    public enum KeyboardType
    {
        Text,
        Buttons
    }
}
