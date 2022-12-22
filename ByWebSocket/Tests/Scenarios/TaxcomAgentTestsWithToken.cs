using System.Threading.Tasks;
using Constants;
using LibrarySettings;

using LogSupport;

using NUnit.Framework;
using TestBase;
using WebSocketTests.Tests.Scenarios.BaseMethods;
using static LibrarySettings.ConstantsForMatch;
using static LibrarySettings.ConstsForInput;

[assembly: Description("Сценарные тесты")]

namespace WebSocketTests.Tests.Scenarios
{
    [TestFixture]
    public class TaxcomAgentTestsWithToken : WsTestBase<TaxcomAgentTestsWithToken>
    {

        [Test, Description("Авторизация")]
        public async Task CheckAuthorizeNotStopSocket()
        {
            Logger.LogCurrentMethodName();
            await WsAction(async wsAdapter =>
            {
                await TokenBase.GetTokenInitAsync(wsAdapter);
                Logger.Info("method was work - GetTokenInitAsync");

                await TokenBase.GetTokenGetCertificate(wsAdapter);
                Logger.Info("method was work - GetTokenGetCertificate");

                await CertificateBase.GetCertificatesFromStore(StoreName, CheckChain, CheckCrl, wsAdapter);
                Logger.Info("method was work - GetCertificatesFromStore");

                await CryptoBase.GetMrSign(SqlIteReaderSocketValues.GetLoginValues().SignContentBase64,
                    SqlIteReaderSocketValues.GetLoginValues().TokenThumb, wsAdapter);
                Logger.Info("method was work - GetMrSign");

                await SupportBase.GetCspVersion(wsAdapter);
                Logger.Info("method was work - GetCspVersion");
            }, CommonInfo.AgentMainAddress);
        }

        [Test, Description("TaxcomAgent запрос о получении бюджета")]
        public async Task TaxComAgentDocumentGetBudget()
        {
            Logger.LogCurrentMethodName();
            await WsAction(async wsAdapter =>
            {
                await TokenBase.GetTokenInitAsync(wsAdapter);
                Logger.Info("method was work - GetTokenInitAsync");

                await TokenBase.GetTokenGetCertificate(wsAdapter);
                Logger.Info("method was work - GetTokenGetCertificate");

                await CertificateBase.GetCertificatesFromStore(StoreName, CheckChain, CheckCrl, wsAdapter);
                Logger.Info("method was work - GetCertificatesFromStore");

                await CryptoBase.GetSignByHash(HashContent, SqlIteReaderSocketValues.GetLoginValues().TokenThumb, wsAdapter);
                Logger.Info("method was work - GetSignByHash");
            }, CommonInfo.AgentMainAddress);
        }

        [Test, Description("отправить отчет в ФНС")]
        public async Task SendFNS()
        {
            Logger.LogCurrentMethodName();
            await WsAction(async wsAdapter =>
            {
                await TokenBase.GetTokenInitAsync(wsAdapter);
                Logger.Info("method was work - GetTokenInitAsync");

                await TokenBase.GetTokenGetCertificate(wsAdapter);
                Logger.Info("method was work - GetTokenGetCertificate");

                await CertificateBase.GetCertificatesFromStore(StoreName, CheckChain, CheckCrl, wsAdapter);
                Logger.Info("method was work - GetCertificatesFromStore");

                await CryptoBase.GetSignByHash(HashContent, SqlIteReaderSocketValues.GetLoginValues().TokenThumb, wsAdapter);
                Logger.Info("method was work - GetSignByHash");
            }, CommonInfo.AgentMainAddress);
        }

        [Test, Description("вторичный прием от ФНС: расшифровка")]
        public async Task GetSecondFNSDecrypt()
        {
            Logger.LogCurrentMethodName();
            await WsAction(async wsAdapter =>
            {
                await TokenBase.GetTokenInitAsync(wsAdapter);
                Logger.Info("method was work - GetTokenInitAsync");

                await SupportBase.GetRecipientInfoAsync(SqlIteReaderSocketValues.GetTestValue().RecipientInfoContent, wsAdapter);
                Logger.Info("method was work - GetRecipientInfoAsync");

                await TokenBase.GetTokenGetCertificate(wsAdapter);
                Logger.Info("method was work - GetTokenGetCertificate");
            }, CommonInfo.AgentMainAddress);
        }

