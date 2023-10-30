using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShoe.Model
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        public int shodId { get; set; }
        public Shoe Shoe { get; set; }  
        public int Quatity { get; set; }    
        public string shoppingId {get; set; }
    }
}
