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
    public class SchedulingModel : PageModel
    {
        private readonly ILogger<SchedulingModel> _logger;

        public SchedulingModel(ILogger<SchedulingModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            ViewData["Quote"] = QuoteOfTheDay.Current.Quote;
            ViewData["FetchedTime"] = QuoteOfTheDay.Current.FetchedTime;
        }
    }
}
