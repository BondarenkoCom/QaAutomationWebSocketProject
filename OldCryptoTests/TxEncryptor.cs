using System.Text;
using System.Threading.Tasks;

namespace OldCryptoTests
{
    public class TxEncryptorWindows 
    {
        public async Task<string> UniversalMethod(string jsonIn)
        {
            return await Task.Run(() =>
            {
                unsafe
                {
                    var jsonInByte = Encoding.UTF8.GetBytes(jsonIn);
                    byte* resultByteJson = null;
                    var sizeJsonOutLength = 0;
                    ProxyCryptoC.UniversalMethod(jsonInByte, jsonInByte.Length, &resultByteJson,
                        &sizeJsonOutLength);
                    var result = Encoding.UTF8.GetString(resultByteJson, sizeJsonOutLength);
                    ProxyCryptoC.FreeBuffer(resultByteJson);
                    return result;
                }
            });
        }
    }
}
