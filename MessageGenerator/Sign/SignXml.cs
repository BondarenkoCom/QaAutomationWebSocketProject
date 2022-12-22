using Helpers.Serializations;
using Processor.Interfaces.Const;
using Processor.Interfaces.DTO.Sign.SignXml;

namespace MessageGenerator.Sign
{
    public static class SignXml
    {
        public static string Generate(string contentB64 , string certThumb)
        {
            return SerializationHelpers.JsonSerialize(GenerateObj(contentB64 , certThumb));
        }

        private static SignXmlInputDto GenerateObj(string contentB64, string certThumb)
        {
            return new SignXmlInputDto()
            {
                ContentB64 = contentB64,
                CertThumb = certThumb,
                MethodName = MethodNames.SignXml
            };
        }
    }
}
