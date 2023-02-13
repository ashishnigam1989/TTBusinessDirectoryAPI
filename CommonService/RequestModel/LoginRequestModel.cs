using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonService.RequestModel
{
    public class LoginRequestModel
    {
        [Required(ErrorMessage = "EmailAddress is requred")]
        public string EmailAddress { get; set; }
        [Required(ErrorMessage = "Password is requred")]
        public string Password { get; set; }
    }
}
