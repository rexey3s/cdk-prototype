using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using RosenCDK.Entities.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RosenCDK.Entities
{
    [Table("PERSON")]
    public class Person : Entity<int>
    {
       
   
        [Required]
        public string Name { get; set; }

        public string Company { get; set; }

        [Required]
        [Unique]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public virtual int? RoleId { get; set; }

        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }


        public DateTime CreationTime { get; set; }
     
        public Person()
        {
            CreationTime = DateTime.Now;
        }



    }
}
