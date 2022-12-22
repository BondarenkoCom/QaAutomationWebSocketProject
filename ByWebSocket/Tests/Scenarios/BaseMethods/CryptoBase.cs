using System.Collections.Generic;
using System.Threading.Tasks;
using Helpers.Serializations;
using NUnit.Framework;
using LibrarySettings;
using MessageGenerator.Crypto;
using MessageGenerator.Sign;
using Processor.Interfaces.DTO.Decrypt.MrDecrypt;
using Processor.Interfaces.DTO.Encrypt.MrEncrypt;
using Processor.Interfaces.DTO.Sign.MrSign;
using Processor.Interfaces.DTO.Sign.MrSignAttach;
using Processor.Interfaces.DTO.Sign.MrSignByHash;
using Processor.Interfaces.DTO.Sign.SignSoap;
using Processor.Interfaces.DTO.Sign.SignXml;
using WsAdapter.Interface;
using static LibrarySettings.ConstantsForMatch;

namespace WebSocketTests.Tests.Scenarios.BaseMethods
{
    public static class CryptoBase
    {
        public static async Task GetMrSign(string content, string certThumb , IWsAdapter wsAdapter)
        {
            var mrSign = MrSign.Generate(content, certThumb);
            var resMrSign = await wsAdapter.SendMessageWithoutCloseSocket(mrSign);
            var obj = SerializationHelpers.JsonDeserialize<MrSignOutputDto>(resMrSign);

            Assert.Multiple(() =>
            {
                Assert.That(obj.SignContent, Is.Not.Empty);
                Assert.That(obj.SignContent, Is.Not.Null);
                Assert.That(obj.SignContent, Does.Match(ResultPatternSign));

                Assert.That(resMrSign, Is.Not.Empty);
                Assert.That(resMrSign, Is.Not.Null);
                Assert.That(resMrSign, Does.Match(ResultPatternSignName));
            });
        }

        public static async Task GetSignByHash(string hash, string certThumb, IWsAdapter wsAdapter)
        {
            var message = MrSignByHash.Generate(hash, certThumb);
            var resHash = await wsAdapter.SendMessageWithoutCloseSocket(message);

            var obj = SerializationHelpers.JsonDeserialize<MrSignByHashOutDto>(resHash);

            Assert.Multiple(() =>
            {
                Assert.That(obj.SignContent, Does.Match(BeginPatternContent));
                Assert.That(obj.SignContent, Is.Not.Null);
                Assert.That(obj.SignContent, Is.Not.Empty);

                Assert.That(resHash, Does.Match(ResultNameSign));
                Assert.That(resHash, Is.Not.Null);
                Assert.That(resHash, Is.Not.Empty);
            });

        }

        //TODO MGA А что делает данный метод? Почему он лезет в базу?
        public static async Task GetMrDecrypt(string enContent , IWsAdapter wsAdapter)
        {
            var mrSign = MrSign.Generate(SqlIteReaderSocketValues.GetLoginValues().SignContentBase64, SqlIteReaderSocketValues.GetLoginValues().TokenThumb);
            var resMrSign = await wsAdapter.SendMessageWithoutCloseSocket(mrSign);
            var objMrSign = SerializationHelpers.JsonDeserialize<MrSignOutputDto>(resMrSign);

            var socketMrEncrypt = MrEncrypt.Generate(objMrSign.SignContent,
                new List<string> { $"{SqlIteReaderSocketValues.GetLoginValues().TokenThumb}" },
                new List<string> { });
            var resultMrEncrypt = await wsAdapter.SendMessageWithoutCloseSocket(socketMrEncrypt);
            var objMrEncrypt = SerializationHelpers.JsonDeserialize<MrEncryptOutDto>(resultMrEncrypt);

            var socketMrDecrypt = MrDecrypt.Generate(objMrEncrypt.EncContent);
            var resultMrDecrypt = await wsAdapter.SendMessageWithoutCloseSocket(socketMrDecrypt);
            var obj = SerializationHelpers.JsonDeserialize<MrDecryptOutputDto>(resultMrDecrypt);

            Assert.Multiple(() =>
            {
                Assert.That(obj.DecContent, Is.Not.Empty);
                Assert.That(obj.DecContent, Is.Not.Null);

                Assert.That(resultMrDecrypt, Does.Match(DecryptContentForMatch));
                Assert.That(resultMrDecrypt, Is.Not.Empty);
                Assert.That(resultMrDecrypt, Is.Not.Null);
            });
        }

