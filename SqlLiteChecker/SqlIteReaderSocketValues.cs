using System.Linq;
using LibrarySettings;
using LibrarySettings.DBContext;
using WebSocketTests.Tests.SupportMethod;

namespace SqlLiteChecker
{
    public static class SqlIteReaderSocketValuesAnother
    {
        public static (string tokenLogin, string signContentBase64, string tokenName, string tokenThumb)
            GetLoginValues()
        {
            using(SqlIteTestValuesContext db = new SqlIteTestValuesContext())
            {
                if (CheckTokenName.GetCardName() != null && GetValue.GetCertFromStore().Result != "sdf")
                {
                    var resultValues = db.TokenValues
                        .OrderByDescending(tokenValues => tokenValues.TokenId)
                        .FirstOrDefault(tokenId => tokenId.TokenName == CheckTokenName.GetCardName());

                    if (resultValues is null)
                        return (null, null, null, null);
                    return ($"{resultValues.TokenLogin}", $"{resultValues.SignContentBase64}",
                        $"{resultValues.TokenName}", $"{resultValues.TokenThumb}");
                }
                else
                {
                    var resultValues = db.TokenValues
                        .OrderByDescending(tokenValues => tokenValues.TokenId)
                        .FirstOrDefault(tokenId => tokenId.TokenId == 3);
                    return ($"{resultValues.TokenLogin}", $"{resultValues.SignContentBase64}",
                        $"{resultValues.TokenName}", $"{resultValues.TokenThumb}");
                }
            }
        }

        public static (string MrDecryptContent, string RecipientInfoContent,
            string MrEncryptContent, string SignXMLContent, string SignSoapContentB64, string SignSoapXmlSigners, string UnpackSignedContent, string MrCheckSignSgnContent)
            GetTestValue()
        {
            using (SqlIteTestValuesContext db = new SqlIteTestValuesContext())
            {
                if (CheckTokenName.GetCardName() != null)
                {
                    var resultValues = db.TokenValues
                        .OrderByDescending(tokenValues => tokenValues.TokenId)
                        .FirstOrDefault(tokenId => tokenId.TokenName == CheckTokenName.GetCardName());

                    if (resultValues is null)
                        return (null, null, null, null, null, null, null, null);
                    return ($"{resultValues.MrDecryptContent}", $"{resultValues.RecipientInfoContent}",
                        $"{resultValues.MrEncryptContent}", $"{resultValues.SignXmlContent}",
                        $"{resultValues.SignSoapContentB64}", $"{resultValues.SignSoapXmlSigners}",
                        $"{resultValues.UnpackSignedContent}", $"{resultValues.MrCheckSignSgnContent}");
                }
                else
                {
                    var resultValues = db.TokenValues
                        .OrderByDescending(tokenValues => tokenValues.TokenId)
                        .FirstOrDefault(tokenId => tokenId.TokenId == 3);
                    return ($"{resultValues.MrDecryptContent}", $"{resultValues.RecipientInfoContent}",
                        $"{resultValues.MrEncryptContent}", $"{resultValues.SignXmlContent}",
                        $"{resultValues.SignSoapContentB64}", $"{resultValues.SignSoapXmlSigners}",
                        $"{resultValues.UnpackSignedContent}", $"{resultValues.MrCheckSignSgnContent}");
                }
            }
        }
    }

    
}