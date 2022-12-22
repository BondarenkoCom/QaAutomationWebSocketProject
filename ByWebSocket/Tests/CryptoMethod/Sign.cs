using System.Threading.Tasks;
using LibrarySettings;
using MessageGenerator.Sign;
using NUnit.Framework;
using MessageGenerator.Sign.Check;
using System.Collections.Generic;
using Helpers.Serializations;

using LogSupport;

using MessageGenerator.Crypto;
using Processor.Interfaces.DTO.Decrypt.MrDecrypt;
using Processor.Interfaces.DTO.Encrypt.MrEncrypt;
using Processor.Interfaces.DTO.Sign.Common;
using Processor.Interfaces.DTO.Sign.MrSign;
using Processor.Interfaces.DTO.Sign.MrSignAttach;
using Processor.Interfaces.DTO.Sign.MrSignByHash;
using Processor.Interfaces.DTO.Sign.SignSoap;
using Processor.Interfaces.DTO.Sign.SignXml;
using Processor.Interfaces.DTO.Support.GetRecipientInfo;
using Processor.Interfaces.DTO.Support.MrUnpackSignedFile;
using TestBase;
using static LibrarySettings.ConstantsForMatch;
using static LibrarySettings.ConstsForInput;
using Constants;

namespace WebSocketTests.Tests.CryptoMethod
{
    [TestFixture]
    public class Sign : WsTestBase<Sign>
    {

        [Test]
        public async Task Test_ByHash()
        {
            Logger.LogCurrentMethodName();

            await WsAction(async wsAdapter =>
            {
                var message = MrSignByHash.Generate(HashContent, SqlIteReaderSocketValues.GetLoginValues().TokenThumb);
                Logger.Info($"Сгенерировать MrSignByHash сокет запрос - {message}");

                var resHash = await wsAdapter.SendMessageWithoutCloseSocket(message);
                Logger.Info($"Результат сокета запроса - {resHash}");

                var obj = SerializationHelpers.JsonDeserialize<MrSignByHashOutDto>(resHash);
                Logger.Info("десериализация объекта");

                Assert.Multiple(() =>
                {
                    Assert.That(resHash, Does.Match(ResultNameSign));
                    Assert.That(resHash, Is.Not.Null);
                    Assert.That(resHash, Is.Not.Empty);

                    Assert.That(obj.SignContent, Does.Match(ResultPatternSign));
                    Assert.That(obj.SignContent, Is.Not.Null);
                    Assert.That(obj.SignContent, Is.Not.Empty);
                });
            }, CommonInfo.AgentMainAddress);
        }

        [Test]
        public async Task Test_MrSign()
        {
            Logger.LogCurrentMethodName();

            await WsAction(async wsAdapter =>
            {
             
                var mrSign = MrSign.Generate(SqlIteReaderSocketValues.GetLoginValues().SignContentBase64,
                    SqlIteReaderSocketValues.GetLoginValues().TokenThumb);
                Logger.Info($"Сгенерировать MrSign сокет запрос - {mrSign}");

                var checkName = CheckTokenName.GetCardName();
                
                Logger.Info($"имя токена(если есть) - {checkName}");
                
                var resMrSign = await wsAdapter.SendMessageWithoutCloseSocket(mrSign);
                Logger.Info($"Результат сокета запроса - {resMrSign}");

                var objMrSign = SerializationHelpers.JsonDeserialize<MrSignOutputDto>(resMrSign);
                Logger.Info("десериализация объекта");

                Assert.Multiple(() =>
                {
                    Assert.That(objMrSign.SignContent, Is.Not.Empty);
                    Assert.That(objMrSign.SignContent, Is.Not.Null);
                    Assert.That(objMrSign.SignContent, Does.Match(ResultPatternSign));

                    Assert.That(resMrSign, Is.Not.Empty);
                    Assert.That(resMrSign, Is.Not.Null);
                    Assert.That(resMrSign, Does.Match(ResultPatternSignName));
                });
            },CommonInfo.AgentMainAddress);
        }

