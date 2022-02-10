using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Pages
{
    public class ActionsModel : PageModel
    {
        private readonly ILogger<ActionsModel> _logger;

        public ActionsModel(ILogger<ActionsModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            _logger.LogInformation("Actions page called.");
        }
    }
}
