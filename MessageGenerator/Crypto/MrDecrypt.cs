using Helpers.Serializations;
using Processor.Interfaces.Const;
using Processor.Interfaces.DTO.Decrypt.MrDecrypt;

namespace MessageGenerator.Crypto
{
    public static class MrDecrypt
    {
        public static string Generate(string encContent)
        {
            return SerializationHelpers.JsonSerialize(GenerateObj(encContent));
        }

        private static MrDecryptInputDto GenerateObj(string encContent)
        {
            return new MrDecryptInputDto()
            {
                EncContent = encContent,
                MethodName = MethodNames.MrDecrypt
            };
        }
    }
}