        [Test]
        public async Task Test_GetRecipientInfoAsync()
        {
            Logger.LogCurrentMethodName();

            await WsAction(async wsAdapter =>
            {
                var socketReceipt =
                    GetRecipientInfo.Generate(SqlIteReaderSocketValues.GetTestValue().RecipientInfoContent);
                Logger.Info($"Сгенерировать GetRecipientInfo сокет запрос - {socketReceipt}");

                var resultReceipt = await wsAdapter.SendMessageWithoutCloseSocket(socketReceipt);
                Logger.Info($"socket responce - {resultReceipt}");

                var obj = SerializationHelpers.JsonDeserialize<GetRecipientInfoOutputDto>(resultReceipt);
                Logger.Info("десериализация объекта");

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
                    Assert.That(resultReceipt, Does.Match(ResultNameRecipients));
                });
            },CommonInfo.AgentMainAddress);
        }

        [Test]
        public async Task Test_SignSoap()
        {
            Logger.LogCurrentMethodName();

            await WsAction(async wsAdapter =>
            {
                var signSoapMes = SignSoap.Generate(SqlIteReaderSocketValues.GetTestValue().SignSoapContentB64,
                    XmlSigners);
                Logger.Info($"Сгенерировать SignSoap сокет запрос - {signSoapMes}");

                var resultSocket = await wsAdapter.SendMessageWithoutCloseSocket(signSoapMes);
                Logger.Info($"Результат сокета запроса - {resultSocket}");

                var obj = SerializationHelpers.JsonDeserialize<SignSoapOutputDto>(resultSocket);
                Logger.Info($"десериализация объекта - {obj.SignContent}");

                Assert.Multiple(() =>
                {
                    Assert.That(obj.SignContent, Is.Not.Empty);
                    Assert.That(obj.SignContent, Is.Not.Null);

                    Assert.That(resultSocket, Is.Not.Empty);
                    Assert.That(resultSocket, Is.Not.Null);
                    Assert.That(resultSocket, Does.Match(ResultPatternSignName));
                });
            }, CommonInfo.AgentMainAddress);
        }

        [Test]
        public async Task Test_SignXml()
        {
            Logger.LogCurrentMethodName();

            await WsAction(async wsAdapter =>
            {
                var signXml = SignXml.Generate(SqlIteReaderSocketValues.GetTestValue().SignXmlContent,
                    SqlIteReaderSocketValues.GetLoginValues().TokenThumb);
                Logger.Info($"Сгенерировать SignXml сокет запрос - {signXml}");

                var resultSocket = await wsAdapter.SendMessageWithoutCloseSocket(signXml);
                Logger.Info($"Результат сокета запроса - {resultSocket}");

                var obj = SerializationHelpers.JsonDeserialize<SignXmlOutputDto>(resultSocket);
                Logger.Info("десериализация объекта");

                Assert.Multiple(() =>
                {
                    Assert.That(obj.SignContent, Is.Not.Empty);
                    Assert.That(obj.SignContent, Is.Not.Null);

                    Assert.That(resultSocket, Is.Not.Empty);
                    Assert.That(resultSocket, Is.Not.Null);
                    Assert.That(resultSocket, Does.Match(ResultPatternSignName));
                });
            }, CommonInfo.AgentMainAddress);
        }

        [Test]
        public async Task Test_MrSignAttach()
        {
            Logger.LogCurrentMethodName();

            await WsAction(async wsAdapter =>
            {
                var mrSignAttach = MrSignAttach.Generate(SqlIteReaderSocketValues.GetLoginValues().SignContentBase64,
                    SqlIteReaderSocketValues.GetLoginValues().TokenThumb);
                Logger.Info($"Сгенерировать MrSignAttach сокет запрос - {mrSignAttach}");

                var resultSignAttach = await wsAdapter.SendMessageWithoutCloseSocket(mrSignAttach);
                Logger.Info($"Результат сокета запроса - {resultSignAttach}");

                var obj = SerializationHelpers.JsonDeserialize<MrSignAttachOutputDto>(resultSignAttach);
                Logger.Info("десериализация объекта");

                Assert.Multiple(() =>
                {

                    Assert.That(obj.SignContent, Does.Match(ResultPatternSign));
                    Assert.That(obj.SignContent, Is.Not.Null);
                    Assert.That(obj.SignContent, Is.Not.Empty);

                    Assert.That(resultSignAttach, Does.Match(ResultNameSign));
                    Assert.That(resultSignAttach, Is.Not.Null);
                    Assert.That(resultSignAttach, Is.Not.Empty);
                });
            },CommonInfo.AgentMainAddress);
        }

        [Test]
        public async Task Test_MrUnpackSignedFile()
        {
            Logger.LogCurrentMethodName();

            await WsAction(async wsAdapter =>
            {
                var unpackSignMessage = MrUnpackSignedFile.Generate(SqlIteReaderSocketValues.GetTestValue().UnpackSignedContent);
                Logger.Info($"Сгенерировать MrUnpackSignedFile сокет запрос - {unpackSignMessage}");

                var resultUnpackSign = await wsAdapter.SendMessageWithoutCloseSocket(unpackSignMessage);
                Logger.Info($"Результат сокета запроса - {resultUnpackSign}");

                var obj = SerializationHelpers.JsonDeserialize<MrUnpackSignedFileOutputDto>(resultUnpackSign);
                Logger.Info("десериализация объекта");

                Assert.Multiple(() =>
                {
                    Assert.That(obj.UnpackData, Does.Match(SqlIteReaderSocketValues.GetLoginValues().SignContentBase64));
                    Assert.That(obj.UnpackData, Is.Not.Empty);
                    Assert.That(obj.UnpackData, Is.Not.Null);

                    Assert.That(resultUnpackSign, Does.Match(ResultNameUnpack));
                    Assert.That(resultUnpackSign, Is.Not.Empty);
                    Assert.That(resultUnpackSign, Is.Not.Null);
                });
            }, CommonInfo.AgentMainAddress);
        }

        [Test]
        public async Task Test_VerifySoap()
        {
            Logger.LogCurrentMethodName();

            await WsAction(async wsAdapter =>
            {
                var soapMes = VerifySignatureSoap.Generate(SqlIteReaderSocketValues.GetTestValue().SignSoapContentB64);
                Logger.Info($"Сгенерировать VerifySignatureSoap сокет запрос - {soapMes}");

                var resultVerifySoap = await wsAdapter.SendMessageWithoutCloseSocket(soapMes);
                Logger.Info($"Результат сокета запроса - {resultVerifySoap}");

                var obj = SerializationHelpers.JsonDeserialize<CheckSignBaseDto>(resultVerifySoap);
                Logger.Info("десериализация объекта");

                Assert.Multiple(() =>
                {
                    Assert.That(obj.SgnsCount.ToString(), Does.Match(ResultTokenInit.ToString()));
                    Assert.That(obj.IsSignOk.ToString(), Does.Match(AnswerTrueForMatch));

                    Assert.That(obj.SgnsInfo[0].SgnCertThumb, Is.Not.Empty);
                    Assert.That(obj.SgnsInfo[0].SgnCertThumb, Is.Not.Null);

                    Assert.That(resultVerifySoap, Does.Match(ResultVerfSoapCount));
                    Assert.That(resultVerifySoap, Does.Match(ResultVerfSoapBool));
                    Assert.That(resultVerifySoap, Is.Not.Empty);
                    Assert.That(resultVerifySoap, Is.Not.Null);
                });
            }, CommonInfo.AgentMainAddress);
        }

        [Test]
        public async Task Test_MrEncryptAsync()
        {
            Logger.LogCurrentMethodName();

            await WsAction(async wsAdapter =>
            {
                var mrSign = MrSign.Generate(SqlIteReaderSocketValues.GetLoginValues().SignContentBase64,
                    SqlIteReaderSocketValues.GetLoginValues().TokenThumb);
                Logger.Info($"Сгенерировать MrSign сокет запрос - {mrSign}");

                var resMrSign = await wsAdapter.SendMessageWithoutCloseSocket(mrSign);
                Logger.Info($"Результат сокета запроса - {resMrSign}");

                var objMrSign = SerializationHelpers.JsonDeserialize<MrSignOutputDto>(resMrSign);
                Logger.Info("десериализация объекта");

                var socketMrEncrypt = MrEncrypt.Generate(objMrSign.SignContent,
                    new List<string> { $"{SqlIteReaderSocketValues.GetLoginValues().TokenThumb}" },
                    new List<string>( ));
                Logger.Info($"Сгенерировать MrEncrypt сокет запрос - {objMrSign.SignContent}");

                var resultMrEncrypt = await wsAdapter.SendMessageWithoutCloseSocket(socketMrEncrypt);
                Logger.Info($"Результат сокета запроса - {resultMrEncrypt}");

                var objMrEncrypt = SerializationHelpers.JsonDeserialize<MrEncryptOutDto>(resultMrEncrypt);
                Logger.Info("десериализация объекта");

                Assert.Multiple(() =>
                {
                    Assert.That(objMrEncrypt.EncContent, Is.Not.Empty);
                    Assert.That(objMrEncrypt.EncContent, Is.Not.Null);

                    Assert.That(socketMrEncrypt, Is.Not.Empty);
                    Assert.That(socketMrEncrypt, Is.Not.Null);
                });
            },CommonInfo.AgentMainAddress);
        }

        [Test]
        public async Task Test_TestMrDecrypt()
        {
            Logger.LogCurrentMethodName();

            await WsAction(async wsAdapter =>
            {
                var mrSign = MrSign.Generate(SqlIteReaderSocketValues.GetLoginValues().SignContentBase64,
                    SqlIteReaderSocketValues.GetLoginValues().TokenThumb);
                Logger.Info($"Сгенерировать MrSign сокет запрос - {mrSign}");

                var resMrSign = await wsAdapter.SendMessageWithoutCloseSocket(mrSign);
                Logger.Info($"Результат сокета запроса - {resMrSign}");

                var objMrSign = SerializationHelpers.JsonDeserialize<MrSignOutputDto>(resMrSign);
                Logger.Info("десериализация объекта");

                var socketMrEncrypt = MrEncrypt.Generate(objMrSign.SignContent,
                    new List<string> { $"{SqlIteReaderSocketValues.GetLoginValues().TokenThumb}" },
                    new List<string>());
                Logger.Info($"Сгенерировать socketMrEncrypt сокет запрос - {socketMrEncrypt}");

                var resultMrEncrypt = await wsAdapter.SendMessageWithoutCloseSocket(socketMrEncrypt);
                Logger.Info($"Результат сокета запроса - {resultMrEncrypt}");

                var objMrEncrypt = SerializationHelpers.JsonDeserialize<MrEncryptOutDto>(resultMrEncrypt);
                Logger.Info("десериализация объекта");

                var socketMrDecrypt = MrDecrypt.Generate(objMrEncrypt.EncContent);
                Logger.Info($"Сгенерировать MrDecrypt сокет запрос - {socketMrDecrypt}");

                var resultMrDecrypt = await wsAdapter.SendMessageWithoutCloseSocket(socketMrDecrypt);
                Logger.Info($"Результат сокета запроса - {resultMrDecrypt}");

                var obj = SerializationHelpers.JsonDeserialize<MrDecryptOutputDto>(resultMrDecrypt);
                Logger.Info("десериализация объекта");

                Assert.Multiple(() =>
                {
                    Assert.That(obj.DecContent, Is.Not.Empty);
                    Assert.That(obj.DecContent, Is.Not.Null);

                    Assert.That(resultMrDecrypt, Does.Match(DecryptContentForMatch));
                    Assert.That(resultMrDecrypt, Is.Not.Empty);
                    Assert.That(resultMrDecrypt, Is.Not.Null);
                });
            },CommonInfo.AgentMainAddress);
        }

        [Test]
        public async Task Test_MrCheckSign()
        {
            Logger.LogCurrentMethodName();

            await WsAction(async wsAdapter =>
            {
                var socketMrSign = MrCheckSign.Generate(SqlIteReaderSocketValues.GetLoginValues().SignContentBase64,
                    SqlIteReaderSocketValues.GetTestValue().MrCheckSignSgnContent);
                Logger.Info($"Сгенерировать MrCheckSign сокет запрос - {socketMrSign}");

                var resultMrDecrypt = await wsAdapter.SendMessageWithoutCloseSocket(socketMrSign);
                Logger.Info($"socket responce - {resultMrDecrypt}");

                var obj = SerializationHelpers.JsonDeserialize<CheckSignBaseDto>(resultMrDecrypt);
                Logger.Info("Deserialize object");

                Assert.Multiple(() =>
                {
                    Assert.That(obj.IsSignOk.ToString(), Does.Match(AnswerTrueForMatch));
                    Assert.That(obj.SgnsCount.ToString(), Does.Match($"{1}"));

                    Assert.That(obj.SgnsInfo[0].SgnCertThumb, Is.Not.Empty);
                    Assert.That(obj.SgnsInfo[0].SgnCertThumb, Is.Not.Null);

                    Assert.That(obj.SgnsInfo[0].SgnCn, Is.Not.Empty);
                    Assert.That(obj.SgnsInfo[0].SgnCn, Is.Not.Null);

                    Assert.That(resultMrDecrypt, Is.Not.Empty);
                    Assert.That(resultMrDecrypt, Is.Not.Null);
                    Assert.That(resultMrDecrypt, Does.Match(ResultMrDecryptForMatch));
                });
            },CommonInfo.AgentMainAddress);
        }
    }
}