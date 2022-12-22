using System;
using System.Diagnostics;
using System.Threading.Tasks;
using LibrarySettings;
using MessageGenerator.ClientPinInput;
using MessageGenerator.Sign;
using NUnit.Framework;
using TestBase;
using System.Threading;
using Helpers.Serializations;

using LogSupport;

using MessageGenerator.Pincode;

using Processor.Interfaces.DTO.PinClient.ClientPinInput;

namespace WebSocketPinCodeTests
{
    public class PinCodeTests : WsTestBase<PinCodeTests>
    {
        [Test, Order(1)]
        [Ignore("this method broke tests")]
        public void SwitchOffAgent()
        {
            Logger.LogCurrentMethodName();
            try
            {
                if (CheckTokenName.GetCardName() == null)
                {
                    Assert.Pass("USB don't have token ");
                }

                foreach (var process in Process.GetProcessesByName("AgentAgent"))
                {
                    process.Kill();
                    Thread.Sleep(5000);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }


        [Test, Order(2)]
        [Ignore("this method broke tests")]

        public void SwitchOnAgent()
        {
            Logger.LogCurrentMethodName();
            try
            {
                if (CheckTokenName.GetCardName() == null)
                {
                    Assert.Pass("USB don't have token");
                }

                var filePath =
                    Environment.ExpandEnvironmentVariables(
                        @"%USERPROFILE%\AppData\Roaming\AgentAgent\AgentAgent.exe");
                Process.Start(filePath);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }


        [Test , Order(3)]
        [Ignore("this method broke tests")]

        public async Task TestPinCode_Accept()
        {
            try
            {
                Logger.LogCurrentMethodName();
                if (CheckTokenName.GetCardName() == null)
                {
                    Assert.Pass("USB don't have token");
                }

                await WsAction(async wsAdapter =>
                {
                    var webSocketPin = ClientSidePinInpiut.Generate(true);
                    var _ = await wsAdapter.SendMessageWithoutCloseSocket(webSocketPin);

                    var mrSign = MrSign.Generate(SqlIteReaderSocketValues.GetLoginValues().SignContentBase64,
                        SqlIteReaderSocketValues.GetLoginValues().TokenThumb);
                    var __ = await wsAdapter.SendMessageWithoutCloseSocket(mrSign);

                    var webSocketPinPassword =
                        ClientPinInputOutput.Generate(SqlIteReaderSocketValues.GetLoginValues().TokenLogin, false);
                    var res2 = await wsAdapter.SendMessageWithoutCloseSocket(webSocketPinPassword);

                    var ___ = SerializationHelpers.JsonDeserialize<ClientPinInputDto>(res2);

                    Assert.Multiple(() =>
                    {
                        Assert.That(res2, Is.Not.Null);
                        Assert.That(res2, Is.Not.Empty);
                        Assert.That(res2, Does.Match("sign_content"));
                    });
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [Test]
        [Ignore("this method broke tests")]

        public async Task TestPinCodeSendPinCodeCancel()
        {
            Logger.LogCurrentMethodName();
            if (CheckTokenName.GetCardName() == null)
            {
                Assert.Pass("����� �� �������� � USB");
            }

            await WsAction(async wsAdapter =>
            {
                var mrSign = MrSign.Generate(SqlIteReaderSocketValues.GetLoginValues().SignContentBase64,
                    SqlIteReaderSocketValues.GetLoginValues().TokenThumb);
                var _ = await wsAdapter.SendMessageWithoutCloseSocket(mrSign);

                var webSocketPinPassword =
                    ClientPinInputOutput.Generate(SqlIteReaderSocketValues.GetLoginValues().TokenLogin, false);
                var res2 = await wsAdapter.SendMessageWithoutCloseSocket(webSocketPinPassword);

                Assert.Multiple(() =>
                {
                    Assert.That(res2, Is.Not.Null);
                    Assert.That(res2, Is.Not.Empty);
                });
            });
        }
    }
}
