using WebApplication3.Models;

namespace WebApplication3.Services
{
    public interface IAccountServices
    {
        string GenerateJwt(LoginDto dto);
        void RegisterUser(RegisterUserDto dto);
    }
}