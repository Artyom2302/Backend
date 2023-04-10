namespace Backend.Models.DTO
{
    public class LabDTO
    {
       

        public int Id { get; set; }
        public string Name { get; set; }=string.Empty;
        public string Main_stack { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public bool IsDone { get; set; }

        public LabDTO(Lab lab)
        {
            Id = lab.Id;
            Name = lab.Name;
            Main_stack = lab.Main_stack;
            Date = lab.Date;
            IsDone = lab.IsDone;

        }
        public LabDTO()
        {
          
        }
    }
}
