using System;
using System.Collections.Generic;
using System.Text;

namespace Fin.ApplicationCore.Models
{
    public class CustomerModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Comment { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
