using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using OnlineShoe.Model;
using System.Security.Claims;

namespace OnlineShoe.Repository.Implementation
{
    public class UserRepository
    {
        private readonly UserManager<AppUser>   _userManager;
        private readonly IHttpContextAccessor _contextAccessor;

        public UserRepository(UserManager<AppUser> userManager, 
            IHttpContextAccessor contextAccessor)
        {
            _userManager = userManager;
            _contextAccessor = contextAccessor;
        }

        public  string UserId { get => _contextAccessor.HttpContext?.User?.FindFirstValue("uid")!; }

        public static string GetUserId(ClaimsPrincipal user)
        {
            var identity = user.Identity as ClaimsIdentity;
            if (identity == null)
            {
                return null;
            }

            var claim = identity.Claims.FirstOrDefault(c => c.Type.ToLower() == "uid");
            if (claim == null)
            {
                return null;
            }



            return claim.Value; ;
        }

       
    }
}
