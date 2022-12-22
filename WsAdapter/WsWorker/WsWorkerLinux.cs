using System;
using System.Threading.Tasks;
using Websocket.Client;
using WsAdapter.Interface;

namespace WsAdapter.WsWorker
{
    public class WsWorkerLinux : IWsWorker
    {
        private readonly string _address;
        private WebsocketClient _websocketClient;

        public WsWorkerLinux(string address)
        {
            _address = address;
        }

        /// <inheritdoc />
        public async Task Connect()
        {
            _websocketClient = new WebsocketClient(new Uri(_address));
            await _websocketClient.Start();
        }

        public Task<string> SendMessageWithoutCloseSocket(string message)
        {
            return null;
        }

        /// <inheritdoc />
        public void Disconnect()
        {
            _websocketClient.Dispose();
        }
    }
}