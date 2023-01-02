using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace WebApplication3.Models
{
    public class RegisterUserDto
    {
    
       public string Email { get; set; }
  
        public string? FirstName { get; set; }
        
        public string? LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Nationality { get; set; }

        public string ConfirmedPassword { get; set; }

        public string Password { get; set; }

        public int RoleId { get; set; } = 1;
    }
}
