using WebApplication3.Entitie;
using WebApplication3.Models;

namespace WebApplication3.Services
{
    public class AccountServices : IAccountServices


    {
        private readonly RestaurantDbContext _context;

        public AccountServices(RestaurantDbContext context)
        {
            _context = context;
        }
        public void RegisterUser(RegisterUserDto dto)
        {
            var newUser = new User()
            {
                Email = dto.Email,
                DateOfBirth = dto.DateOfBirth,
                Nationality = dto.Nationality,
                RoleId = dto.RoleId,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                PasswordHash = dto.Password

            };


            _context.Users.Add(newUser);
            _context.SaveChanges();


        }

    }
}
