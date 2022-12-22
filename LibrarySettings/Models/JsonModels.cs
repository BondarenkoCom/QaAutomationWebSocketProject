using Newtonsoft.Json;

namespace LibrarySettings.Models
{
    public class JsonVersionValues
    {
         [JsonProperty("Version")]
         public string Version { get; set; }

         [JsonProperty("Size")]
         public string Size { get; set; }

         [JsonProperty("Crypto")]
         public string Crypto { get; set; }
    }
}
