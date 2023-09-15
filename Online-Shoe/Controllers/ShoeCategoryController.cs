using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Online_Shoe.DTO.Category;
using Online_Shoe.DTO.CategoryDTO;
using Online_Shoe.DTO.Shoe_CategoryDTO;
using OnlineShoe.Model;
using OnlineShoe.Repository.Abstract;
using OnlineShoe.Repository.Implementation;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Online_Shoe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoeCategoryController : ControllerBase
    {
        private readonly IShoe_CategoryRepository _shoecategoryRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ShoeCategoryController> _logger;

        public ShoeCategoryController(IShoe_CategoryRepository shoecategoryRepository,
            IMapper mapper, ILogger<ShoeCategoryController> logger)
        {
            _shoecategoryRepository = shoecategoryRepository;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/<ShoeCategoryController>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IEnumerable<Shoe_CategoryDto>> GetAll()
        {
            var shoe_Categories= await _shoecategoryRepository.GetAll();
            var result = _mapper.Map<List<Shoe_CategoryDto>>(shoe_Categories);
            return result;
        }
        //nnnnn
        // GET api/<ShoeCategoryController>/5
        //[HttpGet("{id}")]
        //[ProducesResponseType(200)]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(500)]
        //public async Task<IActionResult> GetById(int id)
        //{
        //    var shoe_Categories = await _shoecategoryRepository.GetById(id);
        //    if (shoe_Categories == null)
        //    {
        //        return NotFound();
        //    }
        //    var result = _mapper.Map<Shoe_CategoryDto>(shoe_Categories);
        //    return Ok(result);
        //}

        // POST api/<ShoeCategoryController>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> AddShoeCategory([FromBody] CreateShoe_CategoryDTO createShoe_Category)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid Post Attempt{nameof(AddShoeCategory)}");
                return BadRequest(ModelState);
            }
            var shoecategoryDTO = _mapper.Map<Shoe_Category>(createShoe_Category);
            await _shoecategoryRepository.AddAsync(shoecategoryDTO);
            return Ok("Successfully");
        }

    }
}
