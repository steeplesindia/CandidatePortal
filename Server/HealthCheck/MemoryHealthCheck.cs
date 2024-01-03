﻿using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;

namespace CandidatePortal.Server.HealthCheck
{
    public class MemoryHealthCheck : IHealthCheck
    {
        private readonly IOptionsMonitor<MemoryCheckOptions> _options;
        public MemoryHealthCheck(IOptionsMonitor<MemoryCheckOptions> options)
        {
            _options = options;
        }

        public string Name => "memory_check";

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var options = _options.Get(context.Registration.Name);

            var allocated = GC.GetTotalMemory(forceFullCollection: false);
            var data = new Dictionary<string, object>()
            {
                { "AllocatedBytes", allocated},
                { "Get0Collections", GC.CollectionCount(0)},
                { "Get1Collections", GC.CollectionCount(1)},
                { "Get2Collections", GC.CollectionCount(2)},
            };

            var status = (allocated < options.Threshold) ? HealthStatus.Healthy : HealthStatus.Unhealthy;

            return Task.FromResult(new HealthCheckResult(status, 
                description: "Reports degraded status. if allocated bytes" + 
                $" >= {options.Threshold} bytes",
                exception:null,
                data:data
                ));
        }
    }

    public class MemoryCheckOptions {
        public string MemoryStatus { get; set; }

        public long Threshold { get; set; } = 1024L * 1024L * 1024L;
    }
}
