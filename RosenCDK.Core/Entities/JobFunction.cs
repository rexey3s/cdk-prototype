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
    [Table("JOBFUNCTION")]
    public class JobFunction : Entity<int>
    {
    
        [Required]
        public string JobFunctionTitle { get; set; }
        [Required]
        [DefaultValue("")]
        public string RequiredCompetences { get; set; }

        [NotMapped]
        public int[] ArrayOfCompetencies
        {
            get
            {
                return !string.IsNullOrEmpty(RequiredCompetences) ? Array.ConvertAll(RequiredCompetences.Split(','), int.Parse) : new int[0];
            }
            set
            {
                var _data = value;
                RequiredCompetences = String.Join(",", _data.Select(p => p.ToString()).ToArray());
            }
        }
    }
}
