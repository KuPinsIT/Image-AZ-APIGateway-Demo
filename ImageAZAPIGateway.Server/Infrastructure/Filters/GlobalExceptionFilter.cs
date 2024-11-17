using ImageAZAPIGateway.Application.Common.Seedwork.Authorization;
using ImageAZAPIGateway.Domain.Shared.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Net;

namespace ImageAZAPIGateway.Server.Infrastructure.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<GlobalExceptionFilter> _logger;

        public GlobalExceptionFilter(IWebHostEnvironment env, ILogger<GlobalExceptionFilter> logger)
        {
            _env = env;
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            var ex = context.Exception.GetOriginalException();
            var exMessage = ex.Message;
            _logger.LogError(ex, exMessage);

            ProblemDetails problem;
            if (context.Exception.GetType() == typeof(NotFoundException))
            {
                problem = new ProblemDetails
                {
                    Status = (int)HttpStatusCode.NotFound,
                    Title = exMessage
                };
            }
            else if (context.Exception.GetType().IsAssignableTo(typeof(UnauthorizedException)))
            {
                problem = new ProblemDetails
                {
                    Status = (int)HttpStatusCode.Forbidden,
                    Title = exMessage
                };
            }
            else if (context.Exception.GetType() == typeof(ValidationException) 
                || context.Exception.GetType() == typeof(FluentValidation.ValidationException))
            {
                problem = new ProblemDetails
                {
                    Status = (int)HttpStatusCode.BadRequest,
                    Title = exMessage
                };
            }
            else if (context.Exception.GetType().IsAssignableTo(typeof(BusinessException)))
            {
                problem = new ProblemDetails
                {
                    Status = (int)HttpStatusCode.OK,
                    Title = exMessage
                };

                problem.Extensions.Add("errorCode", "BUSINESS_ERROR");
            }
            else
            {
                problem = new ProblemDetails
                {
                    Status = (int)HttpStatusCode.InternalServerError,
                    Title = "An error occurred. Please try again.",
                };

                if (_env.EnvironmentName.ToUpper() == "DEVELOPMENT")
                {
                    problem.Detail = ex.ToString();
                }
            }

            context.Result = new ObjectResult(problem);
            context.ExceptionHandled = true;
        }
    }
}
