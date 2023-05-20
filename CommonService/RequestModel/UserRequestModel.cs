using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonService.RequestModel
{
    public class UserRequestModel
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "Role is requred")]
        public int RoleId { get; set; }
        [Required(ErrorMessage = "Name is requred")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Surname is requred")]
        public string Surname { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        [Required(ErrorMessage = "EmailAddress is requred")]
        public string EmailAddress { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public string EmailConfirmationCode { get; set; }
        public string PasswordResetCode { get; set; }
        public DateTime? LastLoginTime { get; set; }
        public bool IsDeleted { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
        public bool? IsActive { get; set; }
        public bool ShouldChangePasswordOnNextLogin { get; set; }
        public Guid? ProfilePictureId { get; set; }
        public string AuthenticationSource { get; set; }
        public string Mobile { get; set; }
        public string CountryCode { get; set; }
        public string Designation { get; set; }
    }
}
