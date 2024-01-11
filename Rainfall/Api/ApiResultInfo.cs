using Microsoft.AspNetCore.Http.Features;

namespace Rainfall.Api
{
    public class ApiResultInfo
    {
        public ApiResultInfo()
        {
            items = new ApiResultDetail[] { };
        }
        public ApiResultDetail[] items { get; set; }
    }

    public class ApiResultDetail
    {
        public string @id { get; set; }

        public string dateTime { get; set; }

        public string measure { get; set; }

        public double value { get; set; }
    }
}
