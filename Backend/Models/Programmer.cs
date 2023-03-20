namespace Backend.Models
{
    public class Programmer
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Main_stack { get; set; }

        public int Order_Id  { get; set; }
        public bool Is_admin { get; set; }


    public Programmer() {
        this.Name = string.Empty;  
        this.Surname = string.Empty; 
        this.Main_stack = string.Empty;
        this.Order_Id = 0;
        this.Is_admin = false;
        }
        public Programmer(string name,string surname,string main_stack,int Order_Id,bool isadmin)
        {
           this.Name = name;    
           this.Surname = surname;
           this.Main_stack = main_stack;
           this.Order_Id = Order_Id;
            this.Is_admin = isadmin;
        }

    }
}
