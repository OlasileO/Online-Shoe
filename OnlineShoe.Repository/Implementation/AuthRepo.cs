using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OnlineShoe.Model;
using OnlineShoe.Model.Data;
using OnlineShoe.Repository.Abstract;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OnlineShoe.Repository.Implementation
{
    public class AuthRepo : IAuthRepo
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AuthRepo(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, 
            IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        private async Task<JwtSecurityToken> GenerateToken(AppUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var roleClaims = new List<Claim>();

            for (int i = 0; i < roles.Count; i++)
            {
                roleClaims.Add(new Claim(ClaimTypes.Role, roles[i]));
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTKey:Secret"])!);
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _configuration["JWTKey:ValidIssuer"],
                audience: _configuration["JWTKey:ValidAudience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(15),
                signingCredentials: signingCredentials);
            return jwtSecurityToken;
        }


        public async Task<AuthResponse> Login(Login login)
        {
            var user = await _userManager.FindByEmailAsync(login.Email);

            if (user is null)
            {
                throw new Exception("Invalid Email");
            }


            if (!await _userManager.CheckPasswordAsync(user, login.Password))
            {

                throw new Exception("Invalid Password");
            }

            JwtSecurityToken jwtSecurityToken = await GenerateToken(user);

            var response = new AuthResponse
            {
                Id = user.Id,
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                Email = user.Email,
                UserName = user.UserName
            };

            return response;
        }

        public async Task<RegistrationResponse> Registration(Register register, string role)
        {
            var userExists = await _userManager.FindByEmailAsync(register.Email);
            if (userExists != null)
                throw new Exception("User already exists");
            AppUser user = new()
            {
                Email = register.Email,
                UserName = register.UserName,
                SecurityStamp = Guid.NewGuid().ToString(),
                FristName = register.FristName,
                LastName = register.LastName,
                PhoneNumber = register.PhoneNumber,
                Address = register.Address,
            };
            var createUser = await _userManager.CreateAsync(user, register.Password);
            if (!createUser.Succeeded)
                throw new Exception("User creation failed! Please check user details and try again");
            if (!await _roleManager.RoleExistsAsync(role))
                await _roleManager.CreateAsync(new IdentityRole(role));

            if (await _roleManager.RoleExistsAsync(role))
                await _userManager.AddToRoleAsync(user, role);

            return new RegistrationResponse() { UserId = user.Id }; ;
        }
    }
}