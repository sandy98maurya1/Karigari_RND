using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models
{
    public class AccountModel
    {
    }

    public class TokenRequest
    {
       
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Contact { get; set; }
        public string? UserType { get; set; }
        public string? OTP { get; set; }
    }

    public class RefreshTokenRequest
    {
        public string token { get; set; }
        public string refreshToken { get; set; }

    }

    public class TokenResponse
    {
        public bool IsSuccess { get; set; }
        public string Access_token { get; set; }
        public string Token_type { get; set; }
        public int Expires_in { get; set; }
        public string UserName { get; set; }
        public DateTime Issued { get; set; }
        public DateTime Expires { get; set; }
        public string Error { get; set; }
        public string Error_description { get; set; }
        public bool Is2FAEnabled { get; set; }
        public bool IsAccountLock { get; set; }
        public int[] AccessRights { get; set; }
        public int InvalidLoginAttempt { get; set; }
        public string LoginId { get; set; }
        public bool IsPasswordTemporary { get; set; }
        public int ScreenTimeOutMins { get; set; }
        public string UserGuidDocumentUrl { get; set; }
        public int TimeOutMins { get; set; }
        public string RefreshToken { get; set; }
    }

    public class OTPResponse
    {
        public string OTP { get; set; }
        public bool IsUserProfile { get; set; }
        public bool IsOTP { get; set; }
        public bool IsRegistrationForm { get; set; }
        public string Message { get; set; }
    }
}
