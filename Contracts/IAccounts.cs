using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface IAccounts
    {
        public OTPModel GetOtpFromDb(string request);
        public bool SaveOTP(OTPModel Otp);
    }
}
