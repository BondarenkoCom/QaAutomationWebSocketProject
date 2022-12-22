using Helpers.Serializations;
using Processor.Interfaces.Const;
using Processor.Interfaces.DTO.BaseInfo;
using Processor.Interfaces.DTO.Sign.MrSignByHash;

namespace MessageGenerator.Sign
{
    public static class MrSignByHash
    {
        public static string Generate(string hash, string certThumb)
        {
            return SerializationHelpers.JsonSerialize(GenerateObj(hash, certThumb));
        }

        private static MrSignByHashInputDto GenerateObj(string hash, string certThumb)
        {
            return new MrSignByHashInputDto()
            {
                CertThumb = certThumb,
                Content = hash,
                MethodName = MethodNames.MrSignByHash
            };
        }

        public static MrSignByHashInputDto GenerateObj(string hash, string certThumb, MinimalInputUniversalObjectDto minimalInput)
        {
            return new MrSignByHashInputDto()
            {
                CertThumb = certThumb,
                Content = hash,
                CheckChain = minimalInput.CheckChain,
                CheckCrl = minimalInput.CheckCrl,
                StoreName = minimalInput.StoreName,
                MethodName = MethodNames.MrSignByHash
            };
        }
    }
}