using System.Threading.Tasks;
using Helpers.Serializations;
using LogSupport;
using NUnit.Framework;
using MessageGenerator.Token;
using Processor.Interfaces.DTO.CertOperation.GetCertFromStore;
using Processor.Interfaces.DTO.Token.TokenInitialize;
using TestBase;
using static LibrarySettings.ConstantsForMatch;
using Constants;

namespace WebSocketTests.Tests.CryptoMethod
{
    [TestFixture]
    public class Token : WsTestBase<Token>
    {

        [Test]
        public async Task Test_TokenInitialize()
        {
            Logger.LogCurrentMethodName();
            await WsAction(async wsAdapter =>
            {
                var tokMes = TokenInitialize.Generate();
                Logger.Info($"Сгенерировать TokenInitialize сокет запрос - {tokMes}");

                var resultOf = await wsAdapter.SendMessageWithoutCloseSocket(tokMes);
                Logger.Info($"Результат сокета запроса - {resultOf}");

                var obj = SerializationHelpers.JsonDeserialize<TokenInitializeOutputDto>(resultOf);
                Logger.Info("десериализация объекта");

                Assert.Multiple(() =>
                {
                    Assert.That(obj.Number.ToString(), Does.Match(ResultTokenInit.ToString()));
                    Assert.That(obj.Number.ToString(), Is.Not.Empty);
                    Assert.That(obj.Number.ToString(), Is.Not.Null);

                    Assert.That(resultOf, Does.Match(ResultTokenInitFull));
                    Assert.That(resultOf, Is.Not.Empty);
                    Assert.That(resultOf, Is.Not.Null);
                });
            }, CommonInfo.AgentMainAddress);
        }

        [Test]
        public async Task Test_TokenGetCertificates()
        {
            Logger.LogCurrentMethodName();

            await WsAction(async wsAdapter =>
            {
                var tokenMes = TokenGetCertificate.Generate();
                Logger.Info($"Сгенерировать TokenGetCertificate сокет запрос - {tokenMes}");

                var resultOf = await wsAdapter.SendMessageWithoutCloseSocket(tokenMes);
                Logger.Info($"Результат сокета запроса - {resultOf}");

                var obj = SerializationHelpers.JsonDeserialize<GetCertInfoOutputDto>(resultOf);
                Logger.Info("десериализация объекта");

                Assert.Multiple(() =>
                    {
                        Assert.That(obj.Certificates, Is.Not.Null);

                        Assert.That(resultOf, Is.Not.Empty);
                        Assert.That(resultOf, Does.Match(CertResult));
                        Assert.That(resultOf, Is.Not.Null);
                    });
                }, CommonInfo.AgentMainAddress);
            }
        }
}
