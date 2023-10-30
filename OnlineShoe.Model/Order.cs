using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShoe.Model
{
    public class Order
    {
        public int Id { get; set; } 
        public string User_id { get; set; }
        public int Total_order { get; set; }    
        public DateTime Order_date { get; set; }
        public ICollection<Order_item> order_Items { get; set; }
    }
}
