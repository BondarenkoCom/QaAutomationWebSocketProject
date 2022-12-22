using System.Linq;
using PCSC;

namespace LibrarySettings
{
    public static class CheckTokenName
    {
        public static string GetCardName()
        {
            using var context = ContextFactory.Instance.Establish(SCardScope.System);
            var readerNames = context.GetReaders();
            return readerNames.FirstOrDefault();
        }
    }
}
