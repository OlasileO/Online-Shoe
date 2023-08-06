﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShoe.Model
{
    public class Shoe_Review
    {
        public int Id { get; set; } 
        public Shoe? Shoe_id { get; set; }   
        //public AppUser User_id { get; set; }
        public int Rating { get; set; } 
        public string? Comment { get; set; } 
        public DateTime Created_at { get; set; }    
    }
}