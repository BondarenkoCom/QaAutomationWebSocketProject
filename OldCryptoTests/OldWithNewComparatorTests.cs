using NUnit.Framework;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading;
using Helpers.Serializations;
using LibrarySettings;

using LogSupport;

using MessageGenerator.Crypto;
using MessageGenerator.Sign;
using MessageGenerator.Sign.Check;
using Processor.Interfaces.DTO.Decrypt.MrDecrypt;
using Processor.Interfaces.DTO.Encrypt.MrEncrypt;
using Processor.Interfaces.DTO.Sign.MrSign;
using Processor.Interfaces.DTO.Sign.MrSignAttach;
using Processor.Interfaces.DTO.Sign.SignSoap;
using WsAdapter.Interface;
using TestBase;
using Constants;

namespace OldCryptoTests
{
    [TestFixture, Description("Тесты обращаются к старой крипто библиотеке прогоняют данные и делают тоже самое но уже через актуальную версию агента")]
    public class OldWithNewComparatorTests : WsTestBase<OldWithNewComparatorTests> 
    {
        private readonly TxEncryptorWindows _encryptor = new();

        [Test, Category("Old Crypto Tests")]
        [TestCase(1, 1)]
        [TestCase(1, 2)]
        [TestCase(2, 1)]
        public async Task Test_mrSign_And_MrCheckSign_Check_Old_Crypto(int typeOfOldDll, int typeOfNewDll)
        {
            Logger.LogCurrentMethodName();

            await WsAction(async wsAdapter =>
            {
                var typeOfOldDllFirst = (EnumRequests.TypeOfWork)typeOfOldDll;
                var typeOfNewDllSecond = (EnumRequests.TypeOfWork)typeOfNewDll;

                var mrSign = MrSign.Generate(SqlIteReaderSocketValues.GetLoginValues().SignContentBase64,
                    $"{SqlLiteReaderCertificateFromStoreForTests.GetLocalThumb()}");
                Logger.Info($"Сгенерировать MrSign сокет запрос - {mrSign}");

                var res = await MakeOperation(typeOfOldDllFirst, mrSign, wsAdapter);
                Logger.Info($"Результат сокета запроса - {res}");

                var obj = SerializationHelpers.JsonDeserialize<MrSignOutputDto>(res);
                var soapVerifyMes = MrCheckSign.Generate(SqlIteReaderSocketValues.GetLoginValues().SignContentBase64,
                    obj.SignContent);
                Logger.Info($"Сгенерировать MrCheckSign сокет запрос - {soapVerifyMes}");

                var resVerifySoap = await MakeOperation(typeOfNewDllSecond, soapVerifyMes, wsAdapter);
                Logger.Info($"Результат сокета запроса - {resVerifySoap}");

                Assert.Multiple(() =>
                {
                    Assert.That(resVerifySoap, Is.Not.Empty);
                    Assert.That(resVerifySoap, Is.Not.Null);
                    Assert.That(resVerifySoap, Does.Match("{\"sgnsCount\":1"));
                });
            }, CommonInfo.AgentMainAddress);
        }

        [Test, Category("Old Crypto Tests")]
        [TestCase(1, 1)]
        [TestCase(1, 2)]
        [TestCase(2, 1)]
        public async Task Test_mrSign_And_MrGetCertsInfoFromSign_Check_Old_Crypto(int typeOfOldDll, int typeOfNewDll)
        {
            Logger.LogCurrentMethodName();

            await WsAction(async wsAdapter =>
            {
                var typeOfOldDllFirst = (EnumRequests.TypeOfWork)typeOfOldDll;
                var typeOfNewDllSecond = (EnumRequests.TypeOfWork)typeOfNewDll;

                var mrSign = MrSign.Generate(SqlIteReaderSocketValues.GetLoginValues().SignContentBase64,
                    $"{SqlLiteReaderCertificateFromStoreForTests.GetLocalThumb()}");
                Logger.Info($"Сгенерировать MrSign сокет запрос - {mrSign}");

                var res = await MakeOperation(typeOfOldDllFirst, mrSign, wsAdapter);
                Logger.Info($"Результат сокета запроса - {res}");

                var obj = SerializationHelpers.JsonDeserialize<MrSignOutputDto>(res);
                var mrGetCertsMes = MrGetCertsInfoFromSign.Generate(obj.SignContent);
                Logger.Info($"Сгенерировать MrSign сокет запрос - {mrGetCertsMes}");

                var resVerifySoap = await MakeOperation(typeOfNewDllSecond, mrGetCertsMes, wsAdapter);
                Logger.Info($"Результат сокета запроса - {resVerifySoap}");

                Assert.Multiple(() =>
                {
                    Assert.That(resVerifySoap, Is.Not.Empty);
                    Assert.That(resVerifySoap, Is.Not.Null);
                    Assert.That(resVerifySoap, Does.Match("{\"certificates\":"));
                });
            }, CommonInfo.AgentMainAddress);
        }

