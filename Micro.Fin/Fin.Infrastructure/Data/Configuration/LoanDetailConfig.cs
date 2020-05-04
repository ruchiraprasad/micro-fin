using Fin.ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fin.Infrastructure.Data.Configuration
{
    public class LoanDetailConfig : IEntityTypeConfiguration<LoanDetail>
    {
        public void Configure(EntityTypeBuilder<LoanDetail> builder)
        {
            builder.HasOne(p => p.Loan)
                .WithMany(p => p.LoanDetails)
                .HasForeignKey(p => p.LoanId);
        }
    }
}
