using System;
using System.Collections.Generic;
using System.Text;

namespace OTPGenerator
{
    public static class SMSOTP
    {
        public static string SendSMSOTP(string contact)
        {
            try
            {
                string otp = UtilitFunctions.GenerateRandomNo(101010,909090).ToString();

                // Find your Account Sid and Auth Token at twilio.com/user/account  
                const string accountSid = "AXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
                const string authToken = "6XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
                //TwilioClient.Init(accountSid, authToken);

                //var to = new PhoneNumber("+91" + contact);
                //var message = MessageResource.Create(
                //    to,
                //    from: new PhoneNumber("+18XXXXXXXXXX"), //  From number, must be an SMS-enabled Twilio number ( This will send sms from ur "To" numbers ).  
                //    body: $"Hello {model.Name} !! Welcome to Asp.Net Core With Twilio SMS API !!");
                return otp;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
