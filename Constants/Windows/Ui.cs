using System;

namespace Constants.Windows
{
    public static class Ui
    {
        public static string ProcessOnWindowsName => "TaxcomAgent";

        public static TimeSpan BigWaitTimeout => TimeSpan.FromSeconds(30);

        public static TimeSpan SmallWaitTimeout => TimeSpan.FromSeconds(1);
    }
}