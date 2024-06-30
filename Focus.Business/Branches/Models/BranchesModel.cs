using System;

namespace Focus.Business.Branches.Models
{
    public class BranchesModel
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string BranchName { get; set; }
        public bool IsCentralized { get; set; }
        public bool IsApproval { get; set; }
        public bool IsOnline { get; set; }
        public bool MainBranch { get; set; }
        public bool IsActive { get; set; }
        public string ContactNo { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string LocationName { get; set; }
        public Guid LocationId { get; set; }
        public Guid BusinessId { get; set; }
        public string BranchType { get;  set; }
    }
}
