using Fin.ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fin.Infrastructure.Data.Configuration
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(p => p.FirstName)
                .HasMaxLength(100);
            builder.Property(p => p.LastName)
                .HasMaxLength(500);
            builder.Property(p => p.UserName)
                .HasMaxLength(100);
            builder.Property(p => p.Password)
                .HasMaxLength(100);
            builder.Property(p => p.Email)
                .HasMaxLength(100);
            builder.Property(p => p.Phone)
                .HasMaxLength(100);
        }
    }
}
