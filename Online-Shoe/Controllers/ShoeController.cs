using Microsoft.AspNetCore.Mvc;
using Online_Shoe.DTO.ShoeDTO;
using OnlineShoe.Model;
using OnlineShoe.Repository.Abstract;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Online_Shoe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoeController : ControllerBase
    {
        private readonly IShoeRepository _shoeRepository;

        public ShoeController(IShoeRepository shoeRepository)
        {
            _shoeRepository = shoeRepository;
        }

        // GET: api/<ShoeController>
        [HttpGet]
        public async Task<IEnumerable<ShoeDto>> GetAll()
        {
            var shoes = await _shoeRepository.GetAll();
            return shoes;
        }

        [HttpPost]
        public async Task<IActionResult> AddShoe([FromBody] CreateShoeDto shoe)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result  = await _shoeRepository.AddAsync(shoe);
            return Ok("Successfully");
        }

      
    }
}
