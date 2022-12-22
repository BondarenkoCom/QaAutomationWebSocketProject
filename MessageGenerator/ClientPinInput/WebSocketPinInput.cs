using Helpers.Serializations;
using Processor.Interfaces.DTO.PinClient.ClientPinInput;

namespace MessageGenerator.ClientPinInput
{
    public class WebSocketPinInput
    {
        public static string Generate(bool wrongLastInput, uint maxPinLen , int inputTryLeft , int maxTryCount)
        {
            return SerializationHelpers.JsonSerialize(GenerateObj(wrongLastInput, maxPinLen , inputTryLeft , maxTryCount));
        }

        private static ClientPinInputDto GenerateObj(bool wrongLastInput, uint maxPinLen, int inputTryLeft, int maxTryCount)
        {
            return new ClientPinInputDto()
            {
                WrongLastInput = wrongLastInput , 
                MaxPinLen = maxPinLen, 
                InputTryLeft = inputTryLeft, 
                MaxTryCount = maxTryCount
            };
        }
    }
}