        [Test, Description("прием от ФНС")]
        public async Task GetFirstFNSDecrypt()
        {
            Logger.LogCurrentMethodName();
            await WsAction(async wsAdapter =>
            {
                await TokenBase.GetTokenInitAsync(wsAdapter);
                Logger.Info("method was work - GetTokenInitAsync");

                await SupportBase.GetRecipientInfoAsync(SqlIteReaderSocketValues.GetTestValue().RecipientInfoContent, wsAdapter);
                Logger.Info("method was work - GetRecipientInfoAsync");

                await TokenBase.GetTokenInitAsync(wsAdapter);
                Logger.Info("method was work - GetTokenInitAsync");

                await TokenBase.GetTokenGetCertificate(wsAdapter);
                Logger.Info("method was work - GetTokenGetCertificate");

                await CertificateBase.GetCertificatesFromStore(StoreName, CheckChain, CheckCrl, wsAdapter);
                Logger.Info("method was work - GetCertificatesFromStore");

                await CryptoBase.GetMrDecrypt(SqlIteReaderSocketValues.GetTestValue().MrDecryptContent, wsAdapter);
                Logger.Info("method was work - GetMrDecrypt");
            }, CommonInfo.AgentMainAddress);
        }

        [Test, Description("Отправка ПФР очтета: открепленная подпись")]
        public async Task SendPFRSign()
        {
            Logger.LogCurrentMethodName();
            await WsAction(async wsAdapter =>
            {
                await TokenBase.GetTokenInitAsync(wsAdapter);
                Logger.Info("method was work - GetTokenInitAsync");

                await TokenBase.GetTokenGetCertificate(wsAdapter);
                Logger.Info("method was work - GetTokenGetCertificate");

                await CertificateBase.GetCertificatesFromStore(StoreName, CheckChain, CheckCrl, wsAdapter);
                Logger.Info("method was work - GetCertificatesFromStore");

                await CryptoBase.GetMrSign(SqlIteReaderSocketValues.GetLoginValues().SignContentBase64,
                    SqlIteReaderSocketValues.GetLoginValues().TokenThumb, wsAdapter);
                Logger.Info("method was work - GetMrSign");
            }, CommonInfo.AgentMainAddress);
        }

        [Test, Description("отправка ПФР отчета: шифрование")]
        public async Task SendPFREncrypt()
        {
            Logger.LogCurrentMethodName();
            await WsAction(async wsAdapter =>
            {
                await TokenBase.GetTokenInitAsync(wsAdapter);
                Logger.Info("method was work - GetTokenInitAsync");

                await TokenBase.GetTokenGetCertificate(wsAdapter);
                Logger.Info("method was work - GetTokenGetCertificate");

                await CertificateBase.GetCertificatesFromStore(StoreName, CheckChain, CheckCrl, wsAdapter);
                Logger.Info("method was work - GetCertificatesFromStore");

                await CryptoBase.GetMrSign(SqlIteReaderSocketValues.GetLoginValues().SignContentBase64,
                    SqlIteReaderSocketValues.GetLoginValues().TokenThumb, wsAdapter);
                Logger.Info("method was work - GetMrSign");

                await CryptoBase.GetMrEncrypt(SqlIteReaderSocketValues.GetTestValue().MrEncryptContent, wsAdapter);
                Logger.Info("method was work - GetMrEncrypt");
            }, CommonInfo.AgentMainAddress);
        }

        [Test, Description("отправка ЕТК: xml-signature")]
        public async Task SendEtkXmlSign()
        {
            Logger.LogCurrentMethodName();
            await WsAction(async wsAdapter =>
            {
                await TokenBase.GetTokenInitAsync(wsAdapter);
                Logger.Info("method was work - GetTokenInitAsync");

                await TokenBase.GetTokenGetCertificate(wsAdapter);
                Logger.Info("method was work - GetTokenGetCertificate");

                await CertificateBase.GetCertificatesFromStore(StoreName, CheckChain, CheckCrl, wsAdapter);
                Logger.Info("method was work - GetCertificatesFromStore");

                await CryptoBase.GetSignXML(SqlIteReaderSocketValues.GetTestValue().SignXmlContent,
                    SqlIteReaderSocketValues.GetLoginValues().TokenThumb, wsAdapter);
                Logger.Info("method was work - GetSignXML");
            }, CommonInfo.AgentMainAddress);
        }

