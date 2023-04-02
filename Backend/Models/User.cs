using Backend.Models.DTO;

namespace Backend.Models
{
    public class User
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public User() {
            this.Login = " ";
            this.Password = " ";  

        }

        public User(string login, string password,bool isAdmin)
        {
            this.Login = login;
            this.Password = password;
            this.IsAdmin = isAdmin;
        }

        public User(Auth_User user)
        {
            this.Login = user.Login;
            this.Password = user.Password;
            this.IsAdmin = false;
        }
    }
}
