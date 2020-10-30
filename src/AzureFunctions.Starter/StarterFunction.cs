using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace AzureFunctions.Starter
{
    public class StarterFunction
    {
        private readonly ILogger<StarterFunction> _log;

        public StarterFunction(ILogger<StarterFunction> log)
        {
            this._log = log ?? throw new ArgumentNullException(nameof(log));

        }

        [FunctionName("StarterFunction")]
        public void Run([TimerTrigger("0 */5 * * * *", RunOnStartup = true)]TimerInfo myTimer)
        {
            _log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
        }
    }
}
