using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using WebApp.Interfaces;
using WebApp.Models;

namespace WebApp.Scheduling.Tasks
{
    public class QuoteOfTheDayTask : IScheduledTask
    {
        public string Schedule => "* * * * *"; // Scheduled at every minute

        public async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            var httpClient = new HttpClient();

            var quoteJson = JObject.Parse(await httpClient.GetStringAsync("http://quotes.rest/qod.json"));

            QuoteOfTheDay.Current = JsonConvert.DeserializeObject<QuoteOfTheDay>(quoteJson["contents"]["quotes"][0].ToString());
            QuoteOfTheDay.Current.FetchedTime = DateTime.Now;
        }
    }
}
