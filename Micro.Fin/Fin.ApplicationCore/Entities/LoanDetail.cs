using Fin.ApplicationCore.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fin.ApplicationCore.Entities
{
    public class LoanDetail : BaseEntity
    {
        public int LoanId { get; set; }
        public virtual Loan Loan { get; set; }
        public int Installment { get; set; }
        public DateTime Month { get; set; }
        public decimal MonthlyInterest { get; set; }
        public decimal Paid { get; set; }
        public decimal LatePaid { get; set; }
        public DateTime PaidDate { get; set; }
        public decimal TotalInterest { get; set; }
        public decimal Balance { get; set; }
        public InterestType InterestType { get; set; }
    }
}
