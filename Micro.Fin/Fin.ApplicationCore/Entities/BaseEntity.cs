using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Fin.ApplicationCore.Entities
{
    public class BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(50)]
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        [MaxLength(50)]
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        [Timestamp]
        public byte[] Timestamp { get; set; }
    }
}
