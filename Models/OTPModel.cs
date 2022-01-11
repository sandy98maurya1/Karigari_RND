using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    /// <summary>
    /// the logic behind this class is, to register otp in table and after otp expiration time, or at the time of validate otp
    /// it requires data from this table. so CRUD operation will perform on this class
    /// </summary>
    public class OTPModel
    {
        public int Id { get; set; }
        public string OTPForUser { get; set; }
        public string OTP { get; set; } 
        public DateTime OTPValidFrom { get; set; }
        public DateTime OTPValidTo { get; set; }
    }
}
