using System.Collections.Generic;
using Helpers.Serializations;
using Processor.Interfaces.Const;
using Processor.Interfaces.DTO.Encrypt.MrEncrypt;

namespace MessageGenerator.Crypto
{
    public static class MrEncrypt
    {
        public static string Generate(string content, List<string> publicKeysArrayThumb , List<string> publicKeysArrayBase64)
        {
            return SerializationHelpers.JsonSerialize(GenerateObj(content, publicKeysArrayThumb, publicKeysArrayBase64));
        }

        private static MrEncryptInputDto GenerateObj(string content, List<string> publicKeysArrayThumb, List<string> publicKeysArrayBase64)
        {
            return new MrEncryptInputDto()
            {
                Content = content,
                PublicKeysArrayThumb = publicKeysArrayThumb,
                PublicKeysArrayBase64 = publicKeysArrayBase64,

                MethodName = MethodNames.MrEncrypt
            };
        }
    }
}
