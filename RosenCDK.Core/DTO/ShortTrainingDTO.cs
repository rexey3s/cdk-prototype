using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RosenCDK.DTO
{
    public class ShortTrainingDTO
    {
        public ShortTrainingDTO(string name)
        {
            ProgramName = name;
        }

        public string ProgramName { get; set; }
    }
}
