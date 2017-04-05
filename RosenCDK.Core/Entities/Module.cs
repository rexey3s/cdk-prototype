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
    [Table("MODULE")]
    public class Module : Entity<int>
    {
    
        [Required]
        [DefaultValue("")]
        public string CompetenciesTrained { get; set; }
        [Required]
        public string AreaOfObjective { get; set; }
        [Required]
        public virtual int TypeId { get; set; }
        [ForeignKey("TypeId")]
        public virtual ModuleType ModuleType { get; set; }

        [Required]
        public string Title { get; set; }
        [Required]
        public string Objectives { get; set; }
        [Required]
        public string TopicsCovered { get; set; }
        [Required]
        public string Exercises { get; set; }
        [Required]
        public double Theory { get; set; }
        [Required]
        public double Pratical { get; set; }
        public string Methods { get; set; }
        public string ReferencesDoc { get; set; }
        [Required]
        public bool ExamInclude { get; set; }
        public string RoomOrEquipment { get; set; }
        public string LearningTransfer { get; set; }

        public DateTime? ExpirationDate { get; set; }
        [Required]
        public string TargetGroup { get; set; }

        [Required]
        public virtual int PersonId { get; set; }
        [ForeignKey("PersonId")]
        public virtual Person Person { get; set; }

        [NotMapped]
        public int[] ArrayOfTrainingCompetencies
        {
            get
            {
                return !string.IsNullOrEmpty(CompetenciesTrained) ? Array.ConvertAll(CompetenciesTrained.Split(','), int.Parse) : new int[0];
            }
            set
            {
                var _data = value;
                CompetenciesTrained = String.Join(",", _data.Select(p => p.ToString()).ToArray());
            }
        }

        [NotMapped]
        public double ModuleDuration {
            get
            {
                return  (Theory + Pratical);
            }
        }
    }
}
