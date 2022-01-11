using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Runtime;
using Newtonsoft.Json;
using System.Net.Http;
using Utility;
using System.Text;
using System.Runtime.Caching;
using Utility.Enums;
using Microsoft.AspNetCore.Identity;


namespace Karigari.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly ILabour _userLabour;
        private readonly ICompanys _userEmployer;
        private readonly IAccounts _accounts;
        private readonly Models.AppSettings _appSettings;
        

        public AccountsController(ILabour userEmployee, ICompanys userEmployer, Models.AppSettings appSettings,
                                    IAccounts accounts)
        {
            _userLabour = userEmployee;
            _userEmployer = userEmployer;
            _accounts = accounts;
            _appSettings = appSettings;
           
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tokenRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("GetToken")]
        [AllowAnonymous]
        public async Task<IActionResult> GetToken([FromBody]TokenRequest tokenRequest)
        {
            TokenResponse tokenResponse = new TokenResponse();
            ApiExposeResponse<Dictionary<string, string>> modelErrors = GetModelErrors(tokenRequest);
            dynamic userObj;
            if (modelErrors.Error.Count() == 0)
            {
                
                if (tokenRequest.UserType == UserType.Employee.ToString()) 
                    userObj = ValidateEmployee(tokenRequest);
                else
                    userObj = ValidateEmployer(tokenRequest);

                if (userObj == null)
                {
                    tokenResponse = new TokenResponse();
                    tokenResponse.IsSuccess = false;
                    tokenResponse.Error = ErrorMessages.LoginFail;
                    tokenResponse.Error_description = ErrorMessages.LoginFail;
                }
                else
                {
                    tokenResponse = GenerateToke(userObj, tokenRequest.UserType);
                    if (string.IsNullOrEmpty(tokenResponse.Error))
                    {
                        var cache = MemoryCache.Default;

                        tokenResponse.IsSuccess = true;
                        tokenResponse.LoginId = tokenRequest.Email;
                        tokenResponse.UserName = userObj.FirstName + " " + userObj.LastName;
                    }
                    GlobalUser.Id = userObj.Id;
                    GlobalUser.FirstName = userObj.FirstName;
                    GlobalUser.LastName = userObj.LastName;
                    GlobalUser.Contact = userObj.Contact;
                }
            }
            else
            {
                tokenResponse = new TokenResponse();
                tokenResponse.IsSuccess = false;
                tokenResponse.Error = ErrorMessages.UserOrPasswordEmpty;
                tokenResponse.Error_description = ErrorMessages.UserOrPasswordEmpty;
            }

            return Ok(tokenResponse);
        }


        /// <summary>
        /// This function Generates token return the token response
        /// </summary>
        /// <param name="userObj"></param>
        /// <returns></returns>
        private TokenResponse GenerateToke(dynamic userObj,string UserType)
        {
            TokenResponse tokenResponse = new TokenResponse();

            var token = GetAccessToken(userObj, UserType);

            tokenResponse.Access_token = token;
            tokenResponse.AccessRights = new int[] { 1, 2 };//(int)userObj.Role;
            tokenResponse.Expires = DateTime.Now;
            tokenResponse.TimeOutMins = (int)Utility.Enums.AppSettings.ScreenTimeoutMins;

            //userObj.WithoutPassword();

            return tokenResponse;
        }
        

        /// <summary>
        /// This function generates access token with claim it returns jwt token
        /// </summary>
        /// <param name="userObj"></param>
        /// <returns></returns>
        private string GetAccessToken(dynamic userObj , string UserType)
        {
            var key = Encoding.UTF8.GetBytes(_appSettings.Secret);
            var securityKey = new SymmetricSecurityKey(key);
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, _appSettings.Audiance),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim(Constants.ClaimTypeId,userObj.Id.ToString()),
                new Claim(Constants.ClaimTypeFirstName, userObj.FirstName),
                new Claim(Constants.ClaimTypeLastName, userObj.LastName),                
                new Claim(ClaimTypes.Role, userObj.Role.ToUpper())

            };

            if (UserType == Utility.Enums.UserType.Employee.ToString())
                claims.Append(new Claim(Constants.ClaimContact, userObj.Contact));

            if (UserType == Utility.Enums.UserType.Employer.ToString())
                claims.Append(new Claim(Constants.ClaimTypeEmail, userObj.Email));

            var token = new JwtSecurityToken(
                    _appSettings.Issuer,
                    _appSettings.Issuer,
                    claims,
                    null,
                    expires: DateTime.Now.AddMinutes(120),
                    signingCredentials: credentials
              );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }


        /// <summary>
        /// ValidateUser: used to validate user and check login attempts of user. if attempts more then max attempts user account will locked.
        /// </summary>
        /// <param name="userObj"></param>
        /// <returns></returns>
        private Labour ValidateEmployee(TokenRequest request)
        {
            Labour userObj = _userLabour.GetLabourByContact(request.Contact);
            request.Password = SHA256Cryptography.GetHashString(request.Password);

            if (userObj != null && (request.Contact.Equals(userObj.Contact) && request.Password.Equals(userObj.Password)))
                return userObj;
            
            return userObj;
        }

       
        /// <summary>
        /// Validates employer
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private Models.Company ValidateEmployer(TokenRequest request)
        {
            Models.Company userObj = _userEmployer.GetCompanyByEmail(request.Email);
            request.Password = SHA256Cryptography.GetHashString(request.Password);

            if (userObj != null && (request.Email.Equals(userObj.BusinessEmail) && request.Password.Equals(userObj.Password)))
                return userObj;
            else if (userObj != null && (request.Email.Equals(userObj.BusinessEmail)))
                return userObj;

            
            return userObj;
        }

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false, //you might want to validate the audience and issuer depending on your use case
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.Secret)),
                ValidateLifetime = false //here we are saying that we don't care about the token's expiration date
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="refreshTokenRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("GetRefreshToken")]
        [Authorize(Roles = Roles.EMPLOYEE +","+ Roles.EMPLOYER)]
        public async Task<IActionResult> Refresh(RefreshTokenRequest refreshTokenRequest)
        {
            TokenResponse tokenResponse = new TokenResponse();
            var principal = GetPrincipalFromExpiredToken(refreshTokenRequest.token);
            var username = principal.Identity.Name; //this is mapped to the Name claim by default
            var email = principal.Claims.FirstOrDefault(c => c.Type == "Email")?.Value;

            var user = _userLabour.GetLabourByContact(email);
            if (user == null || user.RefreshToken != refreshTokenRequest.refreshToken) return BadRequest();

            var newJwtToken = GetAccessToken(user, Utility.Enums.UserType.Employee.ToString());
            var newRefreshToken = Utility.CommonUitilityFunctions.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            _userLabour.UpdateLabour(user);

            tokenResponse.Access_token = newJwtToken;
            tokenResponse.RefreshToken = refreshTokenRequest.refreshToken;

            return Ok(tokenResponse);
        }

        [HttpPost, Route("SendSMSOTP")]
        public IActionResult GenerateOTP(TokenRequest request)
        {
            OTPResponse response = new OTPResponse();
            bool isValidateEmployee = (_userLabour.GetLabourByContact(request.Contact)) != null ? true : false;
            bool isValidateOTP = ValidateOTP(request);

            if ((!isValidateOTP && !isValidateEmployee) || (!isValidateOTP && isValidateEmployee))
            {
                string otp = OTPGenerator.SMSOTP.SendSMSOTP(request.Contact);
                _accounts.SaveOTP(new OTPModel()
                {
                    OTP = otp,
                    OTPForUser = request.Contact,
                    OTPValidFrom = DateTime.Now,
                    OTPValidTo = DateTime.Now.AddMinutes(Convert.ToInt32(Utility.Enums.AppSettings.OTPValidMins))
                });
                response.OTP = otp;
                response.IsOTP = true;
            }

            response.IsUserProfile = (isValidateOTP && isValidateEmployee) ? true : false;
            response.IsRegistrationForm = (isValidateOTP && !isValidateEmployee) ? true : false;


            return Ok(response);
        }

        [HttpPost, Route("SendEmailOTP")]
        public IActionResult GenerateEmailOTP(TokenRequest request)
        {
            OTPResponse response = new OTPResponse();
            bool isValidateEmployer = (_userEmployer.GetCompanyByEmail(request.Email)) != null ? true : false ;
            bool isValidateOTP = ValidateOTP(request);

            if ((!isValidateOTP && !isValidateEmployer) || (!isValidateOTP && isValidateEmployer))
            {
                var otp = OTPGenerator.EmailOTP.GenerateEmailOTP(request.Email);
                //send email notifiction
                
                _accounts.SaveOTP(new OTPModel()
                {
                    OTP = otp,
                    OTPForUser = request.Email,
                    OTPValidFrom = DateTime.Now,
                    OTPValidTo = DateTime.Now.AddMinutes(Convert.ToInt32(Utility.Enums.AppSettings.OTPValidMins))
                });
                response.OTP = otp;
                response.IsOTP = true;
            }

            response.IsUserProfile = (isValidateOTP && isValidateEmployer) ? true : false;
            response.IsRegistrationForm = (isValidateOTP && !isValidateEmployer) ? true : false;
            

            return Ok(response);
        }

        [HttpPost, Route("VerifyOTP")]
        public IActionResult VerifyOTP(TokenRequest request)
        {
            OTPResponse response = new OTPResponse();
            bool isValidateEmployer = false;
            bool isValidateEmployee = false;

            if (request.UserType == Utility.Enums.UserType.Employer.ToString())
                isValidateEmployer = (_userEmployer.GetCompanyByEmail(request.Email)) != null ? true : false;
            else if (request.UserType == Utility.Enums.UserType.Employee.ToString())
                isValidateEmployee = (_userLabour.GetLabourByContact(request.Contact)) != null ? true : false;

            bool isValidateOTP = ValidateOTP(request);
            if (isValidateOTP)
            {
                if (request.UserType == Utility.Enums.UserType.Employer.ToString())
                {
                    response.IsUserProfile = (isValidateOTP && isValidateEmployer) ? true : false;
                    response.IsRegistrationForm = (isValidateOTP && !isValidateEmployer) ? true : false;
                }
                else if (request.UserType == Utility.Enums.UserType.Employee.ToString())
                {
                    response.IsUserProfile = (isValidateOTP && isValidateEmployee) ? true : false;
                    response.IsRegistrationForm = (isValidateOTP && !isValidateEmployee) ? true : false;
                }
            }
            else
            {
                response.Message = string.Format(ErrorMessages.OTPExpired);
            }
            return Ok(response);
        }

        private bool ValidateOTP(TokenRequest request)
        {
           
            bool validateOtp = false;
            OTPModel otp = null;

            otp =  !string.IsNullOrEmpty(request.Contact) ? 
                    _accounts.GetOtpFromDb(request.Contact) : 
                    _accounts.GetOtpFromDb(request.Email);
            
            if(otp != null)
                validateOtp = (  DateTime.Now > otp.OTPValidFrom && DateTime.Now < otp.OTPValidTo) ? true : false;

            if(otp == null)
                validateOtp = (string.IsNullOrEmpty(request.OTP) && string.IsNullOrEmpty(request.Password)) ? false : true;

            return validateOtp;
        }

        private ApiExposeResponse<Dictionary<string, string>> GetModelErrors<T>(T Model)
        {
            Dictionary<string, string> errors = new Dictionary<string, string>();
            if (!ModelState.IsValid)
            {
                foreach (var state in ModelState)
                {
                    string errordetails = string.Empty;
                    foreach (var error in state.Value.Errors)
                    {
                        errordetails = errordetails + error.ErrorMessage;
                    }

                    errors.Add(state.Key.Contains(".") ? state.Key.Split('.')[1] : state.Key, errordetails);
                }
            }

            return new ApiExposeResponse<Dictionary<string, string>>
            {
                IsSuccess = false,
                Message = ErrorMessages.InValidInputMsg,
                Error = errors
            };
        }

    }

   
}