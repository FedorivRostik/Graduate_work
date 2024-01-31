using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Weather.Services.ProductApi.Models;

namespace Weather.Services.ProductApi.Data.Configurations;

public class GenreConfigurations : IEntityTypeConfiguration<Genre>
{
    public void Configure(EntityTypeBuilder<Genre> builder)
    {
        builder
             .HasKey(g => g.GenreId);

        builder
            .HasIndex(g => g.Name)
            .IsUnique();

        builder
            .Property(g => g.Name)
            .IsRequired();

        builder.HasMany(g => g.Products)
            .WithOne(g => g.Genre)
            .HasForeignKey(g => g.GenreId);

    }
}
