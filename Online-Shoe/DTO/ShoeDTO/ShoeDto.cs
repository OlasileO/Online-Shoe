﻿using Online_Shoe.DTO.CategoryDTO;
namespace Online_Shoe.DTO.ShoeDTO
{
    public class ShoeDto
    {
        public int Id { get; set; }
        public int? CategoryId { get; set; }
        //public CategoryDTo? CategoryDTO { get; set; }
        public string? Model { get; set; }
        public string? Image { get; set; }
        public double Price { get; set; }
        public int Size { get; set; }
        public string? Color { get; set; }
        public string? Description { get; set; }
        public DateTime Created_At { get; set; }
    }
}
