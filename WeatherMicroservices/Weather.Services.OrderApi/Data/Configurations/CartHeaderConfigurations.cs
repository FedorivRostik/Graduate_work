using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Weather.Services.CartApi.Models;
using Weather.Services.CartApi.Utilities.Constants;

namespace Weather.Services.CartApi.Data.Configurations;

public class CartHeaderConfigurations : IEntityTypeConfiguration<CartHeader>
{
    public void Configure(EntityTypeBuilder<CartHeader> builder)
    {
        builder.Property(c => c.Status)
            .HasDefaultValue(CartStatuses.StatusOpen);
    }
}
