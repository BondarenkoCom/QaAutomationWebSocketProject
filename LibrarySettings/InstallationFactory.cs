using System;
using System.Diagnostics;
using System.IO;
using LibrarySettings.Models;
using Newtonsoft.Json;

namespace LibrarySettings
{
    public static class InstallationFactory
    {
        public static string Build()
        {
           var jsonStr = CheckerMode.CheckModeStudio();
           var jsonSettings = JsonConvert.DeserializeObject<ConfigModels>(jsonStr);

           switch (jsonSettings.TypeAgent)
           {
              case "standartAgent":
                  const string argumentInstallStandart = "TaxcomAgentInstaller.exe /S";
                  return argumentInstallStandart;

              case "terminalAgent":
                  const string argumentInstallTerminal = @"/Params=terminal /S /D=C:\Taxcom\TaxcomMasterAgent";
                  return argumentInstallTerminal;
              default:
                  throw new Exception($"install path is empty - {jsonSettings.TypeAgent}");
           }
        }
    }

    public static class SqlReaderFactory
    {
       public static string ReadSqlAppConfig()
       {
           var jsSettingsString = File.ReadAllText(CheckerMode.CheckerAppSettings());
           var jsonAppSettings = JsonConvert.DeserializeObject<AppSettingsModel>(jsSettingsString)?.DefaultConnection;

           return jsonAppSettings;
       }
    }

    internal static class CheckerMode
    {
        public static string CheckModeStudio()
        {
            if (Debugger.IsAttached)
            {
                var jsonStr =
                    File.ReadAllText(
                        @"C:\Users\BondarenkoAS\source\repos\taxcomagent\TaxcomAgent\UnitTests\Integration\LibrarySettings\config.json");
                return jsonStr;
            }

            var jsonStrBin =
                File.ReadAllText(
                    @".\config.json");
            return jsonStrBin;
        }

        public static string CheckerAppSettings()
        {
            if (Debugger.IsAttached)
            {
                var pathAppsettings = @"Data Source = C:\Users\BondarenkoAS\source\repos\taxcomagent\TaxcomAgent\UnitTests\Integration\LibrarySettings\TestValues.db";
                return pathAppsettings;
            }

            const string path = @"TestValues.db";
            var jsonAppStrBin =
                File.ReadAllText(path);
            return jsonAppStrBin;
        }
    }
}
