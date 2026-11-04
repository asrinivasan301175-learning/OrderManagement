using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderManagement.Domain.Entities;

namespace OrderManagement.Infrastructure.Persistence.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.CustomerName)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(x => x.OrderDate)
            .IsRequired();

        builder.Property(x => x.Status)
            .HasConversion<string>()
            .IsRequired();

        builder.Ignore(x => x.TotalAmount);

        builder.HasMany(x => x.Items)
            .WithOne()
            .HasForeignKey("OrderId")
            .OnDelete(DeleteBehavior.Cascade);

        builder.Navigation(x => x.Items)
            .HasField("_items")
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}