using AutoMapper;
<<<<<<< HEAD
using Online_Shoe.DTO.Category;
using Online_Shoe.DTO.CategoryDTO;
using Online_Shoe.DTO.Shoe_CategoryDTO;
=======
>>>>>>> 9130bbaf77307dbb0ddecfb96e8635117c969e17
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
<<<<<<< HEAD

            /// category 
            CreateMap<Category, CategoryDTo>().ReverseMap();
            CreateMap<Category, CreateCategoryDto>().ReverseMap();
            CreateMap<Category, UpdateCategoryDto>().ReverseMap();


            /// Shoecategory
            CreateMap<Shoe_Category, Shoe_CategoryDto>().ReverseMap();
            CreateMap<Shoe_Category, CreateShoe_CategoryDTO>().ReverseMap();
            CreateMap<Shoe_Category, UpdateShoe_CategoryDto>().ReverseMap();


=======
>>>>>>> 9130bbaf77307dbb0ddecfb96e8635117c969e17
        }
    }
}
