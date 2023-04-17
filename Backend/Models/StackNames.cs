using Backend.Models.DTO;

namespace Backend.Models
{
    public class StackNames
    {
        private string name = string.Empty;

        public int Id { get; set; }
        public string Namestack { get; set; }
        public List<StackName> DifferentNames { get; set; }=new List<StackName>();

        public bool AddDiffNames(StackName stack)
        {
            if (stack != null && !DifferentNames.Contains(stack))
            {
                DifferentNames.Add(stack);
                return true;
            }
            return false;
        }
        public bool DeleteDiffNames(StackName stack)
        {
            if (stack != null && DifferentNames.Contains(stack))
            {
                DifferentNames.Remove(stack);
                return true;
            }
            return false;
        }
        public StackNames()
        {
            
        }
        public StackNames(string name)
        {
            this.Namestack = name;
            this.DifferentNames=new List<StackName>();
        }

    }
}
