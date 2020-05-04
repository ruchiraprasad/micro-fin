using Fin.ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fin.Infrastructure.Data.Configuration
{
    public class LoanConfig : IEntityTypeConfiguration<Loan>
    {
        public void Configure(EntityTypeBuilder<Loan> builder)
        {
            builder.HasOne(p => p.Customer)
                .WithMany(p=> p.Loans)
                .HasForeignKey(p => p.CustomerId);
        }
    }
}
