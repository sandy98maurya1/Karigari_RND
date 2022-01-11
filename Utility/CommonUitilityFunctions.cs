using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Utility
{
    public static class CommonUitilityFunctions
    {
        public static Guid GenrateActivationKey()
        {
            return Guid.NewGuid();
        }

        public static string RandomString(int size, bool isLowerCase = false, bool isAlphaNumeric = false)
        {
            Random random = new Random();
            string chars = Constants.AlphaChars;

            if (isAlphaNumeric)
            {
                chars = Constants.AlphaNumericChars;
            }

            string randomString = new string(Enumerable.Repeat(chars, size)
                                 .Select(s => s[random.Next(s.Length)]).ToArray());

            randomString = isLowerCase ? randomString.ToLower() : randomString;

            return randomString;
        }

        public static string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }


    }
}
