using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Constants;
using WebSocketSharp;
using WsAdapter.Interface;

namespace WsAdapter.WsWorker
{
    public class WsWorkerWin : IWsWorker
    {
        private readonly WebSocket _wsConnection;

        public WsWorkerWin(string address)
        {
            _wsConnection = new WebSocket(address);
        }

        public Task Connect()
        {
            _wsConnection.Connect();
            return Task.CompletedTask;
        }

        public async Task<string> SendMessageWithoutCloseSocket(string message)
        {
            var result = string.Empty;
            var needStop = false;
            var sockError = string.Empty;

            _wsConnection.OnMessage += (sender, e) =>
            {
                result = e.Data;
            };

            _wsConnection.OnClose += (sender, e) =>
            {
                sockError = e.Reason;
                needStop = true;
            };

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            try
            {
                _wsConnection.Send(message);
                do
                {
                    if (needStop)
                        throw new Exception($"Соединение было закрыто! Запрос [{message}] , " +
                                            $"причина Ошибки {sockError}");

                    if (stopwatch.Elapsed > CommonInfo.TimeOfMaxWaitWs)
                        throw new Exception(
                            $"Ответ по ws не был да за [{CommonInfo.TimeOfMaxWaitWs}] на запрос [{message}]");

                    if (!Thread.Yield())
                        await Task.Delay(TimeSpan.FromMilliseconds(100));

                } while (string.IsNullOrEmpty(result));

                return result;
            }
            finally
            {
                stopwatch.Stop();
            }
        }

        public void Disconnect()
        {
            _wsConnection.Close();
        }
    }
}