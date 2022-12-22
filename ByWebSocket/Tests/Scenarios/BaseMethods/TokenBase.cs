using System.Threading.Tasks;
using Helpers.Serializations;
using MessageGenerator.Token;
using NUnit.Framework;
using Processor.Interfaces.DTO.Token.TokenInitialize;
using Processor.Interfaces.DTO.CertOperation.GetCertFromStore;
using WsAdapter.Interface;
using static LibrarySettings.ConstantsForMatch;

namespace WebSocketTests.Tests.Scenarios.BaseMethods
{
    public static class TokenBase
    {
        public static async Task GetTokenInitAsync(IWsAdapter wsAdapter)
        {
            var tokenInit = TokenInitialize.Generate();
            var resToken = await wsAdapter.SendMessageWithoutCloseSocket(tokenInit);
            var obj = SerializationHelpers.JsonDeserialize<TokenInitializeOutputDto>(resToken);

            Assert.Multiple(() =>
            {
                Assert.That(obj.Number.ToString(), Does.Match(ResultTokenInit.ToString()));
                Assert.That(obj.Number.ToString(), Is.Not.Empty);
                Assert.That(obj.Number.ToString(), Is.Not.Null);
                
                Assert.That(resToken, Is.Not.Empty);
                Assert.That(resToken, Is.Not.Null);
            });
        }

        public static async Task GetTokenGetCertificate(IWsAdapter wsAdapter)
        {
            var tokenMes = TokenGetCertificate.Generate();
            var resultCerts = await wsAdapter.SendMessageWithoutCloseSocket(tokenMes);
            var certsObj = SerializationHelpers.JsonDeserialize<GetCertInfoOutputDto>(resultCerts);

            foreach (var ob in certsObj.Certificates)
            {
                Assert.Multiple(() =>
                {
                    Assert.That(ob.Thumb, Is.Not.Empty);
                    Assert.That(ob.Thumb, Is.Not.Null);

                    Assert.That(ob.PublicKeyOid, Is.Not.Empty);
                    Assert.That(ob.PublicKeyOid, Is.Not.Null);

                    Assert.That(ob.Cn, Is.Not.Empty);
                    Assert.That(ob.Cn, Is.Not.Null);

                    Assert.That(ob.Eku, Is.Not.Empty);
                    Assert.That(ob.Eku, Is.Not.Null);

                    Assert.That(ob.ProviderName, Is.Not.Null);
                    Assert.That(ob.ProviderName, Is.Not.Empty);
                });
            }

            Assert.Multiple(() =>
            {
                Assert.That(resultCerts, Is.Not.Empty);
                Assert.That(resultCerts, Does.Match(CertResult));
                Assert.That(resultCerts, Is.Not.Null);
            });
        }
    }
}
