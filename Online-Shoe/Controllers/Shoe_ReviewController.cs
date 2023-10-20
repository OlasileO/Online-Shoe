using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Online_Shoe.DTO.CategoryDTO;
using Online_Shoe.DTO.ShoeReviewDTO;
using OnlineShoe.Model;
using OnlineShoe.Repository.Abstract;
using OnlineShoe.Repository.Implementation;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Online_Shoe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class Shoe_ReviewController : ControllerBase
    {
        private readonly IShoeReview _shoeReview;
        private readonly IShoeRepository _shoeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<Shoe_ReviewController> _logger;
        private readonly UserManager<AppUser> _userManager;

        public Shoe_ReviewController(IShoeReview shoeReview, IShoeRepository shoeRepository, IMapper mapper, 
            ILogger<Shoe_ReviewController> logger,
             UserManager<AppUser> userManager)
        {
            _shoeReview = shoeReview;
            _shoeRepository = shoeRepository;
            _mapper = mapper;
            _logger = logger;
            _userManager = userManager;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetById(int id)
        {
            var shoeReview = await _shoeReview.GetById(id);
            if (shoeReview == null)
            {
                return NotFound();
            }
            var result = _mapper.Map<ShoeReviewDto>(shoeReview);
            return Ok(result);
        }
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IEnumerable<ShoeReviewDto>> GetAll()
        {
            var shoereview = await _shoeReview.GetAll();
            var result = _mapper.Map<List<ShoeReviewDto>>(shoereview);
            return result;
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateReview([FromBody] ShoeReviewCreateDto reviewCreate)
        {
           

            if (reviewCreate == null)
                return BadRequest(ModelState);


            if (!ModelState.IsValid)
                return BadRequest(ModelState);
       
            var categoryDTO = _mapper.Map<Shoe_Review>(reviewCreate);
            categoryDTO.Shoe = await _shoeRepository.GetById(reviewCreate.Shoe_Id);
            await _shoeReview.AddAsync(categoryDTO);
            return Ok("Successfully Created");
        }


    }
}
