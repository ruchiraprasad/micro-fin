using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Fin.ApplicationCore.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        [MaxLength(50)]
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
