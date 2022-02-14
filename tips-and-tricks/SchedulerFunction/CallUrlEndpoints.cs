using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace SchedulerFunction
{
    public static class CallUrlEndpoints
    {
        // For eg: "0 15 10 * * 2" = Fire every Tuesday at 10:15 am.
        // {second=0} {minute=15} {hour=10} {day} {month} {day-of-week=(2=Tuesday)}
        // "* * * * *" = Fire every minute
        private const string TimerSchedule = "* * * * *";
        private static HttpClient _client = new HttpClient();

        [FunctionName("CallUrlEndpoints")]
        public static async Task RunAsync([TimerTrigger(TimerSchedule)] TimerInfo myTimer, ILogger log)
        {
            try
            {
                log.LogInformation($"Calling ActionsUrl at: {DateTime.Now}");

                //"ActionsUrl": "https://localhost:5001/Actions" is in the local.settings.json
                var actionsUrl = Environment.GetEnvironmentVariable("ActionsUrl");

                await _client.GetAsync(actionsUrl);

                log.LogInformation($"Called ActionsUrl successfully at: {DateTime.Now}");
            }
            catch (Exception ex)
            {
                log.LogInformation($"Failed to call ActionsUrl. The error message is:{ex.Message}.");
            }
        }
    }
}
