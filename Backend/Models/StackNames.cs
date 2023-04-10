namespace Backend.Models
{
    public class StackNames
    {
        public int Id { get; set; } 
        public string Name { get; set; }=string.Empty;
        public List<StackName>? DifferentNames { get; set; }

        public bool AddDiffNames(StackName stack) {
            if (stack != null && (DifferentNames!=null) && !DifferentNames.Contains(stack))
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
    }
}
