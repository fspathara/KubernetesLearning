using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LearnHelm
{
    public class ApplicationLifetimeService : IHostedService
    {
        readonly ILogger _logger;
        readonly IHostApplicationLifetime _applicationLifetime;
        readonly IWebHostEnvironment _env;

        public ApplicationLifetimeService(
            ILogger<ApplicationLifetimeService> logger, 
            IHostApplicationLifetime applicationLifetime,
            IWebHostEnvironment env)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _applicationLifetime = applicationLifetime ?? throw new ArgumentNullException(nameof(applicationLifetime));
            _env = env ?? throw new ArgumentNullException(nameof(env));
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            if (!_env.IsDeployedInKubernetes()) return Task.CompletedTask;
            _applicationLifetime.ApplicationStopped.Register(() =>
            {
                _logger.LogInformation("SIGTERM received, waiting for ingress to switch configuration");
                Task.Run(async () => await Task.Delay(30_000)).GetAwaiter().GetResult();
                _logger.LogInformation("TERMINATING");
            });
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    }
}
