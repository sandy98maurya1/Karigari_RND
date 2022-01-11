using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OTPGenerator
{
    public static class EmailOTP
    {
        public static string GenerateEmailOTP(string Email)
        {
            return UtilitFunctions.RandomString(6,false,true);
        }

        
    }
}
