using Helpers.Serializations;
using Processor.Interfaces.Const;
using Processor.Interfaces.DTO.PinClient.ClientSidePinInputSupport;

namespace MessageGenerator.Pincode
{
    public static class ClientSidePinInpiut 
    {
        public static string Generate(bool clientPinInputSupported)
        {
            return SerializationHelpers.JsonSerialize(GenerateObj(clientPinInputSupported));
        }

        private static ClientSidePinInputSupportDto GenerateObj(bool clientPinInputSupported)
        {
            return new ClientSidePinInputSupportDto()
            {
                MethodName = MethodNames.ClientSidePinInputSupport,
                ClientPinInputSupported = clientPinInputSupported
            };
        }
    }
}
