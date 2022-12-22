using Helpers.Serializations;
using Processor.Interfaces.Const;
using Processor.Interfaces.DTO.Token.TokenGetCertificates;

namespace MessageGenerator.Token
{
    public static class TokenGetCertificate
    {
        public static string Generate()
        {
            return SerializationHelpers.JsonSerialize(GenerateObj());
        }

        private static TokenGetCertificatesInputDto GenerateObj()
        {
            return new TokenGetCertificatesInputDto()
            {
                MethodName = MethodNames.TokenGetCertificates
            };
        }
    }
}
