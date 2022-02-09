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
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            ViewData["Quote"] = QuoteOfTheDay.Current.Quote;
            ViewData["Author"] = QuoteOfTheDay.Current.Author;
            ViewData["FetchedTime"] = QuoteOfTheDay.Current.FetchedTime;
        }
    }
}
