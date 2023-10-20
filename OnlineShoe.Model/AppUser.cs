using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShoe.Model
{
    public class AppUser:IdentityUser
    {
        public string FristName { get; set; }   
        public string LastName { get; set; }
        public string Address { get; set; } 
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public bool isActive { get; set; } = true;

    }
}
