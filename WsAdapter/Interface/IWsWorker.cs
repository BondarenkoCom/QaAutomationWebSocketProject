using System.Threading.Tasks;

namespace WsAdapter.Interface
{
    public interface IWsWorker
    {
        Task Connect();

        Task<string> SendMessageWithoutCloseSocket(string message);

        void Disconnect();
    }
}