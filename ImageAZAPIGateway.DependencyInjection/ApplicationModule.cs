using Autofac;
using Microsoft.Extensions.Configuration;

namespace ImageAZAPIGateway.DependencyInjection
{
    public class ApplicationModule : Module
    {
        private readonly IConfiguration _configuration;
        public ApplicationModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            Application.Common.DependencyInjection.Register(builder, _configuration);
            Application.Command.DependencyInjection.Register(builder);
            Application.Query.DependencyInjection.Register(builder);
            Query.DependencyInjection.Register(builder);
            Persistence.DependencyInjection.Register(builder);
        }
    }
}
