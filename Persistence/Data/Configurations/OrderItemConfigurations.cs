using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations
{
    public class OrderItemConfigurations : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("OrderItems");

            builder.HasKey(OI => OI.Id);

            builder.Property(OI => OI.Id).IsRequired();

            builder.Property(OI => OI.Quantity).IsRequired();

            builder.Property(OI => OI.UnitPrice)
                   .HasColumnType("decimal(8,2)")
                   .IsRequired();

            builder.Property(OI => OI.Discount)
                   .HasColumnType("decimal(8,2)")
                   .IsRequired();

            builder.HasOne(OI => OI.Order)
                   .WithMany(O => O.OrderItems)
                   .HasForeignKey(OI => OI.OrderId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(OI => OI.Product)
                   .WithMany()
                   .HasForeignKey(OI => OI.ProductId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
