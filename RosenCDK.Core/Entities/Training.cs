using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RosenCDK.Entities
{
    [Table("TRAINING")]
    public class Training : Entity<int>
    {
   
        [Required]
        public virtual int ProgramId { get; set; }
        [ForeignKey("ProgramId")]
        public virtual Program Programs { get; set; }


        [Required]
        public virtual int StatusId { get; set; }
        [ForeignKey("StatusId")]
        public virtual TrainingStatus TrainingStatus { get; set; }


        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public double TotalDuration { get; set; }
        [Required]
        [DefaultValue("")]
        public string AssignedTrainees { get; set; }
        [Required]
        [DefaultValue("")]
        public string AssignedTrainers { get; set; }
        [Required]
        public string ModuleArrangement { get; set; }

        [NotMapped]
        public int[] ArrayOfAssignedTrainees
        {
            get
            {
                return !string.IsNullOrEmpty(AssignedTrainees) ? Array.ConvertAll(AssignedTrainees.Split(','), int.Parse) : new int[0];
            }
            set
            {
                var _data = value;
                AssignedTrainees = String.Join(",", _data.Select(p => p.ToString()).ToArray());
            }
        }

        [NotMapped]
        public int[] ArrayOfAssignedTrainers
        {
            get
            {
                return !string.IsNullOrEmpty(AssignedTrainers) ? Array.ConvertAll(AssignedTrainers.Split(','), int.Parse) : new int[0];
            }
            set
            {
                var _data = value;
                AssignedTrainers = String.Join(",", _data.Select(p => p.ToString()).ToArray());
            }
        }
    }
}
