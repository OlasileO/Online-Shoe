using OnlineShoe.Model;

namespace Online_Shoe.DTO.ShoeReviewDTO
{
    public class ShoeReviewDto
    {
        public int Id { get; set; }
        public int Shoe_Id { get; set; }
        //public AppUser userid { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime Created_at { get; set; }
    }
}
