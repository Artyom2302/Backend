namespace Backend.Models.DTO
{
    public class ProgrammerDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Main_stack { get; set; }
    


        public ProgrammerDTO()
        {
            this.Id = 0;
            this.Name = string.Empty;
            this.Surname = string.Empty;
            this.Main_stack = string.Empty;
 
        }
        public ProgrammerDTO(int id, string name, string surname, string main_stack, int Order_Id, List<Order> orders)
        {
            this.Id = id;
            this.Name = name;
            this.Surname = surname;
            this.Main_stack = main_stack;
        }
        public ProgrammerDTO(Programmer prog)
        {
            this.Id = prog.Id;
            this.Name = prog.Name;
            this.Surname = prog.Surname;
            this.Main_stack = prog.Main_stack;
        }
    }
}
