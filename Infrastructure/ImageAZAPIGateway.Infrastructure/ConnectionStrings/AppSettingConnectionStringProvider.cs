using EnsureThat;
using ImageAZAPIGateway.Infrastructure.Providers;
using Microsoft.Extensions.Configuration;

namespace ImageAZAPIGateway.Infrastructure.ConnectionStrings
{
    public class AppSettingConnectionStringProvider : IConnectionStringProvider
    {
        private readonly IConfiguration _configuration;
        public AppSettingConnectionStringProvider(IConfiguration configuration)
            => _configuration = configuration;

        public string Get(string name = "DefaultConnection")
        {
            var connString = _configuration.GetConnectionString(name);
            Ensure.That(connString, nameof(connString), opts => opts.WithMessage("Cannot find connection string")).IsNotEmptyOrWhiteSpace();

            return connString;
        }
    }
}
