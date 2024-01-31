using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Weather.Services.ProductApi.Models;

namespace Weather.Services.ProductApi.Data.Configurations;

public class ProductConfigurations : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder
            .HasKey(p => p.ProductId);

        builder
            .HasIndex(p => p.Slug)
            .IsUnique();

        builder
            .ToTable(p => p.HasCheckConstraint("CK_Price_GreaterThanZero", "Price > 0"));

        builder
            .ToTable(p => p.HasCheckConstraint("CK_Amount_GreaterThanZero", "Amount > 0"));
        builder
           .ToTable(p => p.HasCheckConstraint("CK_Discount_GreaterThanZero", "Discount > 0"));

        builder
            .Property(p => p.Name)
            .IsRequired();

        builder
           .Property(p => p.Slug)
           .IsRequired();

        builder
           .Property(p => p.Amount)
           .IsRequired();

        builder
           .Property(p => p.Price)
           .IsRequired();

        builder
        .Property(p => p.Discount)
        .IsRequired(false);

        builder
          .Property(p => p.Description)
          .IsRequired(false);

        builder
         .Property(p => p.CouponCode)
         .IsRequired(false);

        builder
         .Property(p => p.ImageUrl)
         .IsRequired(false);
    }
}
