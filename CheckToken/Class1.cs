using System;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;
using Processor.Interfaces.DTO.CertOperation.GetCertFromStore;
using WebSocketTests.Tests.SupportMethod;

namespace CheckToken
{
    public class Class1
    {
        public static async Task<object> GetCerts()
        {
            var a =  GetValue.GetCertFromStore(thumbs);
            Console.WriteLine(a);
        }
    }
}
