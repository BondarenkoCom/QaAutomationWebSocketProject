using System.Threading.Tasks;
using Helpers.Serializations;
using NUnit.Framework;
using MessageGenerator.Sign.Check;
using Processor.Interfaces.DTO.Support.GetCspVersion;
using Processor.Interfaces.DTO.Support.GetRecipientInfo;
using WsAdapter.Interface;
using static LibrarySettings.ConstantsForMatch;

namespace WebSocketTests.Tests.Scenarios.BaseMethods
{
    public static class SupportBase 
    {
        public static async Task GetCspVersion(IWsAdapter wsAdapter)
        {
            var cspVerRequest = MessageGenerator.Support.GetCspVersion.Generate();
            
            var resultCsp = await wsAdapter.SendMessageWithoutCloseSocket(cspVerRequest);
            var obj = SerializationHelpers.JsonDeserialize<GetCspVersionOutputDto>(resultCsp);

            Assert.Multiple(() =>
            {
                Assert.That(obj.Name, Is.Not.Empty);
                Assert.That(obj.Name, Is.Not.Null);
            });
        }

        public static async Task GetRecipientInfoAsync(string base64EncryptContent , IWsAdapter wsAdapter)
        {
            var socketReceipt = GetRecipientInfo.Generate(base64EncryptContent);
            var resultReceipt = await wsAdapter.SendMessageWithoutCloseSocket(socketReceipt);

            var obj = SerializationHelpers.JsonDeserialize<GetRecipientInfoOutputDto>(resultReceipt);

            Assert.Multiple(() =>
            {
                Assert.That(obj.Recipients[0].Issuer, Is.Not.Empty);
                Assert.That(obj.Recipients[0].Issuer, Is.Not.Null);

                Assert.That(obj.Recipients[0].SerialNumber, Is.Not.Empty);
                Assert.That(obj.Recipients[0].SerialNumber, Is.Not.Null);

                Assert.That(obj.Recipients, Is.Not.Empty);
                Assert.That(obj.Recipients, Is.Not.Null);

                Assert.That(resultReceipt, Is.Not.Empty);
                Assert.That(resultReceipt, Is.Not.Null);
                Assert.That(resultReceipt, Does.Match(ForMatchNameRecip));
            });
        }
    }
}
