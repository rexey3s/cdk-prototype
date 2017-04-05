using System.Collections.Generic;

namespace RosenCDK.DTO
{
    public class TrainerDTO 
    {
        public int TrainerId { get; set; }

        public int PersonId { get; set; }
        public bool IsExternal { get; set; }

        public List<int> FitToTrainModules { get; set; }
        
    }
}
