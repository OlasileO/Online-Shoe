using AutoMapper;
using Online_Shoe.DTO.Category;
using Online_Shoe.DTO.CategoryDTO;
using Online_Shoe.DTO.Shoe_CategoryDTO;
using Online_Shoe.DTO.ShoeDTO;
using OnlineShoe.Model;

namespace Online_Shoe.Helper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Shoe, ShoeDto>().ReverseMap();
            CreateMap<Shoe, CreateShoeDto>().ReverseMap();
            CreateMap<Shoe, UpdateShoeDto>().ReverseMap();

            /// category 
            CreateMap<Category, CategoryDTo>().ReverseMap();
            CreateMap<Category, CreateCategoryDto>().ReverseMap();
            CreateMap<Category, UpdateCategoryDto>().ReverseMap();


            /// Shoecategory
            CreateMap<Shoe_Category, Shoe_CategoryDto>().ReverseMap();
            CreateMap<Shoe_Category, CreateShoe_CategoryDTO>().ReverseMap();
            CreateMap<Shoe_Category, UpdateShoe_CategoryDto>().ReverseMap();


        }
    }
}
