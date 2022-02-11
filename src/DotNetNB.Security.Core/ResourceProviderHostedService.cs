using DotNetNB.Security.Core.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DotNetNB.Security.Core
{
    public class ResourceProviderHostedService : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;

        public ResourceProviderHostedService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();
            var providers = scope.ServiceProvider.GetServices<IResourceProvider>();
            var resourceManager = scope.ServiceProvider.GetService<IResourceManager>();

            var resources = new List<Resource>();
            foreach (var provider in providers)
            {
                resources.AddRange(await provider.ExecuteAsync());
            }

            await resourceManager.CreateAsync(resources);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
        }
    }
}