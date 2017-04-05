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
    [Table("TRAINEE")]
    public class Trainee : Entity<int>
    {
 
        [Required]
        public virtual int PersonId { get; set; }

        [ForeignKey("PersonId")]
        public virtual Person Person { get; set; }

        [Required]
        public string DefaultDepartment { get; set; }

        [DefaultValue("")]
        public string JobFunctions { get; set; }
        [DefaultValue("")]
        public string Competencies { get; set; }
        [DefaultValue("")]
        public string TargetedTrainings { get; set; }
        [DefaultValue("")]
        public string AttendedTrainings { get; set; }

        [NotMapped]
        public int[] ArrayOfCompetence
        {
            get
            {         
                return !string.IsNullOrEmpty(Competencies) ? Array.ConvertAll(Competencies.Split(','), int.Parse) : new int[0];
            }
            set
            {
                var _data = value;
                Competencies = String.Join(",", _data.Select(p => p.ToString()).ToArray());
            }
        }

        [NotMapped]
        public int[] ArrayOfJobFunction
        {
            get
            {
                return !string.IsNullOrEmpty(JobFunctions) ? Array.ConvertAll(JobFunctions.Split(','), int.Parse) : new int[0];
            }
            set
            {
                var _data = value;
                JobFunctions = String.Join(",", _data.Select(p => p.ToString()).ToArray());
            }
        }
        [NotMapped]
        public int[] ArrayOfTargetedTraining
        {
            get
            {
                return !string.IsNullOrEmpty(TargetedTrainings) ? Array.ConvertAll(TargetedTrainings.Split(','), int.Parse) : new int[0];
            }
            set
            {
                var _data = value;
                TargetedTrainings = String.Join(",", _data.Select(p => p.ToString()).ToArray());
            }
        }

        [NotMapped]
        public int[] ArrayOfAttendedTraining
        {
            get
            {
                return !string.IsNullOrEmpty(AttendedTrainings)? Array.ConvertAll(AttendedTrainings.Split(','), int.Parse) : new int[0];
            }
            set
            {
                var _data = value;
                AttendedTrainings = String.Join(",", _data.Select(p => p.ToString()).ToArray());
            }
        }
    }
}
