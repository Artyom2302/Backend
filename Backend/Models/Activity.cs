using System.Runtime.CompilerServices;
using System.Text;

namespace Backend.Models
{
    public class Activity
    {
        int WalkStepCount;
        int RunStepCount;
        int UpStepCount ;
        int Walk { 
            get {
                return WalkStepCount;
            } 
            set { 
                WalkStepCount = value;
            } 
        }
        int Run
        {
            get
            {
                return RunStepCount;
            }
            set
            {
                RunStepCount = value;
            }
        }
        int Up
        {
            get
            {
                return UpStepCount;
            }
            set
            {
                UpStepCount = value;
            }
        }

        int AllCount
        {
            get
            {
                return WalkStepCount + RunStepCount + UpStepCount;
            }

        }

        void AddStep(string NameActivity) {
            switch  (NameActivity) {
                case "Walk":
                    ++this.WalkStepCount;
                    break;
                case "Run":
                    ++this.RunStepCount;
                    break;
                case "Up":
                    ++this.UpStepCount;
                    break;
            }
         }
        void AddSteps(string NameActivity,int Steps)
        {
            switch (NameActivity)
            {
                case "Walk":
                    this.WalkStepCount+=Steps;
                    break;
                case "Run":
                    this.RunStepCount += Steps;
                    break;
                case "Up":
                    this.UpStepCount += Steps;
                    break;
                
            }
        }

        public Activity(int Walk,int Run,int Step) {
            this.WalkStepCount = Walk;
            this.RunStepCount = Run;
            this.UpStepCount = Step;
        }

        public Activity()
        {
            this.WalkStepCount =0;
            this.RunStepCount = 0;
            this.UpStepCount = 0;
        }


    }
}
