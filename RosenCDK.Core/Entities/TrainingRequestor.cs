using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RosenCDK.Entities
{
    [Table("TRAINING_REQUESTOR")]
    public class TrainingRequestor : Entity<int>
    {
        [Required]
        public virtual int PersonId { get; set; }
        [ForeignKey("PersonId")]
        public virtual Person Person { get; set; }
        [Required]
        public string DefaultDepartment { get; set; }
        [Required]
        public string JobFunctions { get; set; }
    }
}
