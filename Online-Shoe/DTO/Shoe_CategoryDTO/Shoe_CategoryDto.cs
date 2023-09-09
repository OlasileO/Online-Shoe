using Online_Shoe.DTO.CategoryDTO;
using Online_Shoe.DTO.ShoeDTO;

namespace Online_Shoe.DTO.Shoe_CategoryDTO
{
    public class Shoe_CategoryDto
    {
        public int Id { get; set; }
        public int  Shoe_id { get; set; }
        public ShoeDto shoe { get; set; }
        public int Category_id { get; set; }
        public CategoryDTo Category { get; set; }
    }
}
