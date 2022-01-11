using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; } 
        public string LastName { get; set; }  
        public string Contact { get; set; } 
        public string Email { get; set; } 
        public string Password { get; set; } 
        public string Address { get; set; } 
        public string Role { get; set; } 
        public int? LoginCount { get; set; } 
        public bool? IsLocked { get; set; } 
        public bool? IsPasswordTemporary { get; set; } 
        public string TemporaryPassword { get; set; }        
        public bool IsActive { get; set; }  
        public string? RefreshToken { get; set; } 

    }
}