        [Test, Description("отправка ЕТК: шифрование данных")]
        public async Task SendEtkXmlEncryptSignValues()
        {
            Logger.LogCurrentMethodName();
            await WsAction(async wsAdapter =>
            {
                await TokenBase.GetTokenInitAsync(wsAdapter);
                Logger.Info("method was work - GetTokenInitAsync");

                await TokenBase.GetTokenGetCertificate(wsAdapter);
                Logger.Info("method was work - GetTokenGetCertificate");

                await CertificateBase.GetCertificatesFromStore(StoreName, CheckChain, CheckCrl, wsAdapter);
                Logger.Info("method was work - GetCertificatesFromStore");

                await CryptoBase.GetSignXML(SqlIteReaderSocketValues.GetTestValue().SignXmlContent,
                    SqlIteReaderSocketValues.GetLoginValues().TokenThumb, wsAdapter);
                Logger.Info("method was work - GetSignXML");

                await CryptoBase.GetMrEncrypt(SqlIteReaderSocketValues.GetTestValue().MrEncryptContent, wsAdapter);
                Logger.Info("method was work - GetMrEncrypt");
            }, CommonInfo.AgentMainAddress);
        }

        [Test, Description("прием вторичной ПФР : Расшифровка")]
        public async Task GetSecondPfrDecrypt()
        {
            Logger.LogCurrentMethodName();
            await WsAction(async wsAdapter =>
            {
                await TokenBase.GetTokenInitAsync(wsAdapter);
                Logger.Info("method was work - GetTokenInitAsync");

                await SupportBase.GetRecipientInfoAsync(SqlIteReaderSocketValues.GetTestValue().RecipientInfoContent, wsAdapter);
                Logger.Info("method was work - GetRecipientInfoAsync");

                await TokenBase.GetTokenInitAsync(wsAdapter);
                Logger.Info("method was work - GetTokenInitAsync");

                await TokenBase.GetTokenGetCertificate(wsAdapter);
                Logger.Info("method was work - GetTokenGetCertificate");

                await CertificateBase.GetCertificatesFromStore(StoreName, CheckChain, CheckCrl, wsAdapter);
                Logger.Info("method was work - GetCertificatesFromStore");

                await CryptoBase.GetMrDecrypt(SqlIteReaderSocketValues.GetTestValue().MrDecryptContent, wsAdapter);
                Logger.Info("method was work - GetMrDecrypt");
            }, CommonInfo.AgentMainAddress);
        }

        [Test, Description("прием сообщения от ПФР: расшифровка")]
        public async Task GetLetterPfrDecrypt()
        {
            Logger.LogCurrentMethodName();
            await WsAction(async wsAdapter =>
            {
                await TokenBase.GetTokenInitAsync(wsAdapter);
                Logger.Info("method was work - GetTokenInitAsync");

                await SupportBase.GetRecipientInfoAsync(SqlIteReaderSocketValues.GetTestValue().RecipientInfoContent, wsAdapter);
                Logger.Info("method was work - GetRecipientInfoAsync");

                await TokenBase.GetTokenInitAsync(wsAdapter);
                Logger.Info("method was work - GetTokenInitAsync");

                await TokenBase.GetTokenGetCertificate(wsAdapter);
                Logger.Info("method was work - GetTokenGetCertificate");

                await CertificateBase.GetCertificatesFromStore(StoreName, CheckChain, CheckCrl, wsAdapter);
                Logger.Info("method was work - GetCertificatesFromStore");

                await CryptoBase.GetMrDecrypt(SqlIteReaderSocketValues.GetTestValue().MrDecryptContent, wsAdapter);
                Logger.Info("method was work - GetMrDecrypt");
            }, CommonInfo.AgentMainAddress);
        }

        [Test, Description("отправка в Росстат: открепленная подпись")]
        public async Task SendRosstatSign()
        {
            Logger.LogCurrentMethodName();
            await WsAction(async wsAdapter =>
            {
                await TokenBase.GetTokenInitAsync(wsAdapter);
                Logger.Info("method was work - GetTokenInitAsync");

                await TokenBase.GetTokenGetCertificate(wsAdapter);
                Logger.Info("method was work - GetTokenGetCertificate");

                await CertificateBase.GetCertificatesFromStore(StoreName, CheckChain, CheckCrl, wsAdapter);
                Logger.Info("method was work - GetCertificatesFromStore");

                await CryptoBase.GetMrSign(SqlIteReaderSocketValues.GetLoginValues().SignContentBase64,
                    SqlIteReaderSocketValues.GetLoginValues().TokenThumb, wsAdapter);
                Logger.Info("method was work - GetMrSign");
            }, CommonInfo.AgentMainAddress);
        }

