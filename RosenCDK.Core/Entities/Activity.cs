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
    [Table("ACTIVITY")]
    public class Activity : Entity<int>
    {
        
        public Activity()
        {

        }
        public Activity(string actName)
        {
            ActivityName = actName;
        }
        [Required]
        public string ActivityName { get; set; }
        public virtual ICollection<RoleDistribution> RoleDistributions { get; set; }

    }
}
