using Helpers.Serializations;
using Processor.Interfaces.Const;
using Processor.Interfaces.DTO.Sign.SignSoap;

namespace MessageGenerator.Sign
{
    public static  class SignSoap
    {
        public static string Generate(string contentB64 , string xmlSigners)
        {
            return SerializationHelpers.JsonSerialize(GenerateObj(contentB64 , xmlSigners));
        }

        private static  SignSoapInputDto GenerateObj(string contentB64 , string xmlSigners)
        {
            return new SignSoapInputDto()
            {
                ContentB64 = contentB64,
                XmlSigners = xmlSigners,
                MethodName = MethodNames.SignSoap
            };
        }
    }
}
