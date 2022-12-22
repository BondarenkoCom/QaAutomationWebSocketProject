
using System;
using System.Threading.Tasks;
using LibrarySettings.Models;

namespace RestAdapter
{
    public interface IRestWork
    {
        public Task<RestApiModels> RestAction(string requestUrl, string request, Enum httpMethod);

        public string CheckPort();
    }
}
