namespace RosenCDK.DTO
{
    public class CompetenceDTO 
    {
        public CompetenceDTO()
        {
            
        }

        public CompetenceDTO(int competenceId, string name, string description)
        {
            CompetenceID = competenceId;
            Name = name;
            Description = description;
        }

        public int CompetenceID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
