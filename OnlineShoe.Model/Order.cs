using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShoe.Model
{
    public class Order
    {
        public int Id { get; set; } 
        public string Userid { get; set; }
        public AppUser User { get; set; }

        public DateTime Order_date { get; set; }
        
        public ICollection<Orderitem> orderItems { get; set; }
    }
}
