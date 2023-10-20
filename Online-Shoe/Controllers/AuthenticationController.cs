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
                
                if (result.StatusCode == 0)
                    return BadRequest(result.StatusMessage);
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

                var (status, message) = await _authRepo.Registration(model, UserRoles.User);
                if (status == 0)
                    return BadRequest(message);
                return CreatedAtAction(nameof(Register), model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [HttpPost]
        [Route("refreshToken")]
        public async Task<IActionResult> RefreshToken(TokenRefresh model)
        {
            try
            {
                if (model == null)
                    return BadRequest("Invalid BadRequest");
                var result = await _authRepo.GetRefreshToken(model);
                if (result.StatusCode == 0)
                    return BadRequest(result.StatusMessage);
                return Ok(result);


            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
       
    }
}
