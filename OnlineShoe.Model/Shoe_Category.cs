using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShoe.Model
{
    public class Shoe_Category
    {
        public int Id { get; set; }
        public Shoe? Shoe_id { get; set; }
        public Category? Category_id { get; set; }
    }
}
