using Helpers.Serializations;
using Processor.Interfaces.Const;
using Processor.Interfaces.DTO.BaseInfo;

namespace MessageGenerator.Pincode
{
    public static class WebSocketPin
    {
        public static string Generate(string storeName, bool checkChain, bool checkCrl, bool checkTimeValidity)
        {
            return SerializationHelpers.JsonSerialize(GenerateObj(storeName, checkChain , checkCrl , checkTimeValidity));
        }

        private static MinimalInputUniversalObjectDto GenerateObj(string storeName, bool checkChain , bool checkCrl ,bool checkTimeValidity)
        {
            return new MinimalInputUniversalObjectDto()
            {
                MethodName = MethodNames.ClientSidePinInputSupport,
                StoreName = storeName,
                CheckChain = checkChain,
                CheckCrl = checkCrl,
                CheckTimeValidity = checkTimeValidity
            };
        }
    }
}
