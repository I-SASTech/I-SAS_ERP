using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.LoginHistories.Queries.GetLoginHistoryList
{
    public class LoginHistoryModel
    {
        public string UserName { get; set; }
        public string LoginDate { get; set; }
        public string LogoutDate { get; set; }
        public string LoginTime { get; set; }
        public string LogoutTime { get; set; }
        public string UserRole { get; set; }
    }
}
