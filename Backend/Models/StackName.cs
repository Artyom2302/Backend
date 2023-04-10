using Backend.Models.DTO;

namespace Backend.Models
{
    public class StackName
    {
        private string name = string.Empty;

        public int Id { get; set; }
        public string Name
        {
            get => name; set => name = value.ToLower(); }
        public StackName(StackNamesDTO DTO)
        {
            this.Name = DTO.Name;
        }

        public StackName()
        {
        }
    }
}
