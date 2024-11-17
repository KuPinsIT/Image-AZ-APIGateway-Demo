using Autofac;
using ImageAZAPIGateway.Application.Query.Interfaces;
using System.Reflection;

namespace ImageAZAPIGateway.Query
{
    public static class DependencyInjection
    {
        public static void Register(ContainerBuilder container)
        {
            // DbContext
            container.RegisterType<ApplicationQueryDbContext>().As<IApplicationQueryDbContext>().InstancePerLifetimeScope();

            container.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}
