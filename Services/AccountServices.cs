using Microsoft.AspNetCore.Identity;
using WebApplication3.Entitie;
using WebApplication3.Models;

namespace WebApplication3.Services
{
    public class AccountServices : IAccountServices


    {
        private readonly RestaurantDbContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;

        public AccountServices(RestaurantDbContext context, IPasswordHasher<User> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }
        public void RegisterUser(RegisterUserDto dto)
        {
            var newUser = new User()
            {
                Email = dto.Email,
                DateOfBirth = dto.DateOfBirth,
                Nationality = dto.Nationality,
                RoleId = dto.RoleId,
                
               

            };

           var hashedPassword = _passwordHasher.HashPassword(newUser, dto.Password);
            newUser.PasswordHash = hashedPassword;
            _context.Users.Add(newUser);
            _context.SaveChanges();


        }

    }
}
