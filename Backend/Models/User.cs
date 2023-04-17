using Backend.Models.DTO;

namespace Backend.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
       public List<Lab> Labs { get; set; }=new List<Lab>();
        public User() {
            this.Login = " ";
            this.Password = " ";
            this.Labs = new List<Lab>();
           
        }

       
        public User(Auth_User user)
        {
            this.Login = user.Login;
            this.Password = user.Password;
            this.IsAdmin = false;
            this.Labs = new List<Lab>();

        }
        public User(UserDTO user)
        {
          
            this.Login = user.Login;
            this.Password = user.Password;
            this.IsAdmin = user.IsAdmin;
            this.Labs = new List<Lab>();
        }

        public bool AddLab(Lab lab)
        {
            if (lab != null && !Labs.Contains(lab))
            {
                Labs.Add(lab);
                return true;
            }
            return false;
        }

        public bool DeleteLab(Lab lab)
        {
            if (lab != null && Labs.Contains(lab))
            {
                Labs.Remove(lab);
                return true;
            }
            return false;
        }

    }
}
