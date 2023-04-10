namespace Backend.Models
{
    public class Programmer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Main_stack { get; set; }
        public List<Lab> Labs { get; set; }
        public double score { 
            get {
                double avg_score=0;
                foreach (Lab lab in Labs)
                {
                    if (lab.Review != null)
                    {
                        avg_score+=lab.Review.Score;
                    }
                }
                return avg_score;
            }
             }

 
        public Programmer() {
        this.Id = 0; 
        this.Name = string.Empty;  
        this.Surname = string.Empty; 
        this.Main_stack =string.Empty;
        this.Labs = new List<Lab>();
       
        }

        public bool AddLab(Lab lab)
        {
            if (lab != null && !Labs.Contains(lab) && lab.Main_stack==Main_stack)
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
