using FluentValidation.Results;
using FluentValidation;
using MediatR;

namespace ImageAZAPIGateway.Application.Common.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
    {
        private readonly IValidator<TRequest>[] _validators;

        public ValidationBehavior(IValidator<TRequest>[] validators) => _validators = validators;

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var failures = new List<ValidationFailure>();
            foreach (var validator in _validators)
            {
                var result = await validator.ValidateAsync(request, cancellationToken);
                if (result.Errors is { Count: > 0 })
                {
                    failures.AddRange(result.Errors);
                }
            }

            if (failures.Any())
            {
                throw new ValidationException(failures);
            }

            var response = await next();
            return response;
        }
    }
}
