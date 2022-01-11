using Contracts.DataContracts;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class AccountsData : IAccountsData
    {
        private readonly ApplicationDbContext _dbcontext;
       
        public AccountsData(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public OTPModel GetOtpFromDb(string request)
        {
            try
            {
                return _dbcontext.OTPModel.FirstOrDefault(x => x.OTPForUser == request);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool SaveOTP(OTPModel Otp)
        {
            try
            {
                var existingOtp = _dbcontext.OTPModel.FirstOrDefault(o => o.OTPForUser == Otp.OTPForUser);
                if (existingOtp != null)
                {
                    existingOtp.OTPForUser = Otp.OTPForUser;
                    existingOtp.OTP = Otp.OTP;
                    existingOtp.OTPValidFrom = Otp.OTPValidFrom;
                    existingOtp.OTPValidTo = Otp.OTPValidTo;

                    _dbcontext.OTPModel.Update(existingOtp);
                }
                else
                {
                    _dbcontext.OTPModel.Add(Otp);
                }

                _dbcontext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
