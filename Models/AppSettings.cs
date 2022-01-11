using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class AppSettings
    {
        public string Secret { get; set; } = "9STM5IhCQReO5ZdEUNfAJOQZAtt19uiDhNtKKUt";
        public string Issuer { get; set; } = "https://localhost:44378/";
        public string Audiance { get; set; } = "KarigariPostmanClient";

    }
}
