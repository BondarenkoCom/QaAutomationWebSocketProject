using Helpers.Serializations;
using Processor.Interfaces.Const;
using Processor.Interfaces.DTO.Token.TokenInitialize;

namespace MessageGenerator.Token
{
    public static class TokenInitialize
    {
        public static string Generate()
        {
            return SerializationHelpers.JsonSerialize(GenerateObj());
        }

        private static TokenInitializeInputDto GenerateObj()
        {
            return new TokenInitializeInputDto()
            {
                MethodName = MethodNames.TokenInitialize
            };
        }
    }
}
