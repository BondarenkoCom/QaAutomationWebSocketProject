using System.Collections.Generic;
using Helpers.Serializations;
using Processor.Interfaces.Const;
using Processor.Interfaces.DTO.Encrypt.EncryptSoap;

namespace MessageGenerator.Sign.Check
{
    public class EncryptSoap
    {
        public static string Generate(string content , List<string> publicKeysArrayThumb, List<string> publicKeysArrayBase64,
            List<string> publicKeysArrayThumbSender, List<string> publicKeysArrayBase64Sender)
        {
            return SerializationHelpers.JsonSerialize(GenerateObj(content , publicKeysArrayThumb, publicKeysArrayBase64, publicKeysArrayThumbSender, publicKeysArrayBase64Sender));
        }

        private static EncryptSoapInputDto GenerateObj(string content, List<string> publicKeysArrayThumb, List<string> publicKeysArrayBase64,
            List<string> publicKeysArrayThumbSender, List<string> publicKeysArrayBase64Sender)
        {
            return new EncryptSoapInputDto()
            {
                Content = content,
                PublicKeysArrayThumb = publicKeysArrayThumb ,
                PublicKeysArrayBase64 = publicKeysArrayBase64,
                PublicKeysArrayThumbSender = publicKeysArrayThumbSender,
                PublicKeysArrayBase64Sender = publicKeysArrayBase64Sender,
                MethodName = MethodNames.EncryptSoap
            };
        }
    }
}

