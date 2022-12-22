using Helpers.Serializations;
using Processor.Interfaces.Const;
using Processor.Interfaces.DTO.Support.MrUnpackSignedFile;

namespace MessageGenerator.Sign.Check
{
    public static class MrUnpackSignedFile
    {
        public static string Generate(string sgnData)
        {
            return SerializationHelpers.JsonSerialize(GenerateObj(sgnData));
        }

        private static MrUnpackSignedFileInputDto GenerateObj(string sgnData)
        {
            return new MrUnpackSignedFileInputDto()
            {
                SgnData = sgnData,
                MethodName = MethodNames.MrUnpackSignedFile
            };
        }
    }
}
