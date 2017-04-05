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
    [Table("TRAINER")]
    public class Trainer : Entity<int>
    {
        [Required]
        public virtual int PersonId { get; set; }
        [ForeignKey("PersonId")]
        public virtual Person Person { get; set; }

        public bool IsExternal { get; set; }

        /* FIT_TO_TRAIN_MODULES*/
        [Required]
        public string SuitableModules { get; set; }

        [NotMapped]
        public int[] ArrayOfSuitableModules
        {
            get
            {
                return !string.IsNullOrEmpty(SuitableModules) ? Array.ConvertAll(SuitableModules.Split(','), int.Parse) : new int[0];
            }
            set
            {
                var _data = value;
                SuitableModules = String.Join(",", _data.Select(p => p.ToString()).ToArray());
            }
        }
    }
}
