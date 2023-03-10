using System.Runtime.InteropServices;

namespace OldCryptoTests
{
    public static class ProxyCryptoC
    {
        [DllImport(@"..\WindowsTestsNew\OldDllLibrary\npAgentCryptoAX.dll", EntryPoint = "universal_method")]
        public static extern unsafe void UniversalMethod(byte[] jsonIn, int jsonInLength, byte** jsonOut, int* jsonOutLength);
        
        [DllImport(@"..\WindowsTestsNew\OldDllLibrary\npAgentCryptoAX.dll", EntryPoint = "free_buffer")]
        public static extern unsafe void FreeBuffer(byte* ptr);
    }
}