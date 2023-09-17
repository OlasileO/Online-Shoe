using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Online_Shoe.DTO.ShoeDTO;
using OnlineShoe.Model;
using OnlineShoe.Repository.Abstract;


namespace Online_Shoe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoeController : ControllerBase
    {
        private readonly IShoeRepository _shoeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ShoeController> _logger;

        public ShoeController(IShoeRepository shoeRepository, IMapper mapper, 
            ILogger<ShoeController> logger)
        {
            _shoeRepository = shoeRepository;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/<ShoeController>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IEnumerable<ShoeDto>> GetAll()
        {
            var shoes = await _shoeRepository.GetAll();
<<<<<<< HEAD
            var result = _mapper.Map<List<ShoeDto>>(shoes);
            return result;
        }
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetById(int id)
        {
            var shoes = await _shoeRepository.GetById(id);
            if (shoes == null)
            {
                return NotFound();
            }
            var result = _mapper.Map<ShoeDto>(shoes);
            return Ok(result);
=======
            var result =  _mapper.Map<List<ShoeDto>>(shoes);
            return result;
>>>>>>> 9130bbaf77307dbb0ddecfb96e8635117c969e17
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> AddShoe([FromBody] CreateShoeDto shoeCreate)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid Post Attempt{nameof(AddShoe)}");
                return BadRequest(ModelState);
            }
            var shoeDTO = _mapper.Map<Shoe>(shoeCreate);
<<<<<<< HEAD
            await _shoeRepository.AddAsync(shoeDTO);
=======
             await _shoeRepository.AddAsync(shoeDTO);
>>>>>>> 9130bbaf77307dbb0ddecfb96e8635117c969e17
            return Ok("Successfully");
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateShoe(int id, [FromBody] UpdateShoeDto updateShoe)
        {
            if (!ModelState.IsValid || id < 1)
            {
                _logger.LogError($"Invalid Post Attempt{nameof(AddShoe)}");
                return BadRequest(ModelState);
            }
            var shoe = await _shoeRepository.GetByIdAsync(x => x.Id == id);
            if (shoe == null)
            {
                _logger.LogError($"invalid Update Attempt in {nameof(UpdateShoe)}");
                return BadRequest("Submitted Data is Invalid");
            }
            _mapper.Map(updateShoe, shoe);
            await _shoeRepository.UpdateAsync(shoe);
            return Ok("Successfully");
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteShoe(int id)
        {
            if (id < 1)
            {
                _logger.LogError($"invalid Delete Attempt in {nameof(DeleteShoe)}");
                return BadRequest(ModelState);
            }
            var shoe = await _shoeRepository.GetByIdAsync(x => x.Id == id);
            if (shoe == null)
            {
                _logger.LogError($"invalid Delete Attempt in {nameof(DeleteShoe)}");
                return BadRequest("Submitted Data is Invalid");
            }

            await _shoeRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
