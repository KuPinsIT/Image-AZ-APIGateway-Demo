using AutoMapper;
using AutoMapper.QueryableExtensions;
using ImageAZAPIGateway.Application.Common.Seedwork;
using ImageAZAPIGateway.Application.Query.DataModel.Images;
using ImageAZAPIGateway.Application.Query.Interfaces;
using ImageAZAPIGateway.Application.Query.Queries.Images.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ImageAZAPIGateway.Application.Query.Queries.Images
{
    public class GetLatestImageQuery : IQuery<ImageVm>
    {
        public class Handler(IApplicationQueryDbContext dbContext, IMapper mapper) : IRequestHandler<GetLatestImageQuery, ImageVm>
        {
            private readonly IApplicationQueryDbContext _dbContext = dbContext;
            private readonly IMapper _mapper = mapper;

            public async Task<ImageVm> Handle(GetLatestImageQuery request, CancellationToken cancellationToken)
            {
                var query = _dbContext.Set<Image>()
                    .AsNoTracking();

                var latestImage = await query.OrderByDescending(img => img.CreatedDate)
                    .ProjectTo<ImageVm>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(cancellationToken);

                return latestImage;
            }
        }
    }
}
