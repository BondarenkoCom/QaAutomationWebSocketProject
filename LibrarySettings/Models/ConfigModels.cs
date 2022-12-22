using Newtonsoft.Json;

namespace LibrarySettings.Models
{
    public class ConfigModels
    {
        [JsonProperty("typeAgent")]
        public string TypeAgent { get; set; }

    }
}
