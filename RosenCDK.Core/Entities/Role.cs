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
    [Table("ROLE")]
    public class Role : Entity<int>
    {   
        
        [Required]
        public string RoleName { get; set; }

        //public virtual ICollection<Person> People { get; set; }
        public virtual ICollection<RoleDistribution> RoleDistributions { get; set; }

    }
}
