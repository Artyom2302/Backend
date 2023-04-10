namespace Backend.Models
{
    public class Review
    {
        public int Id { get; set; }
        public double Score { get; set; }
        public string ReviewText { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.Now;   
    }
}
