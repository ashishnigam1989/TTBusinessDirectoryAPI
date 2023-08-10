using System;
using System.Collections.Generic;

namespace DatabaseService.DbEntities
{
    public partial class UserSocialLogins
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string LoginProvider { get; set; }
        public string ProviderKey { get; set; }
    }
}
