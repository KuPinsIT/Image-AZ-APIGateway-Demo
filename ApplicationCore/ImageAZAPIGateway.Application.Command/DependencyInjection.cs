using Autofac;
using AutoMapper.Contrib.Autofac.DependencyInjection;
using FluentValidation;
using MediatR;
using System.Reflection;

namespace ImageAZAPIGateway.Application.Command
{
    public static class DependencyInjection
    {
        public static void Register(ContainerBuilder container)
        {
            // AutoMapper
            container.RegisterAutoMapper(Assembly.GetExecutingAssembly());

            // CommandHandlers
            container.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .AsClosedTypesOf(typeof(IRequestHandler<>))
                .InstancePerDependency();

            container.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .AsClosedTypesOf(typeof(IRequestHandler<,>))
                .InstancePerDependency();

            // Command's Validators
            container.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .AsClosedTypesOf(typeof(IValidator<>))
                .InstancePerDependency();

            // DomainEventHandlers
            container.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .AsClosedTypesOf(typeof(INotificationHandler<>))
                .InstancePerDependency();
        }
    }
}
