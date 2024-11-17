using Autofac;
using ImageAZAPIGateway.Application.Common.Behaviors;
using ImageAZAPIGateway.Application.Common.Shared;
using MediatR;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace ImageAZAPIGateway.Application.Common
{
    public static class DependencyInjection
    {
        public static void Register(ContainerBuilder container, IConfiguration configuration)
        {
            // Register Mediator behaviors
            container.RegisterGeneric(typeof(ValidationBehavior<,>)).AsImplementedInterfaces().InstancePerDependency();

            // Application Context
            container.RegisterType<ApplicationContext>().AsSelf().InstancePerLifetimeScope();

            // CommandHandlers
            container.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .AsClosedTypesOf(typeof(IRequestHandler<,>))
                .InstancePerDependency();
        }
    }
}
