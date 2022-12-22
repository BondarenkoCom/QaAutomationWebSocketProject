using Helpers.Serializations;
using Processor.Interfaces.Const;
using Processor.Interfaces.DTO.CertOperation.MrGetCertsInfoFromSign;

namespace MessageGenerator.Sign.Check
{
    public static class MrGetCertsInfoFromSign
    {
        public static string Generate(string signDataB64)
        {
            return SerializationHelpers.JsonSerialize(GenerateObj(signDataB64));
        }

        private static MrGetCertsInfoFromSignInputDto GenerateObj(string signDataB64)
        {
            return new MrGetCertsInfoFromSignInputDto()
            {
                SignDataB64 = signDataB64,
                MethodName = MethodNames.MrGetCertsInfoFromSign
            };
        }
    }
}
