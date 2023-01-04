using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Pages
{
    public class ActionsModel : PageModel
    {
        private readonly ILogger<ActionsModel> _logger;

        public ActionsModel(ILogger<ActionsModel> logger, IAppRepository appRepository)
        {
            _logger = logger;
        }

        public async Task OnGetAsync()
        {
            // This should push the log into SQLite Database, but it doesn't :(
            _logger.LogInformation("Actions page called.");
        }
    }
}
