using MediatR;

namespace ImageAZAPIGateway.Application.Common.Seedwork
{
    public interface IQuery<out T> : IRequest<T>
    {
    }

}
