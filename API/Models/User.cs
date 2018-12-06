using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class User : IdentityUser
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
