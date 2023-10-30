using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShoe.Model;
using OnlineShoe.Repository.Abstract;

namespace Online_Shoe.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly  IAuthRepo _authRepo;
        private readonly ILogger<AuthenticationController> _logger;
        private readonly UserManager<AppUser> _userManager;

        public AuthenticationController(IAuthRepo authRepo,
            ILogger<AuthenticationController> logger,
            UserManager<AppUser> userManager)
        {
            _authRepo = authRepo;
            _logger = logger;
            _userManager = userManager;
            
        }
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody]Login model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _authRepo.Login(model);
               return Ok(result); 
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(Register model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok( await _authRepo.Registration(model, UserRoles.User));
            
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        
    }
}
