using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace CommonService.Constants
{
    public static class CommonConstants
    {
        public static string FileTempPath = "C:/TempImages/";
        public static string CompanyLogo = "Content/Company/{0}/Logo/";
        public static string S3BaseURL { get; set; } = "https://stagingtripoturf.s3.us-east-1.amazonaws.com/";
    }
}
