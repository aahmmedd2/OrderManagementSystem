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
    public class OrderConfigurations : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");

            builder.HasKey(O => O.Id);
            
            builder.Property(O => O.Id).IsRequired();

            builder.Property(O => O.CustomerId).IsRequired();

            builder.HasOne(O => O.Customer)
                   .WithMany(C => C.Orders)
                   .HasForeignKey(O => O.CustomerId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Property(O => O.Status)
                   .HasConversion<string>()
                   .IsRequired();

            builder.Property(O => O.PaymentMethod)
                   .HasConversion<string>()
                   .IsRequired();

            builder.Property(O => O.OrderDate)
                   .HasColumnType("datetime")
                   .IsRequired();

            builder.Property(O => O.TotalAmount)
                   .HasColumnType("decimal(8,2)")
                   .IsRequired();
        }
    }
}
