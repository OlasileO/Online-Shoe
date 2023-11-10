using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShoe.Model;
using OnlineShoe.Repository.Abstract;
using OnlineShoe.Repository.Implementation;

namespace Online_Shoe.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly  IAuthRepo _authRepo;
        private readonly ILogger<AuthenticationController> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmailSender _emailSender;

        public AuthenticationController(IAuthRepo authRepo,
            ILogger<AuthenticationController> logger,
            UserManager<AppUser> userManager,
            IEmailSender emailSender)
        {
            _authRepo = authRepo;
            _logger = logger;
            _userManager = userManager;
            _emailSender = emailSender;
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
                var register = await _authRepo.Registration(model, UserRoles.User);


                string emailSubject = "Contact Confirmation";
                string username = model.FristName + " " + model.LastName;
                string emailMessage = "Dear " + username + "\n" +
                    "We received you message. Thank you for register with us.\n" +
                    "You are welcome the OnelineShoe Store.\n" +
                    "Best Regards\n";


                _emailSender.SendEmail(emailSubject, model.Email, username, emailMessage).Wait();

                return Ok(register);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

            //try
            //{
            //    if (!ModelState.IsValid)
            //        return BadRequest(ModelState);

            //    return Ok(await _authRepo.Registration(model, UserRoles.User));



            //}
            //catch (Exception ex)
            //{
            //    _logger.LogError(ex.Message);
            //    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            //}


        }
    }
}
