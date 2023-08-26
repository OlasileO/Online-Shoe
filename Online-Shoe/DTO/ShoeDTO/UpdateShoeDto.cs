namespace Online_Shoe.DTO.ShoeDTO
{
    public class UpdateShoeDto
    {
        public int Id { get; set; }
        public string? ShoeBrand { get; set; }
        public string? Model { get; set; }
        public string? Image { get; set; }
        public double Price { get; set; }
        public int Size { get; set; }
        public string? Color { get; set; }
        public string? Description { get; set; }
        public DateTime Updated_At { get; set; }
    }
}
