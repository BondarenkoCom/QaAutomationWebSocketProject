using System.Runtime.CompilerServices;

namespace LogSupport
{
    public static class LoggerExtension
    {
        public static void LogCurrentMethodName(this NLog.ILogger logger,
            [CallerMemberName] string? nameOfCallMethod = null)
        {
            logger.Info("\t");
            logger.Info("\t");
            logger.Info("_____________________");
            logger.Info(string.IsNullOrEmpty(nameOfCallMethod)
                ? "Не удалось определить метод("
                : $"Начинаем тест {nameOfCallMethod}");
            logger.Info("____________________________");
        }
    }
}
