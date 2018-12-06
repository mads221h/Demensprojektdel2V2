using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WelfareDenmarkLiveMap.Models
{
    public class Patient
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public County County { get; set; }

    }
}
