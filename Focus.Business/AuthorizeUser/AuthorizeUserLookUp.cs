using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.AuthorizeUser
{
    public class AuthorizeUserLookUp
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool ChangePriceDuringSale { get; set; }
        public bool GiveDiscountDuringSale { get; set; }
        public bool IsLoginFail { get; set; }
    }
}