        [Test, Description("отправка в Росстат: шифровать")]
        public async Task SendRosstatSignCrypt()
        {
            Logger.LogCurrentMethodName();
            await WsAction(async wsAdapter =>
            {
                await TokenBase.GetTokenInitAsync(wsAdapter);
                Logger.Info("method was work - GetTokenInitAsync");

                await TokenBase.GetTokenGetCertificate(wsAdapter);
                Logger.Info("method was work - GetTokenGetCertificate");

                await CertificateBase.GetCertificatesFromStore(StoreName, CheckChain, CheckCrl, wsAdapter);
                Logger.Info("method was work - GetCertificatesFromStore");

                await CryptoBase.GetMrSign(SqlIteReaderSocketValues.GetLoginValues().SignContentBase64,
                    SqlIteReaderSocketValues.GetLoginValues().TokenThumb, wsAdapter);
                Logger.Info("method was work - GetMrSign");

                await CryptoBase.GetMrDecrypt(SqlIteReaderSocketValues.GetTestValue().MrDecryptContent, wsAdapter);
                Logger.Info("method was work - GetMrDecrypt");
            }, CommonInfo.AgentMainAddress);
        }

        [Test, Description("вторичный прием Росстат: расшифровать ")]
        public async Task SendRosstatSignDeCrypt()
        {
            Logger.LogCurrentMethodName();
            await WsAction(async wsAdapter =>
            {
                await TokenBase.GetTokenInitAsync(wsAdapter);
                Logger.Info("method was work - GetTokenInitAsync");

                await SupportBase.GetRecipientInfoAsync(SqlIteReaderSocketValues.GetTestValue().RecipientInfoContent, wsAdapter);
                Logger.Info("method was work - GetRecipientInfoAsync");

                await TokenBase.GetTokenInitAsync(wsAdapter);
                Logger.Info("method was work - GetTokenInitAsync");

                await TokenBase.GetTokenGetCertificate(wsAdapter);
                Logger.Info("method was work - GetTokenGetCertificate");

                await CryptoBase.GetMrDecrypt(SqlIteReaderSocketValues.GetTestValue().MrDecryptContent, wsAdapter);
                Logger.Info("method was work - GetMrDecrypt");

            }, CommonInfo.AgentMainAddress);
        }

        [Test, Description("reception letter Rosstat: decrypt")]
        public async Task SendRosstatSignEnCrypt()
        {
            Logger.LogCurrentMethodName();
            await WsAction(async wsAdapter =>
            {
                await TokenBase.GetTokenInitAsync(wsAdapter);
                Logger.Info("method was work - GetMrDecrypt");

                await SupportBase.GetRecipientInfoAsync(SqlIteReaderSocketValues.GetTestValue().RecipientInfoContent, wsAdapter);
                Logger.Info("method was work - GetMrDecrypt");

                await TokenBase.GetTokenInitAsync(wsAdapter);
                Logger.Info("method was work - GetMrDecrypt");

                await TokenBase.GetTokenGetCertificate(wsAdapter);
                Logger.Info("method was work - GetMrDecrypt");

                await CertificateBase.GetCertificatesFromStore(StoreName, CheckChain, CheckCrl, wsAdapter);
                await CryptoBase.GetMrDecrypt(SqlIteReaderSocketValues.GetTestValue().MrDecryptContent, wsAdapter);
            }, CommonInfo.AgentMainAddress);
        }

        [Test, Description("отправка формы 4ФСС в ФСС: открепленная подпись")]
        public async Task FormFourAttachSign()
        {
            Logger.LogCurrentMethodName();
            await WsAction(async wsAdapter =>
            {
                await TokenBase.GetTokenInitAsync(wsAdapter);
                Logger.Info("method was work - GetTokenInitAsync");

                await TokenBase.GetTokenGetCertificate(wsAdapter);
                Logger.Info("method was work - GetTokenGetCertificate");

                await CertificateBase.GetCertificatesFromStore(StoreName, CheckChain, CheckCrl, wsAdapter);
                Logger.Info("method was work - GetCertificatesFromStore");

                await CryptoBase.GetMrSignAttach(SqlIteReaderSocketValues.GetLoginValues().SignContentBase64,
                    SqlIteReaderSocketValues.GetLoginValues().TokenThumb, wsAdapter);
                Logger.Info("method was work - GetMrSignAttach");

            }, CommonInfo.AgentMainAddress);
        }

