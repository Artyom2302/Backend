namespace Backend.Models.DTO
{
    public class Auth_User
    {
        public string Login { get; set; }
        public string Password { get; set; }

        public Auth_User() { 
            Login = " "; 
            Password = " ";
        }

        public Auth_User(User user) {
                this.Login = user.Login;
                this.Password = user.Password;
        }
    }
}
