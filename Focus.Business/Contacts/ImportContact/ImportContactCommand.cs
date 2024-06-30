using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CsvHelper;
using Dapper;
using Focus.Business.Interface;
using Focus.Business.StockAdjustments.ImportStock;
using Focus.Business.SyncRecords.Commands;
using Focus.Domain.Entities;
using Focus.Domain.Enum;
using Focus.Domain.Interface;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Syncfusion.XlsIO;

namespace Focus.Business.Contacts.ImportContact
{
    public class ImportContactCommand : IRequest<List<ImportContactLookUp>>
    {
        // delegate int NumberChanger(int n);
        public List<ImportContactLookUp> ContactLookUps { get; set; }
        public bool IsCustomer { get; set; }

        public class Handler : IRequestHandler<ImportContactCommand, List<ImportContactLookUp>>
        {
            public readonly IApplicationDbContext Context;
            public readonly ILogger Logger;
            private readonly IMediator _mediator;
            private readonly IConfiguration _configuration;
            private readonly IUserHttpContextProvider _contextProvider;
            private readonly IHostingEnvironment _environment;
            private string _adjustmentCode = null;
            private string _code;
            public Handler(IApplicationDbContext context, ILogger<ImportContactCommand> logger,
                IMediator mediator, IConfiguration configuration, IUserHttpContextProvider contextProvider,
                IHostingEnvironment environment)
            {
                Context = context;
                Logger = logger;
                _mediator = mediator;
                _configuration = configuration;
                _contextProvider = contextProvider;
                _environment = environment;
            }

