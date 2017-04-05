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
    [Table("COMPETENCE")]
    public class Competence : Entity<int>
    {
       
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
