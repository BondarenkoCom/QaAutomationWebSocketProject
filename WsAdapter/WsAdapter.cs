using System;
using System.Threading.Tasks;
using WsAdapter.Interface;
using WsAdapter.WsWorker;

namespace WsAdapter
{
    public class WsAdapter :IWsAdapter
    {
        private readonly IWsWorker _workerWindows;
        private readonly IWsWorker _workerLinux;

        private IWsWorker GetActualWsWorker()
        {
            if (Environment.OSVersion.Platform == PlatformID.Unix ||
                Environment.OSVersion.Platform == PlatformID.MacOSX)
                return _workerLinux;
            return _workerWindows;
        }

        public WsAdapter(string address)
        {
            _workerWindows = new WsWorkerWin(address);
            _workerLinux = new WsWorkerLinux(address);
        }

        /// <inheritdoc />
        public async Task ConnectAndOperate(Action<IWsWorker> wsOperator)
        {
            var worker = GetActualWsWorker();
            try
            {
                await worker.Connect();
                wsOperator(worker);
            }
            finally
            {
                worker.Disconnect();
            }
        }

        public Task Open()
        {
            return GetActualWsWorker().Connect();
        }

        public void Close()
        {
            GetActualWsWorker().Disconnect();
        }

        public Task<string> SendMessageWithoutCloseSocket(string message)
        {
           return GetActualWsWorker().SendMessageWithoutCloseSocket(message);
        }
    }
}