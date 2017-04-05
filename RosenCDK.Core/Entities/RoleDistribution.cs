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
    [Table("ROLEDISTRIBUTION")]
    public class RoleDistribution : Entity<int>
    {
        public RoleDistribution()
        {

        }

        public RoleDistribution(int roleId, int actId)
        {
            RoleId = roleId;
            ActivityId = actId;
        }
   
        [Required]
        public int RoleId { get; set; }
        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }

        [Required]
        public int ActivityId { get; set; }
        [ForeignKey("ActivityId")]
        public virtual Activity Activity { get; set; }
    }
}
