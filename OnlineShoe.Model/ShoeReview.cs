using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShoe.Model
{
    public class ShoeReview
    {
        public int Id { get; set; } 
        public Shoe? Shoe { get; set; }   
        public int ShoeId { get; set; }
        public string userId { get; set; }
        public int Rating { get; set; } 
        public string? Comment { get; set; } 
        public DateTime Createdat { get; set; }    
    }
}
