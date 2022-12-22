using Helpers.Serializations;
using Processor.Interfaces.Const;
using Processor.Interfaces.DTO.Support.GetCspVersion;

namespace MessageGenerator.Support
{
    public static class GetCspVersion
    {
        public static string Generate()
        {
            return SerializationHelpers.JsonSerialize(GenerateObj());
        }

        private static GetCspVersionInputDto GenerateObj()
        {
            return new GetCspVersionInputDto()
            {
                MethodName = MethodNames.GetCspVersion
            };
        }
    }
}
