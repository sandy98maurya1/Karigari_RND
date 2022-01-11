using System;
using System.Collections.Generic;
using System.Text;

namespace Utility.Enums
{
    public enum AppSettings
    {
        ITDeptContactNo = 1,
        EmailIdForAlert = 2,
        ImageSearchDuration = 3,
        ScreenTimeoutMins = 4,
        InvalidLoginAttemptCount = 5,
        DurationFor2FA = 6,
        ApplicationLogo = 7,
        CompanyLogo = 8,
        UserGuidDoc = 9,
        OTPValidMins = 10,
        PasswordValidDays = 11,
        TemporaryPasswordValidHours = 12,
        DefaultTemporaryPasswordSize = 13,
        DefaultPageSize = 14,
        EmailNameForAlertEmail = 15,
        CacheDurationInHours = 16,
        OwinTokenValidDurationInDays = 17,
        EmailAPIKey = 18,
        BlobContainer = 19,
        BlobThumbnailContainer = 20,
        BlobAccountName = 21,
        BlobAccesskey = 22,
        BlobBaseUrl = 23,
        BlobConnectionString = 24,
        TwilioAccountSId = 25,
        TwilioAuthToken = 26,
        TwilioFromNo = 27,
        TwilioCountryCode = 28
    }

    public enum UserType
    {
        Employee = 1,
        Employer = 2
    }
}
