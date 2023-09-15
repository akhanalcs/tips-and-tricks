using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using WebApp.Interfaces;

namespace WebApp.Services
{
    public class LogPusher : Hub, ILogPusher
    {
        private readonly IHubContext<LogPusher> _hub;

        public LogPusher(IHubContext<LogPusher> hub)
        {
            _hub = hub;
        }

        public Task SendLogAsync(string logMessage)
        {
            return _hub.Clients.All.SendAsync("ReceiveMessage", logMessage);
        }
    }
}
