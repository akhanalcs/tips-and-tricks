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
            try
            {
                var httpClient = new HttpClient();

                //var quoteJson = JObject.Parse(await httpClient.GetStringAsync("http://quotes.rest/qod.json"));
                // QuoteOfTheDay.Current = JsonConvert.DeserializeObject<QuoteOfTheDay>(quoteJson["contents"]["quotes"][0].ToString());

                QuoteOfTheDay.Current.Quote = $"Some fancy quote fetched from some API.";
                QuoteOfTheDay.Current.FetchedTime = DateTime.Now;
            }
            catch (Exception ex)
            {
                QuoteOfTheDay.Current.Quote = $"Something went wrong while calling the Quotes API. The exception message is: {ex.Message}.";
                QuoteOfTheDay.Current.FetchedTime = DateTime.Now;
            }
        }
    }
}
