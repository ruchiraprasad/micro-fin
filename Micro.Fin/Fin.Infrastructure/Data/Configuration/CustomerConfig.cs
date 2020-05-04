using Fin.ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fin.Infrastructure.Data.Configuration
{
    public class CustomerConfig : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.Property(p => p.Name)
                .HasMaxLength(100);
            builder.Property(p => p.Phone)
                .HasMaxLength(30);
            builder.Property(p => p.Address)
                .HasMaxLength(100);
            builder.Property(p => p.Comment)
                .HasMaxLength(500);
        }
    }
}
