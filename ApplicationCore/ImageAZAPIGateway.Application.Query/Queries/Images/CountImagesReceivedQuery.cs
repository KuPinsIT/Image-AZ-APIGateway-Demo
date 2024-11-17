using FluentValidation;
using ImageAZAPIGateway.Application.Common.Seedwork;
using ImageAZAPIGateway.Application.Query.DataModel.Images;
using ImageAZAPIGateway.Application.Query.Interfaces;
using ImageAZAPIGateway.Application.Query.Queries.Images.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ImageAZAPIGateway.Application.Query.Queries.Images
{
    public class CountImagesReceivedQuery : IQuery<ImageSummaryVm>
    {
        public DateTime ReceivedFromDate { get; set; }
        public DateTime? ReceivedToDate { get; set; } = DateTime.UtcNow;
        public class Handler(IApplicationQueryDbContext dbContext) : IRequestHandler<CountImagesReceivedQuery, ImageSummaryVm>
        {
            private readonly IApplicationQueryDbContext _dbContext = dbContext;

            public async Task<ImageSummaryVm> Handle(CountImagesReceivedQuery request, CancellationToken cancellationToken)
            {
                var query = _dbContext.Set<Image>()
                    .AsNoTracking();

                var totalImages = await query.OrderByDescending(img => img.CreatedDate)
                    .Where(img => img.CreatedDate >= request.ReceivedFromDate)
                    .Where(img => img.CreatedDate <= request.ReceivedToDate)
                    .CountAsync(cancellationToken);

                return new ImageSummaryVm(totalImages);
            }
        }

        public class Validator : AbstractValidator<CountImagesReceivedQuery>
        {
            public Validator()
            {
                RuleFor(cmd => cmd.ReceivedFromDate).NotNull();
            }
        }
    }
}
