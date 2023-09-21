using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShoe.Model
{
    public class Shoe
    {
        public int Id { get; set; }
        public string? ShoeBrand { get; set; }
        public Category Category { get; set; }
        public int? CategoryId { get; set; } 
        public string? Image { get; set; }
        public double Price { get; set; }
        public int Size { get; set; }
        public string? Color { get; set; }
        public string? Description { get; set; }
        public DateTime Created_At { get; set; }
        public DateTime Updated_At { get;set; }
        public ICollection<Shoe_Review> Reviews { get; set; }
        public ICollection<Order_item> Order_Items { get; set; }
    }
}
