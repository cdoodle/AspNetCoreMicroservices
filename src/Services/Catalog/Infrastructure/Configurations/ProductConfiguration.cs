using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            //EF implicit
            //builder.HasKey(p => p.Id);
            builder.Property(p => p.Price).HasPrecision(18, 2);
            builder.Property(product => product.Summary).HasMaxLength(500);
        }
    }
}
