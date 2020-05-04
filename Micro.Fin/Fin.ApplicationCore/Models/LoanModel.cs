using System;
using System.Collections.Generic;
using System.Text;

namespace Fin.ApplicationCore.Models
{
    public class LoanModel
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public decimal InitialLoanAmount { get; set; }
        public DateTime DateGranted { get; set; }
        public int PeriodMonths { get; set; }
        public decimal CapitalOutstanding { get; set; }
    }
}
