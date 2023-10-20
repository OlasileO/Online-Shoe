using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OnlineShoe.Model;
using OnlineShoe.Model.Data;
using OnlineShoe.Repository.Abstract;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace OnlineShoe.Repository.Implementation
{
    public class AuthRepo : IAuthRepo
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ShoeDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public AuthRepo(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, 
            ShoeDbContext dbContext, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _dbContext = dbContext;
            _configuration = configuration;
        }

        public async Task<Token> GetRefreshToken(TokenRefresh requestToken)
        {
            Token _token = new Token();
            var principal = GetPrincipalFromExpiredToken(requestToken.AccessToken);
            string username = principal.Identity.Name;
            var user = await _userManager.FindByNameAsync(username);

            if (user == null || user.RefreshToken != requestToken.RefreshToken)
            {
                _token.StatusCode = 0;
                _token.StatusMessage = "Invalid access token or refresh token";
                return _token;
            }

            var authClaims = new List<Claim>
            {
               new Claim(ClaimTypes.Name, user.UserName),
               new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
            var newToken = GenerateToken(authClaims);
            var newRefreshToken = GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            await _userManager.UpdateAsync(user);

            _token.StatusCode = 1;
            _token.StatusMessage = "Success";
            _token.AccessToken = newToken;
            _token.RefreshToken = newRefreshToken;
            return _token;
        }
        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            };
        }


        private string GenerateToken(List<Claim> authClaims)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = Encoding.UTF8.GetBytes(_configuration["JWTKey:Secret"]);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Issuer = _configuration["JWTKey:ValidIssuer"],
                    Audience = _configuration["JWTKey:ValidAudience"],
                    Expires = DateTime.Now.AddMinutes(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature),
                    Subject = new ClaimsIdentity(authClaims),
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var refreshToken = GenerateRefreshToken();
                return tokenHandler.WriteToken(token);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var Key = Encoding.UTF8.GetBytes(_configuration["JWTKey:Secret"]);

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Key),
                ClockSkew = TimeSpan.Zero
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            JwtSecurityToken jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }


            return principal;
        }

        public async Task<Token> Login(Login login)
        {
            Token _token = new Token();
            var user = await _userManager.FindByNameAsync(login.Username);
            if (user is null)
            {
                _token.StatusCode = 0;
                _token.StatusMessage = "Invalid User";
                return _token;
            }

            if (!await _userManager.CheckPasswordAsync(user, login.Password))
            {
                _token.StatusCode = 0;
                _token.StatusMessage = "Invalid Password";
                return _token;
            }

            var userRoles = await _userManager.GetRolesAsync(user);

            var authClaim = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
               new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),

            };
            foreach (var userRole in userRoles)
            {
                authClaim.Add(new Claim(ClaimTypes.Role, userRole));
            }
            _token.AccessToken = GenerateToken(authClaim);
            _token.RefreshToken = GenerateRefreshToken();
            _token.StatusCode = 1;
            _token.StatusMessage = "Success";

            //var _RefreshTokenValidityInDays = Convert.ToInt64(_configuration["JWTKey:RefreshTokenValidityInDays"]);
            user.RefreshToken = _token.RefreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddMinutes(1);
            await _userManager.UpdateAsync(user);

            return _token;
        }

        public async Task<(int, string)> Registration(Register register, string role)
        {
            var userExists = await _userManager.FindByEmailAsync(register.UserName);
            if (userExists != null)
                return (0, "User already exists");
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
                return (0, "User creation failed! Please check user details and try again");
            if (!await _roleManager.RoleExistsAsync(role))
                await _roleManager.CreateAsync(new IdentityRole(role));

            if (await _roleManager.RoleExistsAsync(role))
                await _userManager.AddToRoleAsync(user, role);

            return (1, "User Successfully Created");
        }
    }
}
