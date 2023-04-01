namespace Backend.Models
{
    public class Programmer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Main_stack { get; set; }
        public List<Order> Orders { get; set; } 


    public Programmer() {
            this.Id = 0; 
        this.Name = string.Empty;  
        this.Surname = string.Empty; 
        this.Main_stack = string.Empty;
       
        this.Orders = new List<Order>();
        }
        public Programmer(int id,string name,string surname,string main_stack,int Order_Id,List<Order> orders)
        {
           this.Id = id;
           this.Name = name;    
           this.Surname = surname;
           this.Main_stack = main_stack;
           this.Orders = orders;
        }
        

    }
}
