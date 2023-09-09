using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShoe.Model
{
    public class Order_item
    {
        public int Id { get; set; } 
        public Order? Order{ get; set; }
        public int Order_Id { get; set; }
        public Shoe? Shoe { get; set; }
        public int Shoe_Id { get; set; }
        public int quantity { get; set; }   
        public double Total_price { get; set; }

    }
}
