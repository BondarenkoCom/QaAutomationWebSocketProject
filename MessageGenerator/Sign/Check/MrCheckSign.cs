using Helpers.Serializations;
using Processor.Interfaces.Const;
using Processor.Interfaces.DTO.Sign.CheckSign.MrCheckSign;

namespace MessageGenerator.Sign.Check
{
    public static class MrCheckSign
    {
        public static string Generate(string content, string sgn_content)
        {
            return SerializationHelpers.JsonSerialize(GenerateObj(content, sgn_content));
        }

        private static MrCheckSignInputDto GenerateObj(string content, string sgn_content)
        {
            return new MrCheckSignInputDto()
            {
                Content = content, SgnContent = sgn_content, MethodName = MethodNames.MrCheckSign
            };
        }
    }
}
