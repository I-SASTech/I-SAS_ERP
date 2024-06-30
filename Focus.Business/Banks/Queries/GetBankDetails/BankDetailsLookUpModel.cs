using Focus.Domain.Entities;
using System;
using System.Linq.Expressions;

namespace Focus.Business.Banks.Queries.GetBankDetails
{
    public class BankDetailsLookUpModel
    {
        public Guid Id { get; set; }
        public string ShortName { get; set; }
        public string AccountType { get; set; }
        public string BankName { get; set; }
        public string NameArabic { get; set; }
        public string Code { get; set; }
        public string IsActive { get; set; }
        public bool Active { get; set; }
        public string AccoutName { get; set; }
        public string AccoutNameArabic { get; set; }

        public Guid AccountId { get; set; }
        public string BranchName { get; set; }
        public string AccountNumber { get; set; }
        public string IBNNumber { get; set; }
        public string Location { get; set; }
        public string ContactPerson { get; set; }
        public string ContactName { get; set; }
        public string ManagerName { get; set; }
        public string ManagerContectualNumber { get; set; }
        public string AccounType { get; set; }
        public Guid? CurrencyId { get; set; }
        public string BranchCode { get; set; }
        public string BranchAddress { get; set; }
        public string SwiftCode { get; set; }
        public string Reference { get; set; }


        public static Expression<Func<Bank, BankDetailsLookUpModel>> Projection
        {
            get
            {
                return bank => new BankDetailsLookUpModel
                {
                    Id = bank.Id,
                    BankName = bank.BankName,
                    AccountType = bank.AccountType,
                    ShortName = bank.ShortName,
                    NameArabic = bank.NameArabic,
                    Code = bank.Code,
                    IsActive = bank.IsActive,
                    Active = bank.Active,
                    BranchCode = bank.BranchCode,
                    BranchAddress = bank.BranchAddress,
                    SwiftCode = bank.SwiftCode,
                    AccoutName = bank.AccoutName,
                    AccoutNameArabic = bank.AccoutNameArabic,
                    AccountId = bank.AccountId,
                    BranchName = bank.BankName,
                    AccountNumber = bank.AccountNumber,
                    IBNNumber = bank.IBNNumber,
                    Location = bank.Location,
                    ContactPerson = bank.ContactPerson,
                    ContactName = bank.ContactName,
                    ManagerName = bank.ManagerName,
                    ManagerContectualNumber = bank.ManagerContectualNumber,
                    AccounType = bank.AccounType,
                    CurrencyId = bank.CurrencyId,
                    Reference = bank.Reference,
                };
            }
        }

        public static BankDetailsLookUpModel Create(Bank bank)
        {
            return Projection.Compile().Invoke(bank);
        }
    }
}
