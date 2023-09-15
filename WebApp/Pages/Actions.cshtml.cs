using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Interfaces;
using WebApp.Models;

namespace WebApp.Pages
{
    public class ActionsModel : PageModel
    {
        private readonly ILogger<ActionsModel> _logger;
        private readonly IAppRepository _appRepository;
        private readonly ILogPusher _logsPusher;

        public ActionsModel(ILogger<ActionsModel> logger, IAppRepository appRepository, ILogPusher logsPusher)
        {
            _appRepository = appRepository;
            _logger = logger;
            _logsPusher = logsPusher;
        }

        //Now I'll be calling this page from Azure Functions.
        //And one of the best thing about using Azure Functions is that it's essentially free for simple cases like this!
        //Azure Functions uses a "consumption" based model, which means we only get charged when our function is running.
        //We get a free quota of 1 million executions a month, so we can go a long way for free.
        public async Task OnGetAsync()
        {
            _logger.LogInformation("Actions page called.");
            var log = new Log
            {
                Level = "INFORMATION",
                Message = "Actions page called.",
                TimeStamp = DateTime.Now
            };

            _appRepository.CreateLog(log);

            await _logsPusher.SendLogAsync($"Log Level: {log.Level} | Message: {log.Message} | Logged Time: {string.Format("{0:F}", log.TimeStamp)}");
        }
    }
}
