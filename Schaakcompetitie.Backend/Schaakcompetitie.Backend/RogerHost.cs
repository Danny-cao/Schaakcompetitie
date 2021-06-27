using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Roger.MicroServices.Host;

namespace Schaakcompetitie.Backend
{
    [ExcludeFromCodeCoverage]
    public class RogerHost : IHostedService
    {
        private readonly IMicroserviceHost _microserviceHost;

        public RogerHost(IMicroserviceHost microserviceHost)
        {
            _microserviceHost = microserviceHost;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _microserviceHost.Start();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _microserviceHost.Dispose();
            return Task.CompletedTask;
        }
    }
}