using FluentValidation;
using ImageAZAPIGateway.Application.Command.Features.Images.Specifications;
using ImageAZAPIGateway.Application.Common.Seedwork;
using ImageAZAPIGateway.Domain.Entities.ImageAggregate;
using ImageAZAPIGateway.Domain.Shared.Exceptions;
using ImageAZAPIGateway.Domain.Shared.Utilities;
using MediatR;

namespace ImageAZAPIGateway.Application.Command.Features.Images.Commands
{
    public class AddImageCommand : ICommand<Guid>
    {
        public required string ImageUrl { get; set; }
        public required string Description { get; set; }
        public class Handler(IImageRepository imageRepository) : IRequestHandler<AddImageCommand, Guid>
        {
            private readonly IImageRepository _imageRepository = imageRepository;

            public async Task<Guid> Handle(AddImageCommand request, CancellationToken cancellationToken)
            {
                if(!request.ImageUrl.IsValidImageUrl())
                {
                    throw new ValidationException($"Image url '{request.ImageUrl}' is invalid");
                }

                var existedImage = await _imageRepository
                    .FirstOrDefaultAsync(new ImageByUrlSpec(request.ImageUrl), cancellationToken);
                if (existedImage != null)
                {
                    throw new DuplicatedEntryException($"Image url {request.ImageUrl}");
                }
                var image = new Image(request.ImageUrl, request.Description);
                _imageRepository.Add(image);
                await _imageRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
                return image.Id;
            }
        }

        public class Validator : AbstractValidator<AddImageCommand>
        {
            public Validator()
            {
                RuleFor(cmd => cmd.ImageUrl).NotNull().NotEmpty();
                RuleFor(cmd => cmd.Description).NotNull().NotEmpty();
            }
        }
    }
}