        [Test, Category("Sign Soap check into VerifySignatureSoap")]
        [Order(1)]
        [TestCase(1, 1)] 
        [TestCase(1, 2)]
        [TestCase(2, 1)]
        public async Task Test_OldCrypto_SignSoap_And_Check_OldCrypto_Verify_Old_Cry(int typeOfOldDll, int typeOfNewDll)
        {
            Logger.LogCurrentMethodName();

            await WsAction(async wsAdapter =>
            {
                var typeOfOldDllFirst = (EnumRequests.TypeOfWork)typeOfOldDll;
                var typeOfNewDllSecond = (EnumRequests.TypeOfWork)typeOfNewDll;

                var signSoapMes = SignSoap.Generate(SqlIteReaderSocketValues.GetTestValue().SignSoapContentB64,
                    ConstantsForMatch.XmlSigners);
                Logger.Info($"Сгенерировать SignSoap сокет запрос - {signSoapMes}");

                var res = await MakeOperation(typeOfOldDllFirst, signSoapMes, wsAdapter);
                Logger.Info($"Результат сокета запроса - {res}");

                var obj = SerializationHelpers.JsonDeserialize<SignSoapOutputDto>(res);

                var soapVerifyMes = VerifySignatureSoap.Generate(obj.SignContent);
                Logger.Info($"Сгенерировать VerifySignatureSoap сокет запрос - {soapVerifyMes}");

                var resVerifySoap = await MakeOperation(typeOfNewDllSecond, soapVerifyMes, wsAdapter);
                Logger.Info($"Результат сокета запроса - {resVerifySoap}");

                Assert.Multiple(() =>
                {
                    Assert.That(resVerifySoap, Is.Not.Empty);
                    Assert.That(resVerifySoap, Is.Not.Null);
                    Assert.That(resVerifySoap, Does.Match(ConstantsForMatch.ResultNameSignSecond));
                });
            }, CommonInfo.AgentMainAddress);
        }

        [Test, Category("Old Crypto Tests")]
        [TestCase(1, 1)]
        [TestCase(1, 2)]
        [TestCase(2, 1)]
        public async Task Test_MrEncrypt_And_Check_Decrypt_Old_Cry(int typeOfOldDll, int typeOfNewDll)
        {
            Logger.LogCurrentMethodName();

            await WsAction(async wsAdapter =>
            {
                var typeOfOldDllFirst = (EnumRequests.TypeOfWork)typeOfOldDll;
                var typeOfNewDllSecond = (EnumRequests.TypeOfWork)typeOfNewDll;

                var socketMrEncrypt =
                    MrEncrypt.Generate
                    (SqlIteReaderSocketValues.GetTestValue().MrEncryptContent,
                        new List<string> { $"{SqlLiteReaderCertificateFromStoreForTests.GetLocalThumb()}" },
                        new List<string> { "" });
                Logger.Info($"Сгенерировать MrEncrypt сокет запрос - {socketMrEncrypt}");

                var res = await MakeOperation(typeOfOldDllFirst, socketMrEncrypt, wsAdapter);
                Logger.Info("отработал метод MakeOperation");

                var obj = SerializationHelpers.JsonDeserialize<MrEncryptOutDto>(res);

                var mrDecryptMes = MrDecrypt.Generate(obj.EncContent);
                Logger.Info($"Сгенерировать MrDecrypt сокет запрос - {mrDecryptMes}");

                var resDecrypt = await MakeOperation(typeOfNewDllSecond, mrDecryptMes, wsAdapter);
                Logger.Info($"метод отработал- {resDecrypt}");

                var resObj = SerializationHelpers.JsonDeserialize<MrDecryptOutputDto>(resDecrypt);
                Logger.Info($"метод отработал- {resObj}");

                Assert.Multiple(() =>
                {
                    Assert.That(resObj.DecContent, Is.Not.Empty);
                    Assert.That(resObj.DecContent, Is.Not.Null);

                    Assert.That(resDecrypt, Is.Not.Empty);
                    Assert.That(resDecrypt, Is.Not.Null);
                    Assert.AreEqual(resDecrypt, "enc_content");
                });
            }, CommonInfo.AgentMainAddress);
        }

        [Test, Category("Old Crypto Tests")]
        [TestCase(1, 1)]
        [TestCase(1, 2)]
        [TestCase(2, 1)]
        public async Task Test_SignAttach_And_MrUnpackSignedFile_Cry(int typeOfOldDll, int typeOfNewDll)
        {
            Logger.LogCurrentMethodName();

            await WsAction(async wsAdapter =>
            {
                var typeOfOldDllFirst = (EnumRequests.TypeOfWork)typeOfOldDll;
                var typeOfNewDllSecond = (EnumRequests.TypeOfWork)typeOfNewDll;

                var mrSignAttach = MrSignAttach.Generate(SqlIteReaderSocketValues.GetLoginValues().SignContentBase64,
                    SqlIteReaderSocketValues.GetLoginValues().TokenThumb);
                Logger.Info($"Сгенерировать mrSignAttach сокет запрос - {mrSignAttach}");

                var res = await MakeOperation(typeOfOldDllFirst, mrSignAttach, wsAdapter);
                Logger.Info($"метод отработал- {res}");

                var obj = SerializationHelpers.JsonDeserialize<MrSignAttachOutputDto>(res);

                var signAttachments = MrUnpackSignedFile.Generate(obj.SignContent);
                Logger.Info($"Сгенерировать MrUnpackSignedFile сокет запрос - {signAttachments}");

                var resSignAttach = await MakeOperation(typeOfNewDllSecond, signAttachments, wsAdapter);
                Logger.Info($"метод отработал- {resSignAttach}");

                Assert.Multiple(() =>
                {
                    Assert.That(resSignAttach, Is.Not.Empty);
                    Assert.That(resSignAttach, Is.Not.Null);
                    Assert.That(resSignAttach, Does.Match("unpack_data"));
                    Thread.Sleep(2000);
                    Console.WriteLine("Test_SignAttach_And_MrUnpackSignedFile_Cry test accept");
                });
            }, CommonInfo.AgentMainAddress);
        }

        private async Task<string> MakeOperation(EnumRequests.TypeOfWork type, string message, IWsAdapter wsAdapter)
        {
            if (type == EnumRequests.TypeOfWork.WithAgent)
                return await wsAdapter.SendMessageWithoutCloseSocket(message);
            return await _encryptor.UniversalMethod(message);
        }
    }
}