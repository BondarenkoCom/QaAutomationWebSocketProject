using Helpers.Serializations;
using Processor.Interfaces.Const;
using Processor.Interfaces.DTO.Sign.MrSignAttach;

namespace MessageGenerator.Sign
{
    public static class MrSignAttach
    {
        public static string Generate(string content , string certThumb)
        {
            return SerializationHelpers.JsonSerialize(GenerateObj(content, certThumb));
        }

        private static MrSignAttachInputDto GenerateObj(string content, string certThumb)
        {
            return new MrSignAttachInputDto()
            {
               Content = content,
               CertThumb = certThumb,
               MethodName = MethodNames.MrSignAttach
            };
        }
    }
}
