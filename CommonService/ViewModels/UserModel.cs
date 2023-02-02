using System;
using System.Collections.Generic;
using System.Text;

namespace CommonService.ViewModels
{
    public class UserModel
    {
        public long Id { get; set; }
        public int RoleId { get; set; }
        public string Rolename { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string EmailAddress { get; set; }
        public string Username { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public DateTime? LastLoginTime { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public string Mobile { get; set; }
    }
    public class UserListModel
    {
        public int Count { get; set; }
        public List<UserModel> UserList { get; set; }
    }

}
