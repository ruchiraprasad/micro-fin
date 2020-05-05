using System;
using System.Collections.Generic;
using System.Text;

namespace Fin.ApplicationCore.Entities
{
    public class Customer: BaseEntity
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Comment { get; set; }
        public virtual ICollection<Loan> Loans { get; set; }

        public string DisplayText { 
            get
            {
                return Name + " - " + Phone;
            }
        }
    }
}
