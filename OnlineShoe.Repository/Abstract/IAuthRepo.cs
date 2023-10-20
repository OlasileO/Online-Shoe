using OnlineShoe.Model;

namespace OnlineShoe.Repository.Abstract
{
    public interface IAuthRepo
    {
        Task<(int, string)> Registration(Register register, string role);
        Task<Token> Login(Login login);
        Task<Token> GetRefreshToken(TokenRefresh requestToken);

    }
}
