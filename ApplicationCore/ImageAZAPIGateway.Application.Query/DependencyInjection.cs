using Autofac;
using AutoMapper.Contrib.Autofac.DependencyInjection;
using MediatR;
using System.Reflection;

namespace ImageAZAPIGateway.Application.Query
{
    public static class DependencyInjection
    {
        public static void Register(ContainerBuilder container)
        {
            // AutoMapper
            container.RegisterAutoMapper(Assembly.GetExecutingAssembly());

            // CommandHandlers
            container.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .AsClosedTypesOf(typeof(IRequestHandler<,>))
                .InstancePerDependency();
        }
    }
}
