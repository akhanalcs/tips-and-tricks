using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Interfaces;
using WebApp.Models;

namespace WebApp.Pages.AJAXPages
{
    public class ServiceCallResultModel : PageModel
    {
        private readonly ILogger<ServiceCallResultModel> _logger;
        private readonly IAppRepository _appRepository;

        public ServiceCallResultModel(ILogger<ServiceCallResultModel> logger, IAppRepository appRepository)
        {
            _appRepository = appRepository;
            _logger = logger;
        }

        public JsonResult OnGet(string actionFlag)
        {
            List<string> logs = new List<string>();

            switch (actionFlag)
            {
                case "getActionsPageLogs":
                    logs = _appRepository.Logs
                            .Where(l => l.Message == "Actions page called.")
                            .Select(l => $"Log Level: {l.Level}, Message: {l.Message}, Logged Time: {string.Format("{0:F}", l.TimeStamp)}")
                            .ToList();
                    break;

                case "getSchedulingMethodLogs":
                    logs = _appRepository.Logs
                            .Where(l => l.Message == "Quote fetch method called.")
                            .Select(l => $"Log Level: {l.Level}, Message: {l.Message}, Logged Time: {string.Format("{0:F}", l.TimeStamp)}")
                            .ToList();
                    break;

                case "deleteAllLogs":
                    _appRepository.ClearLogs();
                    break;

                default:
                    break;
            }

            var serviceCallResult = new ServiceCalResult() { Log = logs, Success = true };
            return new JsonResult(serviceCallResult);
        }
    }
}
