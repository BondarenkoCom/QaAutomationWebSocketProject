using System;
using System.Threading.Tasks;
using Constants;
using NUnit.Framework;
using RestAdapter;
using LogSupport;
using NUnit.Framework.Internal;
using RestSharp;
using TestBase;
using static LibrarySettings.ConstantsForMatch;

namespace TestRestApi
{
    [TestFixture]
    public class RestTests : WsTestBase<RestTests>
    {
        IRestWork _restWork = new RestWork();

        [Test]
        public async Task Test_Get_Version()
        {
            Logger.LogCurrentMethodName();

            var responseFromApi = await _restWork.RestAction($"{CommonInfo.LocalHostAddress}{_restWork.CheckPort()}/{CommonInfo.RestApiAgentRequestVersion}", 
                " ", 
                Method.Get);

            Console.WriteLine($"result port - {_restWork.CheckPort()}");

            Logger.Info(
                $"������ ����� �� APi - {responseFromApi.ResponseContent} " +
                $"\n������ - {responseFromApi.ResponseStatusCode}" +
                $"\n���� - {_restWork.CheckPort()}",
                $"\n������ �� Api - {responseFromApi.ToString()}"
                );
            
            Assert.Multiple((TestDelegate)(() =>
            {
                Assert.That(responseFromApi.ResponseContent, Is.Not.Empty);
                Assert.That(responseFromApi.ResponseContent, Is.Not.Null);
                Assert.That(responseFromApi.ResponseContent, Does.Match(VersionPattern));

                Assert.That($"{responseFromApi.ResponseStatusCode}", Does.Match(StatusResponseCode.ToString()));

                Console.WriteLine(
                    $"������ ����� �� APi - {responseFromApi.ResponseContent} " +
                    $"\n������ - {responseFromApi.ResponseStatusCode}" +
                    $"\n���� - {_restWork.CheckPort()}" +
                    $"\nApi ������ - {responseFromApi.ToString()}");
            }));
        }

        [Test]
        public async Task Test_Ping()
        {
            Logger.LogCurrentMethodName();

            var responseFromApi = await _restWork.RestAction(CommonInfo.RestApiPing, " ", Method.Get);
            Logger.Info($"������ ����� �� APi - {responseFromApi}");

            Assert.Multiple((TestDelegate)(() =>
            {
                Assert.That(responseFromApi.ResponseContent, Is.Not.Empty);
                Assert.That(responseFromApi.ResponseContent, Is.Not.Null);

                Assert.That($"{responseFromApi.ResponseStatusCode}", Does.Match(StatusResponseCode.ToString()));
                Console.WriteLine(responseFromApi);

                Console.WriteLine(
                    $"������ ����� �� APi - {responseFromApi.ResponseContent}" +
                    $"\n������ - {responseFromApi.ResponseStatusCode}" +
                    $"\n���� - {_restWork.CheckPort()}");
            }));
        }

        [Test]
        public async Task Test_wss()
        {
            Logger.LogCurrentMethodName();
            var fullWss =$"{CommonInfo.WssApi}{_restWork.CheckPort()}";
            await WsAction(async wsAdapter =>
            {
                var wssRequest = $"{CommonInfo.WssApi}{_restWork.CheckPort()}";
                
                Logger.Info($"��������� ���� - {wssRequest}");

                var resultWss = await wsAdapter.SendMessageWithoutCloseSocket(wssRequest);

                Console.WriteLine(resultWss);

            }, fullWss);
        }
    }
}