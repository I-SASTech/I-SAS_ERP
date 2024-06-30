using System;
using System.Collections.Generic;

namespace Focus.Business.BranchUsers.Models
{
    public class BranchUserModel
    {
        public Guid Id { get; set; }
        public List<BranchModel> BranchId { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
    }

    public class BranchModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
