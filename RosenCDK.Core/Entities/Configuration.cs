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
    [Table("CONFIGURATION")]
    public class Configuration : Entity<int>
    {
      
        [Required]
        public string Name { get; set; }
        [Required]
        public string Value { get; set; }
    }
}
