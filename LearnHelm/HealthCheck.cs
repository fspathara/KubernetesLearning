using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LearnHelm
{
    public class HealthCheck : IHealthCheck
    {
        private static readonly Random _random = new Random();
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var randomNumber = _random.Next(10);
            var result = randomNumber != 4 ?
                    HealthCheckResult.Healthy() : HealthCheckResult.Unhealthy();
            return Task.FromResult(result);
        }
    }
}
