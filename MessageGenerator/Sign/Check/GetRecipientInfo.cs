using Helpers.Serializations;
using Processor.Interfaces.Const;
using Processor.Interfaces.DTO.Support.GetRecipientInfo;

namespace MessageGenerator.Sign.Check
{
    public static class GetRecipientInfo
    {
        public static string Generate(string base64EncryptedContent)
        {
            return SerializationHelpers.JsonSerialize(GenerateObj(base64EncryptedContent));
        }

        private static GetRecipientInfoInputDto GenerateObj(string base64EncryptedContent)
        {
            return new GetRecipientInfoInputDto()
            {
                Base64EncryptedContent = base64EncryptedContent,

                MethodName = MethodNames.GetRecipientInfo
            };
        }
    }
}
