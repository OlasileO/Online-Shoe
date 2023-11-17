using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShoe.Model
{
    public class Orderitem
    {
        public int Id { get; set; } 
        public Order? Order{ get; set; }
        public int OrderId { get; set; }
        public Shoe? Shoe { get; set; }
        public int ShoeId { get; set; }
        public int quantity { get; set; }   
        public double Totalprice { get; set; }

    }
}
