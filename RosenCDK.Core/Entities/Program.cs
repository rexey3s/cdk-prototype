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
    [Table("PROGRAM")]
    public class Program : Entity<int>
    {
    
        [Required]
        public string ProgramTitle { get; set; }

        [Required]
        [DefaultValue("")]
        public string IncludedModules { get; set; }

        [DefaultValue("")]
        public string NeedByPotentialTrainees { get; set; }

        public virtual ICollection<Training> Trainings { get; set; }

        [NotMapped]
        public int[] ArrayOfIncludedModules
        {
            get
            {
                return !string.IsNullOrEmpty(IncludedModules) ? Array.ConvertAll(IncludedModules.Split(','), int.Parse) : new int[0];
            }
            set
            {
                var _data = value;
                IncludedModules = String.Join(",", _data.Select(p => p.ToString()).ToArray());
            }
        }
        [NotMapped]
        public int[] ArrayOfNeedByPotentialTrainees
        {
            get
            {
                return !string.IsNullOrEmpty(NeedByPotentialTrainees) ? Array.ConvertAll(NeedByPotentialTrainees.Split(','), int.Parse) : new int[0];
            }
            set
            {
                var _data = value;
                NeedByPotentialTrainees = String.Join(",", _data.Select(p => p.ToString()).ToArray());
            }
        }
    }
}