        //TODO MGA А что делает данный метод? Почему он лезет в базу?
        public static async Task GetMrEncrypt(string encContent , IWsAdapter wsAdapter)
        {
            var mrSign = MrSign.Generate(SqlIteReaderSocketValues.GetLoginValues().SignContentBase64,
                SqlIteReaderSocketValues.GetLoginValues().TokenThumb);
            var resMrSign = await wsAdapter.SendMessageWithoutCloseSocket(mrSign);
            var objMrSign = SerializationHelpers.JsonDeserialize<MrSignOutputDto>(resMrSign);

            var socketMrEncrypt = MrEncrypt.Generate(objMrSign.SignContent,
                    new List<string> { $"{SqlIteReaderSocketValues.GetLoginValues().TokenThumb}" },
                    new List<string> { });
            var resultMrEncrypt = await wsAdapter.SendMessageWithoutCloseSocket(socketMrEncrypt);
            var obj = SerializationHelpers.JsonDeserialize<MrEncryptOutDto>(resultMrEncrypt);

            Assert.Multiple(() =>
            {
                Assert.That(obj.EncContent, Is.Not.Empty);
                Assert.That(obj.EncContent, Is.Not.Null);

                Assert.That(socketMrEncrypt, Is.Not.Empty);
                Assert.That(socketMrEncrypt, Is.Not.Null);
            });
        }

        public static async Task GetSignXML(string content64, string TokenThumb , IWsAdapter wsAdapter)
        {
            var signXml = SignXml.Generate(content64, TokenThumb);
            var resultSocket = await wsAdapter.SendMessageWithoutCloseSocket(signXml);
            var obj = SerializationHelpers.JsonDeserialize<SignXmlOutputDto>(resultSocket);

            Assert.Multiple(() =>
            {
                Assert.That(obj.SignContent, Is.Not.Empty);
                Assert.That(obj.SignContent, Is.Not.Null);

                Assert.That(resultSocket, Is.Not.Empty);
                Assert.That(resultSocket, Is.Not.Null);
                Assert.That(resultSocket, Does.Match(ResultPatternSignName));
            });
        }

        public static async Task GetMrSignAttach(string content, string thumb , IWsAdapter wsAdapter)
        {
            var mrSignAttach = MrSignAttach.Generate(content, thumb);
            var resultSignAttach = await wsAdapter.SendMessageWithoutCloseSocket(mrSignAttach);

            var obj = SerializationHelpers.JsonDeserialize<MrSignAttachOutputDto>(resultSignAttach);

            Assert.Multiple(() =>
            {
                Assert.That(obj.SignContent, Does.Match(BeginPatternContent));
                Assert.That(obj.SignContent, Is.Not.Null);
                Assert.That(obj.SignContent, Is.Not.Empty);

                Assert.That(resultSignAttach, Does.Match(ResultNameSign));
                Assert.That(resultSignAttach, Is.Not.Null);
                Assert.That(resultSignAttach, Is.Not.Empty);
            });
        }

        public static async Task GetMrSignSoap(string contentB64, string thumbContent , IWsAdapter wsAdapter)
        {
            var signSoapMes = SignSoap.Generate(contentB64, thumbContent);
            var resultSocket = await wsAdapter.SendMessageWithoutCloseSocket(signSoapMes);

            var obj = SerializationHelpers.JsonDeserialize<SignSoapOutputDto>(resultSocket);

            Assert.Multiple(() =>
            {
                Assert.That(obj.SignContent, Is.Not.Empty);
                Assert.That(obj.SignContent, Is.Not.Null);

                Assert.That(resultSocket, Is.Not.Empty);
                Assert.That(resultSocket, Is.Not.Null);
                Assert.That(resultSocket, Does.Match(ResultPatternSignName));
            });
        }
    }
}
