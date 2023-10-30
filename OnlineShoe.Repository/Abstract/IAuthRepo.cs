using OnlineShoe.Model;

namespace OnlineShoe.Repository.Abstract
{
    public interface IAuthRepo
    {
        Task<RegistrationResponse> Registration(Register register, string role);
        Task<AuthResponse> Login(Login login);
       

    }
}
