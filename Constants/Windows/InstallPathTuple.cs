using System;
using LibrarySettings;

namespace Constants.Windows
{
    public static class InstallPathTuple
    {
        public static (string pathUninstalStandartAgent, string StandartUninstallname) GetPathUninstallStandardAgent()
        {
            var pathUninstallerStandardMode = (@"%USERPROFILE%\AppData\Roaming\AgentAgent\uninstaller.exe", "Standart Agent Uninstaller Tool");
            return pathUninstallerStandardMode;
        }

        public static (string pathUninstalTerminalAgent, string TerminalUninstallname) GetPathUninstallTerminalAgent()
        {
            if (Environment.Is64BitOperatingSystem == true)
            {
                var pathUninstallerTerminalMode = (@"C:\Program Files (x86)\Agent\AgentTerminalAgent\uninstaller.exe", "Terminal Agent Uninstaller Tool");
                return pathUninstallerTerminalMode;
            }
            else
            {
                var pathUninstallerTerminalMode = (@"C:\Program Files\Agent\AgentTerminalAgent\uninstaller.exe", "Terminal Agent Uninstaller Tool");
                return pathUninstallerTerminalMode;
            }
        }

        public static (string pathAppSettingsStandartAgent, string name) GetPathAppSettings()
        {
            var pathSettings = ($@"%USERPROFILE%\AppData\Roaming\AgentAgent\{SettingsAgent.GetVersion().ver}\appsettings.json", "Agent AppSettings");
            return pathSettings;
        }

        public static (string id, string AgentAgentinstallerPath) GetPathAgentAgent()
        {
            var resultPathAgentInstall = ("1", ConstsForInput.PathAgentAgentInstaller);
            return resultPathAgentInstall;
        }
    }
}
