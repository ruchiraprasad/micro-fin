using System;
using System.Collections.Generic;
using System.Text;

namespace Fin.ApplicationCore.Entities
{
    public class Loan : BaseEntity
    {
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public decimal InitialLoanAmount { get; set; }
        public DateTime DateGranted { get; set; }
        public int PeriodMonths { get; set; }
        public int Interest { get; set; }
        public string Security { get; set; }
        public decimal PropertyValue { get; set; }
        public decimal CapitalOutstanding { get; set; }
        public virtual ICollection<LoanDetail> LoanDetails { get; set; }
    }
}
