using Backend.Models.DTO;

namespace Backend.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
       public List<Lab>? Labs { get; set; }
        public User() {
            this.Login = " ";
            this.Password = " ";
            this.Labs = null;
           
        }

       
        public User(Auth_User user)
        {
            this.Login = user.Login;
            this.Password = user.Password;
            this.IsAdmin = false;
            this.Labs = null;
         
        }
        public User(UserDTO user)
        {
          
            this.Login = user.Login;
            this.Password = user.Password;
            this.IsAdmin = user.IsAdmin;
            this.Labs = null;
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
                Labs.Add(lab);
                return true;
            }
            return false;
        }

    }
}
