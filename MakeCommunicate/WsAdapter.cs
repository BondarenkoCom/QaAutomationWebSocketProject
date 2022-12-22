using System.Text.Json;
using System.Threading.Tasks;
using WsAdapter.Interface;

namespace MakeCommunicate
{
    public static class WsAdapter
    {
        public static async Task<T> MakeOperateWith<T>(this IWsAdapter wsAdapter, string message)
        {
            return JsonSerializer.Deserialize<T>(await wsAdapter.SendMessageWithoutCloseSocket(message));
        }
    }
}