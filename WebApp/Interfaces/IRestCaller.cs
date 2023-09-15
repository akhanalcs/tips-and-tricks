using RestSharp;
using System.Threading.Tasks;

namespace WebApp.Interfaces
{
    public interface IRestCaller
    {
        Task<(T, string)> SendRequestAndGetDataAsync<T>(RestRequest request, string callingProcessName);
    }
}