        [Test, Description("Отправка формы 4ФСС в ФСС: шифрование")]
        public async Task FormFourAttachSignCrypt()
        {
            Logger.LogCurrentMethodName();
            await WsAction(async wsAdapter =>
            {
                await TokenBase.GetTokenInitAsync(wsAdapter);
                Logger.Info("method was work - GetTokenInitAsync");

                await TokenBase.GetTokenGetCertificate(wsAdapter);
                Logger.Info("method was work - GetTokenGetCertificate");

                await CertificateBase.GetCertificatesFromStore(StoreName, CheckChain, CheckCrl, wsAdapter);
                Logger.Info("method was work - GetCertificatesFromStore");

                await CryptoBase.GetMrSign(SqlIteReaderSocketValues.GetLoginValues().SignContentBase64,
                    SqlIteReaderSocketValues.GetLoginValues().TokenThumb, wsAdapter);
                Logger.Info("method was work - GetMrSign");

                await CryptoBase.GetMrEncrypt(SqlIteReaderSocketValues.GetTestValue().MrEncryptContent, wsAdapter);
                Logger.Info("method was work - GetMrEncrypt");

            }, CommonInfo.AgentMainAddress);
        }

        [Test, Description("прием  4ФСС из ФСС: шифровать")]
        public async Task FormFourAttachSignDecrypt()
        {
            Logger.LogCurrentMethodName();
            await WsAction(async wsAdapter =>
            {
                await TokenBase.GetTokenInitAsync(wsAdapter);
                Logger.Info("method was work - GetTokenInitAsync");

                await SupportBase.GetRecipientInfoAsync(SqlIteReaderSocketValues.GetTestValue().RecipientInfoContent, wsAdapter);
                Logger.Info("method was work - GetRecipientInfoAsync");

                await TokenBase.GetTokenInitAsync(wsAdapter);
                Logger.Info("method was work - GetTokenInitAsync");

                await TokenBase.GetTokenGetCertificate(wsAdapter);
                Logger.Info("method was work - GetTokenGetCertificate");

                await CertificateBase.GetCertificatesFromStore(StoreName, CheckChain, CheckCrl, wsAdapter);
                Logger.Info("method was work - GetCertificatesFromStore");

                await CryptoBase.GetMrDecrypt(SqlIteReaderSocketValues.GetTestValue().MrDecryptContent, wsAdapter);
                Logger.Info("method was work - GetMrDecrypt");

            }, CommonInfo.AgentMainAddress);
        }

        [Test, Description("Отправка ПОВЭД: soap-sign")]
        public async Task POVEDSoapSign()
        {
            Logger.LogCurrentMethodName();
            await WsAction(async wsAdapter =>
            {
                await TokenBase.GetTokenInitAsync(wsAdapter);
                Logger.Info("method was work - GetTokenInitAsync");

                await TokenBase.GetTokenGetCertificate(wsAdapter);
                Logger.Info("method was work - GetTokenGetCertificate");

                await CertificateBase.GetCertificatesFromStore(StoreName, CheckChain, CheckCrl, wsAdapter);
                Logger.Info("method was work - GetCertificatesFromStore");

                await CryptoBase.GetMrSignSoap(SqlIteReaderSocketValues.GetTestValue().SignSoapContentB64, XmlSigners, wsAdapter);
                Logger.Info("method was work - GetMrSignSoap");

                await TokenBase.GetTokenInitAsync(wsAdapter);
                Logger.Info("method was work - GetTokenInitAsync");

                await CertificateBase.GetCertificatesFromStore(StoreName, CheckChain, CheckCrl, wsAdapter);
                Logger.Info("method was work - GetCertificatesFromStore");

                await CryptoBase.GetMrSign(SqlIteReaderSocketValues.GetLoginValues().SignContentBase64,
                    SqlIteReaderSocketValues.GetLoginValues().TokenThumb, wsAdapter);
                Logger.Info("method was work - GetMrSign");
            }, CommonInfo.AgentMainAddress);
        }

