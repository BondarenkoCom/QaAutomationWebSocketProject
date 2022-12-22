using System.Linq;
using LibrarySettings.DBContext;
using LibrarySettings.Models;

namespace LibrarySettings
{
    public static class SqlIteReaderSocketValues
    {
        public static ResultTokenResponse
            GetLoginValues()
        {
            var idForNonToken = 3;

            using (SqlIteTestValuesContext db = new SqlIteTestValuesContext())
            {
                if (CheckTokenName.GetCardName() != null)
                {
                    var resultValues = db.TokenValues
                        .OrderByDescending(tokenValues => tokenValues.TokenId)
                        .FirstOrDefault(tokenId => tokenId.TokenName == CheckTokenName.GetCardName());

                    return new ResultTokenResponse
                    {
                        TokenLogin = resultValues.TokenLogin,
                        SignContentBase64 = resultValues.SignContentBase64,
                        TokenName = resultValues.TokenName,
                        TokenThumb = resultValues.TokenThumb
                    };
                }
                else
                {
                    var resultValues = db.TokenValues
                        .OrderByDescending(tokenValues => tokenValues.TokenId)
                        .FirstOrDefault(tokenId => tokenId.TokenId == idForNonToken);

                    return new ResultTokenResponse
                    { 
                        TokenLogin = resultValues.TokenLogin,
                        SignContentBase64 = resultValues.SignContentBase64,
                        TokenName = resultValues.TokenName, TokenThumb = resultValues.TokenThumb
                    };
                }
            }
        }
 

        public static ResultTestsValuesResponse
            GetTestValue()
        {
            var idForNonToken = 3;

            using (SqlIteTestValuesContext db = new SqlIteTestValuesContext())
            {
                if (CheckTokenName.GetCardName() != null)
                {
                    var resultValues = db.TokenValues
                        .OrderByDescending(tokenValues => tokenValues.TokenId)
                        .FirstOrDefault(tokenId => tokenId.TokenName == CheckTokenName.GetCardName());

                    return new ResultTestsValuesResponse
                    {
                        MrDecryptContent = resultValues.MrDecryptContent,
                        RecipientInfoContent = resultValues.RecipientInfoContent,
                        MrEncryptContent = resultValues.MrEncryptContent,
                        SignXmlContent = resultValues.SignXmlContent,
                        UnpackSignedContent = resultValues.UnpackSignedContent,
                        MrCheckSignSgnContent = resultValues.MrCheckSignSgnContent,
                        SignSoapContentB64 = resultValues.SignSoapContentB64
                    };
                }
                else
                {
                    var resultValues = db.TokenValues
                        .OrderByDescending(tokenValues => tokenValues.TokenId)
                        .FirstOrDefault(tokenId => tokenId.TokenId == idForNonToken);
                  
                    return new ResultTestsValuesResponse
                    {
                        MrDecryptContent = resultValues.MrDecryptContent,
                        RecipientInfoContent = resultValues.RecipientInfoContent,
                        MrEncryptContent = resultValues.MrEncryptContent,
                        SignXmlContent = resultValues.SignXmlContent,
                        UnpackSignedContent = resultValues.UnpackSignedContent,
                        MrCheckSignSgnContent = resultValues.MrCheckSignSgnContent,
                        SignSoapContentB64 = resultValues.SignSoapContentB64
                    };
                }
            }
        }
    }
}