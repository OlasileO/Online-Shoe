using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShoe.Model
{
    public class Shoe_Category
    {
        public Shoe? Shoe { get; set; }
        public int shoe_Id { get; set; }
        public Category? Category{ get; set; }
        public int Category_Id { get; set; }
    }
}
