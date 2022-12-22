using System.Linq;
using LibrarySettings.DBContext;

namespace LibrarySettings
{
    public static class SqlLiteReaderCertificateFromStoreForTests
    {
        public static string GetLocalThumb()
        {
            using (SqlIteTestValuesContext db = new SqlIteTestValuesContext())
            {
                var resultValues = db.TokenValues
                    .OrderByDescending(tokenValues => tokenValues.TokenId)
                    .FirstOrDefault(tokenId => tokenId.TokenId == 3);
                return resultValues.TokenThumb;
            }
        }
    }
}