        [Test, Description("прием ПОВЭД: расшифровка Содержания soap-xml")]
        public async Task POVEDSoapXml()
        {
            Logger.LogCurrentMethodName();
            await WsAction(async wsAdapter =>
            {
                await TokenBase.GetTokenInitAsync(wsAdapter);
                Logger.Info("method was work - GetTokenInitAsync");

                await SupportBase.GetRecipientInfoAsync(SqlIteReaderSocketValues.GetTestValue().RecipientInfoContent, wsAdapter);
                Logger.Info("method was work - GetRecipientInfoAsync");

                await TokenBase.GetTokenInitAsync(wsAdapter);
                Logger.Info("method was work - GetTokenInitAsync");

                await TokenBase.GetTokenGetCertificate(wsAdapter);
                Logger.Info("method was work - GetTokenGetCertificate");

                await CertificateBase.GetCertificatesFromStore(StoreName, CheckChain, CheckCrl, wsAdapter);
                Logger.Info("method was work - GetCertificatesFromStore");

                await CryptoBase.GetMrDecrypt(SqlIteReaderSocketValues.GetTestValue().MrDecryptContent, wsAdapter);
                Logger.Info("method was work - GetMrDecrypt");
            }, CommonInfo.AgentMainAddress);
        }

        [Test, Description("отправка ПВСО: soap-sign")]
        public async Task PVSOSoapSign()
        {
            Logger.LogCurrentMethodName();
            await WsAction(async wsAdapter =>
            {
                await TokenBase.GetTokenInitAsync(wsAdapter);
                Logger.Info("method was work - GetTokenInitAsync");

                await TokenBase.GetTokenGetCertificate(wsAdapter);
                Logger.Info("method was work - GetTokenGetCertificate");

                await CertificateBase.GetCertificatesFromStore(StoreName, CheckChain, CheckCrl, wsAdapter);
                Logger.Info("method was work - GetCertificatesFromStore");

                await CryptoBase.GetMrSignSoap(SqlIteReaderSocketValues.GetTestValue().SignSoapContentB64, XmlSigners, wsAdapter);
                Logger.Info("method was work - GetMrSignSoap");

                await CryptoBase.GetMrEncrypt(SqlIteReaderSocketValues.GetTestValue().MrEncryptContent, wsAdapter);
                Logger.Info("method was work - GetMrEncrypt");

            }, CommonInfo.AgentMainAddress);
        }

        [Test, Description("отправка ПВСО: soap-encrypt")]
        public async Task PVSOSoapCrypt()
        {
            Logger.LogCurrentMethodName();
            await WsAction(async wsAdapter =>
            {
                await TokenBase.GetTokenInitAsync(wsAdapter);
                Logger.Info("method was work - GetTokenInitAsync");

                await SupportBase.GetRecipientInfoAsync(SqlIteReaderSocketValues.GetTestValue().RecipientInfoContent, wsAdapter);
                Logger.Info("method was work - GetRecipientInfoAsync");

                await TokenBase.GetTokenInitAsync(wsAdapter);
                Logger.Info("method was work - GetTokenInitAsync");

                await TokenBase.GetTokenGetCertificate(wsAdapter);
                Logger.Info("method was work - GetTokenGetCertificate");

                await CertificateBase.GetCertificatesFromStore(StoreName, CheckChain, CheckCrl, wsAdapter);
                Logger.Info("method was work - GetCertificatesFromStore");
            }, CommonInfo.AgentMainAddress);
        }

        [Test, Description("подпись росприроднадзора")]
        public async Task RPNXmlSign()
        {
            Logger.LogCurrentMethodName();
            await WsAction(async wsAdapter =>
            {
                await TokenBase.GetTokenInitAsync(wsAdapter);
                Logger.Info("method was work - GetTokenInitAsync");

                await TokenBase.GetTokenGetCertificate(wsAdapter);
                Logger.Info("method was work - GetTokenGetCertificate");

                await CertificateBase.GetCertificatesFromStore(StoreName, CheckChain, CheckCrl, wsAdapter);
                Logger.Info("method was work - GetCertificatesFromStore");

                await CryptoBase.GetSignXML(SqlIteReaderSocketValues.GetTestValue().SignXmlContent,
                    SqlIteReaderSocketValues.GetLoginValues().TokenThumb, wsAdapter);
                Logger.Info("method was work - GetSignXML");

                await CryptoBase.GetMrEncrypt(SqlIteReaderSocketValues.GetTestValue().MrEncryptContent, wsAdapter);
                Logger.Info("method was work - GetMrEncrypt");
            }, CommonInfo.AgentMainAddress);
        }
    }
}
