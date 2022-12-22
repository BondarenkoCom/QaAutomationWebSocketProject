using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using Constants;
using Helpers.SystemInfo;
using LibrarySettings.Models;
using RestSharp;

namespace RestAdapter
{
    public class RestWork: IRestWork
    {
        public async Task<RestApiModels> RestAction(string requestUrl, string request, Enum httpMethod)
        {
            try
            {
                var newClient = new RestClient(requestUrl);
                
                var req = new RestRequest(request, (Method)httpMethod);
        
                var response = await newClient.ExecuteAsync(req);
        
                return new RestApiModels
                {
                    ResponseStatusCode = (int)response.StatusCode,
                    ResponseContent = response.Content
                };
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        public string CheckPort()
        {
            using(TcpClient tcpClient = new TcpClient())
            {
                try
                {
                    var AvailabletPortCheck = TcpPortHelper.CheckPortIsAvailable(CommonInfo.PortTerminalAgent);
                    if (AvailabletPortCheck == false)
                    {
                        return CommonInfo.PortAgent.ToString();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message , e.Data);
                }
            }
            return null;
        }
    }
}