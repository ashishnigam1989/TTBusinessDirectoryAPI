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
        public static string CategoryIcon = "Content/Category/{0}/Icon/";
        public static string S3BaseURL { get; set; } = "http://taazatadka.s3.ap-south-1.amazonaws.com/";
    }
}
