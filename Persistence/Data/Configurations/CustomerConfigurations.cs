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
    internal class CustomerConfigurations : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customers");

            builder.HasKey(C => C.Id);

            builder.Property(C => C.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(C => C.Email)
                   .IsRequired();

            builder.HasMany(C => C.Orders)
                   .WithOne(O => O.Customer)
                   .HasForeignKey(O => O.CustomerId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
