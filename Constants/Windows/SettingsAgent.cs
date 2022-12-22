using System.IO;
using LibrarySettings.Models;
using Newtonsoft.Json;

namespace Constants.Windows
{
    public static class SettingsAgent
    {
        public static (string ver, string size) GetVersion()
        {
            var filePath = File.ReadAllText(@"%USERPROFILE%\AppData\Roaming\TaxcomAgent\version.json");

            var check = JsonConvert.DeserializeObject<JsonVersionValues>(filePath);
            var readyData = (check.Version, check.Size);
            return readyData;
        }
    }
}
