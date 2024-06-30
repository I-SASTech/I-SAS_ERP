using Focus.Domain.Entities;
using Focus.Domain.Enum;
using System;
using System.Collections.Generic;

namespace Focus.Business.CompanyLicences.Queries.GetCompanyLicenceList
{
    public class GetCompanyLicenceListLookupModel
    {
        public Guid? Id { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public bool IsBlock { get; set; }
        public bool IsActive { get; set; }
        public int NumberOfUsers { get; set; }
        public int NumberOfTransactions { get; set; }
        public Guid CompanyId { get; set; }
        public CompanyType CompanyType { get; set; }

        internal static List<GetCompanyLicenceListLookupModel> Create(List<CompanyLicence> companyLicences)
        {
            var licences = new List<GetCompanyLicenceListLookupModel>();
            foreach (var licence in companyLicences)
            {
             licences.Add(new GetCompanyLicenceListLookupModel()
             {
                 Id = licence.Id,
                 CompanyId = licence.CompanyId,
                 CompanyType = licence.CompanyType,
                 FromDate = licence.FromDate,
                 ToDate = licence.ToDate,
                 NumberOfUsers = licence.NumberOfUsers,
                 NumberOfTransactions = (int)licence.NoOfTransactionsAllow,
                 IsActive = licence.IsActive,
                 IsBlock = licence.IsBlock
             });   
            }

            return licences;
        }
    }
}
