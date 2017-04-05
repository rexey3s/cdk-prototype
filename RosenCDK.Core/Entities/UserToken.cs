using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RosenCDK.Entities.Attributes;

namespace RosenCDK.Entities
{

    [Table("USER_TOKEN")]
    public class UserToken : Entity<int>
    {

        [Required]
        [Unique]
        public string Username { get; set; }
        [Required]
        public string AuthToken { get; set; }
        public DateTime? IssuedOn { get; set; }

        public DateTime? ExpiresOn { get; set; }

    }
}
