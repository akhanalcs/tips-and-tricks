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
        //private readonly IAppRepository _appRepository;

        public ServiceCallResultModel(ILogger<ServiceCallResultModel> logger)
        {
            //_appRepository = appRepository;
            _logger = logger;
        }

        public JsonResult OnGet()
        {
            var serviceCallResult = new ServiceCalResult() { Log = new List<string> { "Log 1", "Log 2" }, Success = true };
            return new JsonResult(serviceCallResult);
        }
    }
}
