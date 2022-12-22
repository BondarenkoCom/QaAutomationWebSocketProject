using Helpers.Serializations;

using Processor.Interfaces.Const;
using Processor.Interfaces.DTO.Initialization.GetExtVersion;

namespace MessageGenerator.Support
{
    public static class GetExtVersion
    {
        public static string Generate()
        {
            return SerializationHelpers.JsonSerialize(GenerateObj());
        }

        private static GetExtVersionInputDto GenerateObj()
        {
            return new GetExtVersionInputDto()
            {
                MethodName = MethodNames.GetExtVersion
            };
        }
    }
}