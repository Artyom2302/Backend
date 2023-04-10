namespace Backend.Models.DTO
{
    public class ProgrammerDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }=string.Empty;
        public string Surname { get; set; } = string.Empty; 
        public string Main_stack { get; set; } = string.Empty;
        public ProgrammerDTO()
        {

        }    
    }
}
