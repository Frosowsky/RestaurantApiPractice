using WebApplication3.Models;

namespace WebApplication3.Services
{
    public interface IAccountServices
    {
        void RegisterUser(RegisterUserDto dto);
    }
}