            public async Task<List<ImportContactLookUp>> Handle(ImportContactCommand request, CancellationToken cancellationToken)
            {


                try
                {
                    var companyId = _contextProvider.GetCompanyId().ToString();
                    var sqlQuery =
                        "select * from Contacts where CompanyId = '" +
                        companyId + "';" +
                        " select  a.Code as AccountCode, a.CostCenterId as CostCenterId, c.code as CostCenterCode from Accounts as a  inner join CostCenters as c on a.CostCenterId = c.Id  where a.CompanyId ='" +
                        companyId + "';";

                    var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
                    using (var multi = connection.QueryMultiple(sqlQuery, null))
                    {
                        DropDownCodeModel.ContactCode = multi.Read<Contact>().ToList();
                        DropDownCodeModel.AccountsDataForCategories = multi.Read<AccountsDataForCategory>().ToList();
                    }

                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }


                    // Get Last Code of all drop down from upper list
                    var totalCust = DropDownCodeModel.ContactCode.Count > 0 ? DropDownCodeModel.ContactCode.OrderBy(x => x.IsCustomer == request.IsCustomer).LastOrDefault() :(Contact)null;
                    var lastCode = totalCust != null ? totalCust.Code : String.Empty;
                    var accountInfo = request.IsCustomer ?
                        DropDownCodeModel.AccountsDataForCategories.OrderBy(x => x.AccountCode).LastOrDefault(x => x.CostCenterCode == "120000") :
                        DropDownCodeModel.AccountsDataForCategories.OrderBy(x => x.AccountCode).LastOrDefault(x => x.CostCenterCode == "200000");

                    var acccountCode = accountInfo.AccountCode;


                    var errorContactFileDataList = new List<ImportContactLookUp>();
                    foreach (var contact in request.ContactLookUps)
                    {
                        if (!string.IsNullOrEmpty(contact.EnglishName) ||
                            !string.IsNullOrEmpty(contact.ArabicName))
                        {
                            var contactObj = (Contact)null;
                            var contactLangName = string.IsNullOrEmpty(contact.EnglishName)
                                ? "Arabic" : "English";
                            if (contactLangName == "English")
                            {
                                contactObj = DropDownCodeModel.ContactCode.FirstOrDefault(x =>
                                    x.EnglishName == contact.EnglishName && x.IsCustomer == request.IsCustomer);

                            }
                            else
                            {
                                contactObj = DropDownCodeModel.ContactCode.FirstOrDefault(x =>
                                    x.ArabicName == contact.ArabicName && x.IsCustomer == request.IsCustomer);

                            }

                            if (contactObj == null)
                            {
                                var account = new Account
                                {
                                    Name = contact.EnglishName == "" ? contact.ArabicName : contact.EnglishName,
                                    Description = contact.ArabicName + ' ' + contact.EnglishName,
                                    Code = (int.Parse(acccountCode) + 1).ToString(),
                                    CostCenterId = accountInfo.CostCenterId,
                                    NameArabic = contact.ArabicName,
                                    IsActive = true
                                };
                                await Context.Accounts.AddAsync(account, cancellationToken);
                                acccountCode = account.Code;


                                var contactData = new Contact
                                {
                                    Code = string.IsNullOrEmpty(lastCode) ? GenerateCodeFirstTime(request.IsCustomer) : GenerateNewCode(lastCode, request.IsCustomer),
                                    Category = contact.Category,
                                    EnglishName = contact.EnglishName,
                                    ArabicName = contact.ArabicName,
                                    CommercialRegistrationNo = contact.CommercialRegistrationNo,
                                    VatNo = contact.VatNo,
                                    ContactPerson1 = contact.ContactPerson1,
                                    ContactPerson2 = contact.ContactPerson2,
                                    ContactNo1 = contact.ContactNo1,
                                    Address = contact.Address,
                                    City = contact.City,
                                    AccountId = account.Id,
                                    Remarks = contact.Remarks,
                                    Country = contact.Country,
                                    IsActive = true,
                                    IsCustomer = request.IsCustomer,
                                    Telephone = contact.Telephone,
                                    Website = contact.Website,
                                    IsRaw = contact.IsRaw == "True".ToLower() || contact.IsRaw == "1",

                                };
                                if (request.IsCustomer)
                                {
                                    contactData.CustomerType = string.IsNullOrEmpty(contact.CustomerType)
                                       ? ""
                                       : Convert.ToInt32(contact.CustomerType) == 1
                                           ? "Individual"
                                           : Convert.ToInt32(contact.CustomerType) == 2
                                               ? "Establishment"
                                               : Convert.ToInt32(contact.CustomerType) == 3
                                                   ? "Company"
                                                   : "";

                                }
                                else
                                {
                                    contactData.SupplierType = (SupplierType?)Enum.Parse(typeof(SupplierType),
                                        string.IsNullOrEmpty(contact.CustomerType) ? "0" : contact.CustomerType, true);
                                }


                                await Context.Contacts.AddAsync(contactData, cancellationToken);

                                //var bankAccount = new ContactBankAccount
                                //{
                                //    ContactId = contactData.Id,
                                //    BankAccountTitle1 = String.Empty,
                                //    BankAccountNo1 = String.Empty,
                                //    NameOfBank1 = String.Empty,
                                //    RoutingCode1 = String.Empty,
                                //    City1 = String.Empty,
                                //    IBAN1 = String.Empty,
                                //    BranchName1 = String.Empty,
                                //    Address1 = String.Empty,
                                //    Country1 = String.Empty,

                                //    BankAccountTitle2 = String.Empty,
                                //    BankAccountNo2 = String.Empty,
                                //    NameOfBank2 = String.Empty,
                                //    RoutingCode2 = String.Empty,
                                //    City2 = String.Empty,
                                //    IBAN2 = String.Empty,
                                //    BranchName2 = String.Empty,
                                //    Address2 = String.Empty,
                                //    Country2 = String.Empty,


                                //};
                                //await Context.ContactBankAccounts.AddAsync(bankAccount, cancellationToken);
                                lastCode = contactData.Code;
                            }


                        }
                        else
                        {

                            contact.ErrorDescription = "English or Arabic name of contact is required";
                            errorContactFileDataList.Add(contact);
                        }
                    }

                    await _mediator.Send(new AddUpdateSyncRecordCommand()
                    {
                        SyncRecordModels = Context.SyncLog(),
                        Code = _code,
                    }, cancellationToken);


                    await Context.SaveChangesAsync(cancellationToken);
                    return errorContactFileDataList;
                }
                catch (Exception e)
                {
                    throw new ApplicationException("Something went wrong. Please contact to support");

                }



            }


            public string GenerateCodeFirstTime(bool isCustomer)
            {
                if (isCustomer)
                {
                    return "CU-00001";
                }
                return "SU-00001";
            }

            public string GenerateNewCode(string soNumber, bool isCustomer)
            {
                string fetchNo = soNumber.Substring(3);
                Int32 autoNo = Convert.ToInt32((fetchNo));
                var format = "00000";
                autoNo++;
                var newCode = isCustomer? "CU-" + autoNo.ToString(format) : "SU-" + autoNo.ToString(format);
                return newCode;

            }

        }
    }

    public class AccountsDataForCategory
    {
        public string AccountCode { get; set; }
        public string CostCenterCode { get; set; }
        public Guid CostCenterId { get; set; }
    }
    public static class DropDownCodeModel
    {
        public static ICollection<Contact> ContactCode { get; set; }
        
        public static ICollection<AccountsDataForCategory> AccountsDataForCategories { get; set; }
    }

}
