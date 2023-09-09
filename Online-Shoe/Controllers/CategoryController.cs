using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Online_Shoe.DTO.Category;
using Online_Shoe.DTO.CategoryDTO;
using Online_Shoe.DTO.ShoeDTO;
using OnlineShoe.Model;
using OnlineShoe.Repository.Abstract;
using OnlineShoe.Repository.Implementation;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Online_Shoe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IcategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(IcategoryRepository categoryRepository, 
            IMapper mapper, ILogger<CategoryController> logger)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/<CategoryController>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IEnumerable<CategoryDTo>> GetAll()
        {
            var shoes = await _categoryRepository.GetAll();
            var result = _mapper.Map<List<CategoryDTo>>(shoes);
            return result;
        }

        // GET api/<CategoryController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetById(int id)
        {
            var shoes = await _categoryRepository.GetById(id);
            if (shoes == null)
            {
                return NotFound();
            }
            var result = _mapper.Map<CategoryDTo>(shoes);
            return Ok(result);
        }

        // POST api/<CategoryController>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> AddCategory([FromBody] CreateCategoryDto  createCategory)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid Post Attempt{nameof(AddCategory)}");
                return BadRequest(ModelState);
            }
            var categoryDTO = _mapper.Map<Category>(createCategory);
            await _categoryRepository.AddAsync(categoryDTO);
            return Ok("Successfully");
        }

        // PUT api/<CategoryController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] UpdateCategoryDto updateCategory)
        {
            if (!ModelState.IsValid || id < 1)
            {
                _logger.LogError($"Invalid Post Attempt{nameof(UpdateCategory)}");
                return BadRequest(ModelState);
            }
            var category = await _categoryRepository.GetByIdAsync(x => x.Id == id);
            if (category == null)
            {
                _logger.LogError($"invalid Update Attempt in {nameof(UpdateCategory)}");
                return BadRequest("Submitted Data is Invalid");
            }
            _mapper.Map(updateCategory, category);
            await _categoryRepository.UpdateAsync(category);
            return Ok("Successfully");
        }

        // DELETE api/<CategoryController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            if (id < 1)
            {
                _logger.LogError($"invalid Delete Attempt in {nameof(DeleteCategory)}");
                return BadRequest(ModelState);
            }
            var shoe = await _categoryRepository.GetByIdAsync(x => x.Id == id);
            if (shoe == null)
            {
                _logger.LogError($"invalid Delete Attempt in {nameof(DeleteCategory)}");
                return BadRequest("Submitted Data is Invalid");
            }

            await _categoryRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
