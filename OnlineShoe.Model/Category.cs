using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShoe.Model
{
    public class Category
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public ICollection<Shoe> Shoes { get; set; }
    }
}
