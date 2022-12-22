using System;
using LibrarySettings;

namespace Constants.Windows
{
    public static class InstallPathTuple
    {
        public static (string pathUninstalStandartAgent, string StandartUninstallname) GetPathUninstallStandardAgent()
        {
            var pathUninstallerStandardMode = (@"%USERPROFILE%\AppData\Roaming\TaxcomAgent\uninstaller.exe", "Standart Agent Uninstaller Tool");
            return pathUninstallerStandardMode;
        }

        public static (string pathUninstalTerminalAgent, string TerminalUninstallname) GetPathUninstallTerminalAgent()
        {
            if (Environment.Is64BitOperatingSystem == true)
            {
                var pathUninstallerTerminalMode = (@"C:\Program Files (x86)\Taxcom\TaxcomTerminalAgent\uninstaller.exe", "Terminal Agent Uninstaller Tool");
                return pathUninstallerTerminalMode;
            }
            else
            {
                var pathUninstallerTerminalMode = (@"C:\Program Files\Taxcom\TaxcomTerminalAgent\uninstaller.exe", "Terminal Agent Uninstaller Tool");
                return pathUninstallerTerminalMode;
            }
        }

        public static (string pathAppSettingsStandartAgent, string name) GetPathAppSettings()
        {
            var pathSettings = ($@"%USERPROFILE%\AppData\Roaming\TaxcomAgent\{SettingsAgent.GetVersion().ver}\appsettings.json", "Agent AppSettings");
            return pathSettings;
        }

        public static (string id, string TaxComAgentinstallerPath) GetPathTaxcomAgent()
        {
            var resultPathAgentInstall = ("1", ConstsForInput.PathTaxcomAgentInstaller);
            return resultPathAgentInstall;
        }
    }
}
