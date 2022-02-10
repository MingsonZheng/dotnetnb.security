using Microsoft.Extensions.Hosting;

namespace DotNetNB.Security.Core
{
    public class ResourceProviderHostedService : IHostedService
    {
        private readonly IServiceProvider[] _serviceProviders;

        public ResourceProviderHostedService(IServiceProvider[] serviceProviders)
        {
            _serviceProviders = serviceProviders;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {

        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {

        }
    } 
}