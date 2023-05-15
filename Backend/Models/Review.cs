using Backend.Models.DTO;

namespace Backend.Models
{
    public class Review
    {
        public int Id { get; set; }
        public double Score { get; set; }
        public string ReviewText { get; set; } = string.Empty;

        public Review()
        {
            this.ReviewText = "";
            this.Score = 0;

        }
        public Review(ReviewDTO reviewDTO) {
            this.ReviewText = reviewDTO.text; 
            this.Score=reviewDTO.score;

        }

    }
}
