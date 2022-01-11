using Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models
{
    public class Labour
    {
        [Key]
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Contact { get; set; }
        public string? AlternetContact { get; set; }
        public string? MaritalStatus { get; set; }
        public string? Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public Address Address { get; set; }
        public string? Role { get; set; }
        public WorkerType WorkerType { get; set; }
        public bool IsActive { get; set; }
        public string? RefreshToken { get; set; }
        public string Password { get; set; }
        public bool IsMobileVerified { get; set; }
        public int? DeviceID { get; set; }
    }
}
