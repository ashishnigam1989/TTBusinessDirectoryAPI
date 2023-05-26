using System;
using System.Collections.Generic;
using System.Text;

namespace CommonService.ViewModels
{
    public class LoginResponseModel
    {
        public UserModel User { get; set; }
        public string JwtAccessToken { get; set; }
        public string JwtRefreshToken { get; set; }

    }
}
