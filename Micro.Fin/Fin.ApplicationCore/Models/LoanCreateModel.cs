using System;
using System.Collections.Generic;
using System.Text;

namespace Fin.ApplicationCore.Models
{
    public class LoanCreateModel
    {
        public int? Id { get; set; }
        public int CustomerId { get; set; }
        public decimal InitialLoanAmount { get; set; }
        public DateTime DateGranted { get; set; }
        public int PeriodMonths { get; set; }
        public decimal Interest { get; set; }
        public string Security { get; set; }
        public decimal? PropertyValue { get; set; }
        public decimal CapitalOutstanding { get; set; }
    }
}
