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
        public static string BrandLogo = "Content/Brand/{0}/Logo/";
        public static string ProductLogo = "Content/Product/{0}/Logo/";
        public static string ServiceLogo = "Content/Service/{0}/Logo/";
        public static string BannerEng = "Content/Banners/{0}/Eng/";
        public static string BannerArb = "Content/Banners/{0}/Arb/";
        public static string GalleryImage = "Content/Gallery/{0}/Image/";
        public static string GalleryFile = "Content/Gallery/{0}/File/";
        public static string S3BaseURL { get; set; } = "http://taazatadka.s3.ap-south-1.amazonaws.com/";
    }
}

