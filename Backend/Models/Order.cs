namespace Backend.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Main_stack { get; set; }
        public string Secret_Info { get; set; }
        public Order() { this.Id = 0; this.Main_stack = " "; this.Name = "";this.Secret_Info = " "; }
        public Order(int id, string name,string main_stack)
        {
            this.Id = id;
            this.Name = name;
            this.Main_stack = main_stack;
            this.Secret_Info = " ";
        }
        

    }
}
