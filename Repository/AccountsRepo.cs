using Contracts;
using Contracts.DataContracts;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public class AccountsRepo : IAccounts
    {
        private readonly IAccountsData _accounts;

        public AccountsRepo(IAccountsData accounts)
        {
            _accounts = accounts;
        }
        public OTPModel GetOtpFromDb(string request)
        {
            return _accounts.GetOtpFromDb(request);
        }

        public bool SaveOTP(OTPModel Otp)
        {
            return _accounts.SaveOTP(Otp);
        }
    }
}
