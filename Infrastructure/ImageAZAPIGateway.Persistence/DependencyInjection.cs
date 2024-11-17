using Autofac;
using ImageAZAPIGateway.Infrastructure.ConnectionStrings;
using ImageAZAPIGateway.Infrastructure.Providers;
using System.Reflection;

namespace ImageAZAPIGateway.Persistence
{
    public static class DependencyInjection
    {
        public static void Register(ContainerBuilder container)
        {
            // DbContext
            container.RegisterType<ApplicationDbContext>().InstancePerLifetimeScope();

            // Repositories
            container.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            container.RegisterType<AppSettingConnectionStringProvider>().As<IConnectionStringProvider>().SingleInstance();

        }
    }
}
