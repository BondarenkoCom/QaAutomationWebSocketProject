using Helpers.Serializations;
using Processor.Interfaces.Const;
using Processor.Interfaces.DTO.Sign.MrSign;

namespace MessageGenerator.Sign
{
    public static class MrSign
    {
        public static string Generate(string content, string certThumb)
        {
            return SerializationHelpers.JsonSerialize(GenerateObj(content, certThumb));
        }

        private static MrSignInputDto GenerateObj(string content, string certThumb)
        {
            return new MrSignInputDto()
            {
                Content = content ,
                CertThumb = certThumb,
                MethodName = MethodNames.MrSign
            };
        }
    }
}
