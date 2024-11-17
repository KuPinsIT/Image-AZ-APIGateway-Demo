using MediatR;

namespace ImageAZAPIGateway.Application.Common.Seedwork
{
    public interface ICommand<out T> : IRequest<T>
    {
    }
}
