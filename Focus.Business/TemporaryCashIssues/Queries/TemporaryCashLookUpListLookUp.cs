using System;

namespace Focus.Business.TemporaryCashIssues.Queries
{
    public class TemporaryCashLookUpListLookUp
    {
        public Guid Id { get; set; }
        public string RegistrationNo { get; set; }
        public string UserName { get; set; }
        public string Date { get; set; }
        public decimal Amount { get; set; }
    }
}
