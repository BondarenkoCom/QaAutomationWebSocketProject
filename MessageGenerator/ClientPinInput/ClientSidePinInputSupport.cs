using Helpers.Serializations;
using Processor.Interfaces.DTO.PinClient.ClientSidePinInputSupport;

namespace MessageGenerator.ClientPinInput
{
    public class ClientSidePinInputSupport
    {
        public static string Generate(bool clientPinInputSupported)
        {
            return SerializationHelpers.JsonSerialize(GenerateObj(clientPinInputSupported));
        }

        private static ClientSidePinInputSupportDto GenerateObj(bool clientPinInputSupported)
        {
            return new ClientSidePinInputSupportDto()
            {
                ClientPinInputSupported = clientPinInputSupported,
            };
        }
    }
}
