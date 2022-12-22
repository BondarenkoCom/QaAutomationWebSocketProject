using System.Threading.Tasks;
using Constants;
using Helpers.Serializations;
using NUnit.Framework;
using Processor.Interfaces.DTO.Initialization.GetExtVersion;
using TestBase;

namespace WebSocketTests.Tests.SupportMethod
{
    public class VersionInfo : WsTestBase<VersionInfo>
    { 
        [Test]
        public async Task Test_GetVersion()
        {
            Logger.Info("Test_GetVersion");

            await WsAction(async wsAdapter =>
            { 
                var message = MessageGenerator.Support.GetExtVersion.Generate();
                Logger.Info($"Сгенерировать GetExtVersion сокет запрос - {message}");

                var result = await wsAdapter.SendMessageWithoutCloseSocket(message);
                Logger.Info($"Результат сокета запроса - {result}");

                var obj = SerializationHelpers.JsonDeserialize<GetExtVersionOutputDto>(result);
                Logger.Info("Deserialize object");

                Assert.Multiple(() =>
                {
                    Assert.That(obj.Result, Is.Not.Null);
                    Assert.That(obj.Result, Is.Not.Empty);

                    Assert.IsNotEmpty(result);
                    Assert.That(result, Is.Not.Null);
                });
            }, CommonInfo.AgentMainAddress);
        }
    }
}