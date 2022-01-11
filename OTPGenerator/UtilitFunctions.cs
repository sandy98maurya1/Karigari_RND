using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OTPGenerator
{
    public class UtilitFunctions
    {
        public static int GenerateRandomNo(int minVal = 100000, int maxVal = 999999)
        {
            Random random = new Random();
            return random.Next(minVal, maxVal);
        }

        public static string RandomString(int size, bool isLowerCase = false, bool isAlphaNumeric = false)
        {
            Random random = new Random();
            string chars = Constatns.AlphaChars;

            if (isAlphaNumeric)
            {
                chars = Constatns.AlphaNumericChars;
            }

            string randomString = new string(Enumerable.Repeat(chars, size)
                                 .Select(s => s[random.Next(s.Length)]).ToArray());

            randomString = isLowerCase ? randomString.ToLower() : randomString;

            return randomString;
        }
    }
}
