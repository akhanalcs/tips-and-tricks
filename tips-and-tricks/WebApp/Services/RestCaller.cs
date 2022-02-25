using Microsoft.Extensions.Logging;
using Polly;
using Polly.Timeout;
using Polly.Wrap;
using RestSharp;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace WebApp.Services
{
    public class RestCaller
    {
        private readonly ILogger<RestCaller> _logger;
        private readonly RestClient _restClient;

        private const int retryCountOnTimeout = 2; //So 2 + 1 initial = 3 attempts will be made
        private const int retryCountOnBadResult = 2; //So 2 + 1 initial = 3 attempts will be made
        private const int timeoutInSecondsForEachTry = 15;

        public RestCaller(string baseUrl, ILogger<RestCaller> logger)
        {
            _logger = logger;

            var options = new RestClientOptions(baseUrl)
            {
                //ThrowOnAnyError = true,
                //Timeout = 1, // 1 ms timeout for testing purposes
                //Proxy = GetWebProxy()
            };

            _restClient = new RestClient(options);
        }

        public async Task<(T, string)> SendRequestAndGetDataAsync<T>(RestRequest request, string callingProcessName)
        {
            // Request Logging Part. Maybe use callingProcessName
            string requestForLogging = GetRequestForLogging(request);
            _logger.LogInformation(requestForLogging);

            // Response Part:
            var response = await ExecuteAsyncWithPolicy<T>(request, callingProcessName);

            // Response Logging Part:
            string responseForLogging = response.Content;
            _logger.LogInformation(responseForLogging);

            //Also capture RequestResponseGUID from the logging to track this request/ response in the future.

            if (response.ErrorException != null)
            {
                var message = $"An error occured during this request. The RequestResponseGUID is: blah-blah. Check inner exception and request response entries for more details.";
                var newException = new Exception(message, response.ErrorException);
                throw newException;
            }
            else
            {
                return (response.Data, "RequestResponseGUID");
            }
        }

        private static WebProxy GetWebProxy()
        {
            var proxyUrl = "http://proxy-name.companydomain.com:9090/";
            // First create a proxy object
            var proxy = new WebProxy()
            {
                Address = new Uri(proxyUrl),
                BypassProxyOnLocal = false,
                //UseDefaultCredentials = true, // This uses: Credentials = CredentialCache.DefaultCredentials
                //*** These creds are given to the proxy server, not the web server ***
                Credentials = CredentialCache.DefaultNetworkCredentials
                //Credentials = new NetworkCredential("proxyUserName", "proxyPassword")
            };

            return proxy;
        }

        private async Task<RestResponse<T>> ExecuteAsyncWithPolicy<T>(RestRequest request, string callingProcessName, CancellationToken cancellationToken = default(CancellationToken))
        {
            var finalPolicy = GetFaultHandlingPolicy<T>();
            var policyResult = await finalPolicy
                                    .ExecuteAndCaptureAsync(
                                        (context, ct) => _restClient.ExecuteAsync<T>(request, ct),
                                        contextData: new Dictionary<string, object>
                                        {
                                            { "CallingProcess", callingProcessName }
                                        },
                                        cancellationToken
                                    );

            if (policyResult.Outcome == OutcomeType.Successful)
            {
                return policyResult.Result;
            }
            else if (policyResult.FinalException != null)
            {
                return new RestResponse<T>
                {
                    Request = request,
                    ErrorException = policyResult.FinalException
                };
            }
            else
            {
                return new RestResponse<T>
                {
                    Request = request,
                    ErrorException = new Exception(policyResult.FinalHandledResult?.ErrorMessage)
                };
            }
        }

        private static AsyncPolicyWrap<RestResponse<T>> GetFaultHandlingPolicy<T>()
        {
            var timeoutPolicy = Policy.TimeoutAsync<RestResponse<T>>(timeoutInSecondsForEachTry);

            var timeoutRetryPolicy = Policy<RestResponse<T>>
                                        .Handle<TimeoutRejectedException>() // thrown by Polly's TimeoutPolicy if the inner call times out
                                        .WaitAndRetryAsync(
                                            retryCount: retryCountOnTimeout, // We can also do this with WaitAndRetryForever.
                                            sleepDurationProvider: attempt => TimeSpan.FromSeconds(0.25 * Math.Pow(2, attempt)), // Back off!  2, 4, 8 etc times 1/4-second = 0.5, 1, 2 seconds
                                            onRetryAsync: RetryDelegateAsync
                                        );

            var restResponsePolicy = Policy
                                        .HandleResult<RestResponse<T>>(result => result.ResponseStatus != ResponseStatus.Completed)
                                        //.HandleResult<IRestResponse>(result => result.StatusCode != System.Net.HttpStatusCode.OK) // This DOESN'T capture Transient faults.
                                        .WaitAndRetryAsync(
                                        retryCount: retryCountOnBadResult,
                                        sleepDurationProvider: attempt => TimeSpan.FromSeconds(0.25 * Math.Pow(2, attempt)), // Back off!  2, 4, 8 etc times 1/4-second = 0.5, 1, 2 seconds
                                        onRetryAsync: RetryDelegateAsync
                                        );

            // The goal is to place the timeoutPolicy inside the resultPolicy, to make it time out each try.
            var finalPolicy = Policy.WrapAsync(restResponsePolicy, timeoutRetryPolicy, timeoutPolicy); //timeoutPolicy is innermost and responseResultPolicy is outermost.

            return finalPolicy;
        }

        // AshishK Notes:
        // This will be called whenever the policy should be triggered but before the wait between attempts.
        // If using Action, the sig would look like this:
        // public static async void RetryDelegate<T>(DelegateResult<T> exception, TimeSpan calculatedWaitDuration, int retryCount, Context context)
        private static async Task RetryDelegateAsync<T>(DelegateResult<T> result, TimeSpan calculatedWaitDuration, int retryCount, Context context)
        {
            // This is our new exception handler! 
            var callingProcess = context["CallingProcess"].ToString(); // Might be useful for logging
            var msg = $"The control has fallen into Polly's retry method from {callingProcess} method. This is retry attempt: {retryCount}.\n"; // msg is useful for logging
            if (result is TimeoutRejectedException) msg += $"Operation failed after a timeout.\n";

            if (result.Exception != null)
            {
                msg += $"Handled exception triggered this. Exception message is: {result.Exception.Message}.\n";
            }

            if (result.Result != null)
            {
                msg += "Handled exception didn't trigger this but defined policies did. It relates to HandleResult you have defined in your policy. Check error log to find more details about it.\n";
            }

            msg += $"A retry will be made after waiting {calculatedWaitDuration.TotalMilliseconds} milliseconds.";

            // Log 'msg' only if you want to.
            await Task.CompletedTask;
        }

        private string GetRequestForLogging(RestRequest request)
        {
            var requestToLog = new
            {
                // This will generate the actual Uri used in the request
                RequestUri = _restClient.BuildUri(request),
                // Parameters are custom anonymous objects in order to have the parameter type as a nice string
                // otherwise it will just show the enum value
                parameters = request.Parameters.Select(parameter => new
                {
                    name = parameter.Name,
                    value = parameter.Value,
                    type = parameter.Type.ToString()
                }),
                // ToString() here to have the method as a nice string otherwise it will just show the enum value
                method = request.Method.ToString()
            };

            return JsonSerializer.Serialize(requestToLog);
        }
    }
}
