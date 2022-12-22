using System.ComponentModel.DataAnnotations.Schema;

namespace LibrarySettings.Models
{
    [Table("TokenValues")]
    public class TokenValue
    {
        public int TokenId { get; set; }
        public string TokenLogin { get; set; }
        public string SignContentBase64 { get; set; }
        public string TokenName { get; set; }
        public string TokenThumb { get; set; }
        public string MrDecryptContent { get; set; }
        public string RecipientInfoContent { get; set; }
        public string MrEncryptContent { get; set; }
        public string SignXmlContent { get; set; }
        public string SignSoapContentB64 { get; set; }
        public string SignSoapXmlSigners { get; set; }
        public string UnpackSignedContent { get; set; }
        public string MrCheckSignSgnContent { get; set; }
    }
}
