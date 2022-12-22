using System;
using System.Threading.Tasks;
using Constants;
using Helpers.Serializations;

using LogSupport;

using MessageGenerator.Support;
using NUnit.Framework;
using Processor.Interfaces.DTO.CertOperation.GetCertFromStore;
using Processor.Interfaces.DTO.Support.GetCspVersion;
using TestBase;
using static LibrarySettings.ConstantsForMatch;

namespace WebSocketTests.Tests.SupportMethod
{
    [TestFixture]
    public class GetValue : WsTestBase<GetValue>
    {
        [Test]
        public async Task Test_GetCertFromStore()
        {
            Logger.LogCurrentMethodName();

            await WsAction(async wsAdapter =>
            {
                var certRequest = GetCertificatesFromStore.Generate(StoreName, CheckChain, CheckCrl);
                Logger.Info($"Сгенерировать GetCertificatesFromStore сокет запрос - {certRequest}");

                var resultOf = await wsAdapter.SendMessageWithoutCloseSocket(certRequest);
                Logger.Info($"Результат сокета запроса - {resultOf}");

                var objCert = SerializationHelpers.JsonDeserialize<GetCertInfoOutputDto>(resultOf);
                Logger.Info("десериализация объекта");

                foreach (var ob in objCert.Certificates)
                {
                    Assert.Multiple(() =>
                    {
                        Console.WriteLine(ob.ProviderName);

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
                    Assert.That(resultOf, Is.Not.Empty);
                    Assert.That(resultOf, Is.Not.Null);
                    Assert.That(resultOf, Does.Match(CertResult));
                });
            }, CommonInfo.AgentMainAddress);
        }

        [Test]
        public async Task Test_GetCSPVersion()
        {
            Logger.LogCurrentMethodName();

            await WsAction(async wsAdapter =>
            {
                var cspVerRequest = GetCspVersion.Generate();
                Logger.Info($"Сгенерировать GetCspVersion сокет запрос - {cspVerRequest}");

                var resultCsp = await wsAdapter.SendMessageWithoutCloseSocket(cspVerRequest);
                Logger.Info($"Результат сокета запроса - {resultCsp}");

                var obj = SerializationHelpers.JsonDeserialize<GetCspVersionOutputDto>(resultCsp);
                Logger.Info("Test_VerifySoap");

                Assert.Multiple(() =>
                {
                    Assert.That(obj.Major, Is.Not.Null);
                    Assert.That(obj.Minor, Is.Not.Null);

                    Assert.That(resultCsp, Is.Not.Null);
                    Assert.That(resultCsp, Is.Not.Empty);
                });
            }, CommonInfo.AgentMainAddress);
        }
    }
}