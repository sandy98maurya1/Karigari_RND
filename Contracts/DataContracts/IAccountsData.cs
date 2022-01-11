using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.DataContracts
{
    public interface IAccountsData
    {
        public OTPModel GetOtpFromDb(string request);
        public bool SaveOTP(OTPModel Otp);
    }
}
