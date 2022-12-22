using System;
using System.Threading.Tasks;

namespace WsAdapter.Interface
{
    public interface IWsAdapter
    {
        Task Open();

        void Close();

        Task<string> SendMessageWithoutCloseSocket(string message);

        Task ConnectAndOperate(Action<IWsWorker> wsOperator);
    }
}