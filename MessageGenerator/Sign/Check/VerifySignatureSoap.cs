using Helpers.Serializations;
using Processor.Interfaces.Const;
using Processor.Interfaces.DTO.Sign.CheckSign.VerifySignatureSoap;

namespace MessageGenerator.Sign.Check
{
    public static class VerifySignatureSoap
    {
        public static string Generate(string contentB64)
        {
            return SerializationHelpers.JsonSerialize(GenerateObj(contentB64));
        }

        private static  VerifySignatureSoapInputDto GenerateObj(string contentB64)
        {
            return new VerifySignatureSoapInputDto()
            {
                ContentB64 = contentB64,
                MethodName = MethodNames.VerifySignatureSoap
            };
        }
    }
}
