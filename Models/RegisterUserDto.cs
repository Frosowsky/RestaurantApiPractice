﻿using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace WebApplication3.Models
{
    public class RegisterUserDto
    {
        [Required]
       public string Email { get; set; }
        [AllowNull]
        public string FirstName { get; set; }
        [AllowNull]
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Nationality { get; set; }
        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        public int RoleId { get; set; } = 1;
    }
}