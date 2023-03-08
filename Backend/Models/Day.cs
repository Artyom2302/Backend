using Microsoft.EntityFrameworkCore;
using System.Text;


namespace Backend.Models
{
   
    public class Day
    {
            
        StringBuilder dayOfWeekend;
        int month;
        int dayInMonth;
        Activity activity;

        StringBuilder DayOfWeekend {
            get
            {
                return dayOfWeekend;
            }
            set
            {
                dayOfWeekend = value;
            }
                }
        int Month { 
            get 
            { 
                return month;
            }
            set
            {
                month= value;
            }
        }
        int DayInMonth
        {
            get
            {
                return dayInMonth;
            }
            set
            {
                dayInMonth= value;
            }
        }
       

        public Day()
        {
            this.dayOfWeekend = new StringBuilder();
            this.month = 1;
            this.dayInMonth = 1;
            this.activity = new Activity();    
        }



    }
}
