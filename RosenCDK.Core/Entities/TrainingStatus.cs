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
    [Table("TRAINING_STATUS")]
    public class TrainingStatus : Entity<int>
    {
        public const string PLANNED = "Planned";
        public const string ONGOING = "Ongoing";
        public const string COMPLETED = "Completed";
        public const string CANCELED = "Canceled";

        [Required]
        public string StatusName { get; set; }
        //public virtual IEnumerable<Training> Trainings { get; set; }
    }
}
