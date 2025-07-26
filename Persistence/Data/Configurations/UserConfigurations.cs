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
    internal class UserConfigurations : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(U => U.Id);

            builder.Property(U => U.UserName)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.HasIndex(U => U.UserName)
                   .IsUnique();

            builder.Property(U => U.PasswordHash)
                   .IsRequired();

            builder.Property(U => U.Role)
                   .HasConversion<string>()
                   .IsRequired();
        }
    }
}
