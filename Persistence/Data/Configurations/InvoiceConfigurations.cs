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
    internal class InvoiceConfigurations : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.ToTable("Invoices");

            builder.HasKey(I => I.Id);

            builder.Property(I => I.InvoiceDate)
                   .IsRequired();

            builder.Property(I => I.TotalAmount)
                   .HasColumnType("decimal(10,2)")
                   .IsRequired();

            builder.HasOne(I => I.Order)
                   .WithMany(O => O.Invoices)
                   .HasForeignKey(I => I.OrderId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
