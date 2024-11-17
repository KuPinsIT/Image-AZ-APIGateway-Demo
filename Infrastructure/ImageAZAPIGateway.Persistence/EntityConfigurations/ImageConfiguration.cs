using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ImageAZAPIGateway.Domain.Entities.ImageAggregate;

namespace ImageAZAPIGateway.Persistence.EntityConfigurations
{
    class ImageConfiguration : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            // table
            builder.ToTable(nameof(Image), ApplicationDbContext.SCHEMA);
            builder.ConfigureByConvention();
            builder.HasIndex(nameof(Image.Url)).IsUnique();

            // props
            builder.Property(x => x.Url).IsRequired();
            builder.Property(x => x.Description).IsRequired();
        }
    }
}
