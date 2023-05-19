using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DatabaseService.DbEntities
{
    public partial class Users
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string CountryCode { get; set; }
        public string Mobile { get; set; }
        public string EmailAddress { get; set; }
        public string Designation { get; set; }
        public string Password { get; set; }
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
    }
}
