using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Enums;

namespace Ordering.Infrastructure.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(orderId => orderId.Value, dbId => OrderId.Of(dbId));

        builder.HasOne<Customer>()
            .WithMany()
            .HasForeignKey(x => x.CustomerId)
            .IsRequired();

        builder.HasMany(x => x.OrderItems)
            .WithOne()
            .HasForeignKey(x => x.OrderId);

        builder.ComplexProperty(
            x => x.OrderName, nameBuilder =>
            {
                nameBuilder.Property(n => n.Value)
                    .HasColumnName(nameof(Order.OrderName))
                    .HasMaxLength(100)
                    .IsRequired();
            });

        builder.ComplexProperty(x => x.ShippingAddress, addressBuilder =>
        {
            addressBuilder.Property(a => a.FirstName)
                .HasMaxLength(50)
                .IsRequired();

            addressBuilder.Property(a => a.LastName)
                .HasMaxLength(50)
                .IsRequired();

            addressBuilder.Property(a => a.EmailAddress)
                .HasMaxLength(50);

            addressBuilder.Property(a => a.AddressLine)
                .HasMaxLength(180)
                .IsRequired();

            addressBuilder.Property(a => a.Country)
                .HasMaxLength(50);

            addressBuilder.Property(a => a.State)
                .HasMaxLength(50);

            addressBuilder.Property(a => a.ZipCode)
                .HasMaxLength(5)
                .IsRequired();
        });

        builder.ComplexProperty(x => x.BillingAddress, addressBuilder =>
        {
            addressBuilder.Property(a => a.FirstName)
                .HasMaxLength(50)
                .IsRequired();

            addressBuilder.Property(a => a.LastName)
                .HasMaxLength(50)
                .IsRequired();

            addressBuilder.Property(a => a.EmailAddress)
                .HasMaxLength(50);

            addressBuilder.Property(a => a.AddressLine)
                .HasMaxLength(180)
                .IsRequired();

            addressBuilder.Property(a => a.Country)
                .HasMaxLength(50);

            addressBuilder.Property(a => a.State)
                .HasMaxLength(50);

            addressBuilder.Property(a => a.ZipCode)
                .HasMaxLength(5)
                .IsRequired();
        });

        builder.ComplexProperty(x => x.Payment, paymentBuilder =>
        {
            paymentBuilder.Property(a => a.CardName)
                .HasMaxLength(50);

            paymentBuilder.Property(a => a.CardNumber)
                .HasMaxLength(24)
                .IsRequired();

            paymentBuilder.Property(a => a.Expiration)
                .HasMaxLength(10);

            paymentBuilder.Property(a => a.CVV)
                .HasMaxLength(3);

            paymentBuilder.Property(a => a.PaymentMethod);
        });

        builder.Property(o => o.Status)
            .HasDefaultValue(OrderStatus.Draft)
            .HasConversion(
                s => s.ToString(), 
                dbs => (OrderStatus)Enum.Parse(typeof(OrderStatus), dbs));

        builder.Property(x => x.TotalPrice);
    }
}
