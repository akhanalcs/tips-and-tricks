using System.Threading.Tasks;

namespace WebApp.Interfaces
{
    public interface ILogPusher
    {
        Task SendLogAsync(string logMessage);
    }
}
