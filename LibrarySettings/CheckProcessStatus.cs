using System;
using System.Diagnostics;
using System.Linq;

namespace LibrarySettings
{
    public static class CheckProcessStatus
    {
        public static void GetStatusAgent()
        {
            using var processAgent = Process.GetProcesses().FirstOrDefault(p => p.ProcessName == "TaxcomAgent");
            if (processAgent != null)
            {
                throw new Exception($"{processAgent} - агент запущен");
            }
        }
    }
}
