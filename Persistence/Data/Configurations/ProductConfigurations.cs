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
    public class ProductConfigurations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(P => P.Id);

            builder.Property(P => P.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(P => P.Price)
                   .IsRequired()
                   .HasColumnType("decimal(8,2)");

            builder.Property(P => P.Stock)
                   .IsRequired();

            builder.HasMany(P => P.OrderItems)
                   .WithOne(OI => OI.Product)
                   .HasForeignKey(OI => OI.ProductId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
