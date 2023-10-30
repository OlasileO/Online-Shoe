using OnlineShoe.Model;

namespace Online_Shoe.DTO.ShoeReviewDTO
{
    public class ShoeReviewCreateDto
    {
        public int Id { get; set; }
        public int Shoe_Id { get; set; }
        //aspublic string Userid { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime Created_at { get; set; }
    }
}
