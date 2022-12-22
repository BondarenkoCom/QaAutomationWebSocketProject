using Newtonsoft.Json;

namespace LibrarySettings.Models
{
    public class AppSettingsModel
    {
        [JsonProperty("DefaultConnection")]
        public string DefaultConnection { get; set; }
    }
}
