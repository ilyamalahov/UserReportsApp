using System.Collections.Generic;

namespace UserReportsApp.Api
{
    public class CorsPolicyOptions
    {
        public string[] Origins { get; set; }
        public string[] Methods { get; set; }
        public string[] Headers { get; set; }
        public bool AllowCredentials { get; set; }
    }
}
