using Helpers.Serializations;
using Processor.Interfaces.Const;
using Processor.Interfaces.DTO.CertOperation.GetCertFromStore;

namespace MessageGenerator.Support
{
    public static class GetCertificatesFromStore
    {
        public static string Generate(string storeName , bool checkChain , bool checkCrl)
        {
            return SerializationHelpers.JsonSerialize(GenerateObj(storeName , checkChain, checkCrl));
        }

        private static GetCertInfoInputDto GenerateObj(string storeName, bool checkChain, bool checkCrl)
        {
            return new GetCertInfoInputDto()
            { 
                StoreName = storeName ,
                CheckChain = checkChain,
                CheckCrl = checkCrl,
                MethodName = MethodNames.GetCertificatesFromStore
            };
        }
    }
}
