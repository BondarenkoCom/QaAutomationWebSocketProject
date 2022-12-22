using System.Threading.Tasks;
using Helpers.Serializations;
using NUnit.Framework;
using Processor.Interfaces.DTO.CertOperation.GetCertFromStore;
using WsAdapter.Interface;
using static LibrarySettings.ConstantsForMatch;

namespace WebSocketTests.Tests.Scenarios.BaseMethods
{
    public static class CertificateBase
    {
        public static async Task GetCertificatesFromStore(string storeName, bool checkChain, bool checkCrl, IWsAdapter wsAdapter )
        {
            var certRequest = MessageGenerator.Support.GetCertificatesFromStore.Generate(storeName, checkChain, checkCrl);
            var respSocketCert = await wsAdapter.SendMessageWithoutCloseSocket(certRequest);
            var objCertInfo = SerializationHelpers.JsonDeserialize<GetCertInfoOutputDto>(respSocketCert);
            
            Assert.Multiple(() =>
            {
                Assert.That(objCertInfo.Certificates[0].Eku, Is.Not.Empty);
                Assert.That(objCertInfo.Certificates[0].Eku, Is.Not.Null);
                Assert.That(objCertInfo.Certificates[0].PublicKeyOid, Is.Not.Empty);
                Assert.That(objCertInfo.Certificates[0].PublicKeyOid, Is.Not.Null);

                Assert.That(objCertInfo.Certificates[0].Thumb, Is.Not.Empty);
                Assert.That(objCertInfo.Certificates[0].Thumb, Is.Not.Null);

                Assert.That(respSocketCert, Is.Not.Empty);
                Assert.That(respSocketCert, Is.Not.Null);
                Assert.That(respSocketCert, Does.Match(CertResult));
            });
        }
    }
}
