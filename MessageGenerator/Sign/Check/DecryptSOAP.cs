using System.Collections.Generic;
using Helpers.Serializations;
using Processor.Interfaces.Const;
using Processor.Interfaces.DTO.Decrypt.DecryptSoap;

namespace MessageGenerator.Sign.Check
{
    public class DecryptSoap
    {
        public static string Generate(string content, List<string>publicKeysArrayThumb, List<string>publicKeysArrayBase64)
        {
            return SerializationHelpers.JsonSerialize(GenerateObj(content, publicKeysArrayThumb , publicKeysArrayBase64));
        }

        private static DecryptSoapInputDto GenerateObj(string content, List<string> publicKeysArrayThumb, List<string> publicKeysArrayBase64)
        {
            return new DecryptSoapInputDto()
            {
                Content = content,
                PublicKeysArrayThumb = publicKeysArrayThumb,
                PublicKeysArrayBase64 = publicKeysArrayBase64,
                MethodName = MethodNames.DecryptSoap
            };
        }
    }
}
