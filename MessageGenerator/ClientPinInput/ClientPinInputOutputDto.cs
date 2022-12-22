using Helpers.Serializations;
using Processor.Interfaces.DTO.PinClient.ClientPinInput;

namespace MessageGenerator.ClientPinInput
{
    public static class ClientPinInputOutput
    {
        public static string Generate(string pinResponse, bool userCancelled)
        {
            return SerializationHelpers.JsonSerialize(GenerateObj(pinResponse, userCancelled));
        }

        private static ClientPinInputOutputDto GenerateObj(string pinResponse, bool userCancelled)
        {
            return new ClientPinInputOutputDto()
            {
                PinResponse = pinResponse ,
                UserCancelled = userCancelled
            };
        }
    }
}
