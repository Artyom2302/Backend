using Backend.Models.DTO;
using Microsoft.Identity.Client;

namespace Backend.Models
{
    public class Lab
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Main_stack { get; set; }
        public DateTime Date { get; set; }
        public bool IsDone { get; set; }
        public int? UserId { get; set; }
        public int? ProgrammerId { get; set; }
        public Review? Review
        {
            get; set;
        }
        public Lab() { this.Id = 0; this.Main_stack =string.Empty; this.Name = "";this.Date = new DateTime(); Review =null; }
        public Lab(LabDTO dto)
        {
            
            this.Name = dto.Name;
            this.Main_stack = dto.Main_stack;
            this.Date = dto.Date;
            this.IsDone = dto.IsDone;
            this.Review = null;
        }
        public Lab(LabDTOAdd dto)
        {

            this.Name = dto.Name;
            this.Main_stack = dto.Main_stack;
            this.Date = DateTime.Now;
            this.IsDone = false;
            this.Review = null;
        }

        public bool AddReview(Review review)
        {
            if (review != null && Review==null)
            {
               Review = review;
                return true;
            }
            return false;
        }

        public bool DeleteReview(Review review)
        {
            if (review != null && Review != null && Review==review)
            {
                Review = null;
                return true;
            }
            return false;
        }





    }
}
