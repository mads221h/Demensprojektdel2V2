using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WelfareDenmarkLiveMap.Models
{
    public class Session
    {
        public int ID { get; set; }
        public int CompletionRate { get; set; }
        public DateTime Time { get; set; }
        public Patient Patient { get; set; }
    }
}
