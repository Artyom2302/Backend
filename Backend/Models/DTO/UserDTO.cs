namespace Backend.Models.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public UserDTO(User user) {
        this.Id = user.Id;
            this.Login = user.Login;    
            this.Password = user.Password;
            this.IsAdmin = user.IsAdmin;
        }
        public UserDTO()
        {
            
        }


    }
}
