using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WelfareDenmarkLiveMap.Models
{
    public class Exercise
    {
        public int ID { get; set; }
        public ExerciseType ExerciseType { get; set; }
        public int CompletionRate { get; set; }
        public Session Session { get; set; }
        

    }
}
