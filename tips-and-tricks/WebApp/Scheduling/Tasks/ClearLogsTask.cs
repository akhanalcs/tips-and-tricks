using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApp.Interfaces;

namespace WebApp.Scheduling.Tasks
{
    public class ClearLogsTask : IScheduledTask
    {
        private readonly ILogger<ClearLogsTask> _logger;
        private readonly IAppRepository _appRepository;

        public ClearLogsTask(ILogger<ClearLogsTask> logger, IAppRepository appRepository)
        {
            _appRepository = appRepository;
            _logger = logger;
        }

        public string Schedule => "*/10 * * * *"; // Scheduled at every 10th minute.

        public async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            _appRepository.ClearLogs();
            await Task.CompletedTask;
        }
    }
}
