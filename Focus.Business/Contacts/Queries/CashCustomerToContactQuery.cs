using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Office2010.Excel;
using Focus.Business.Common;
using Focus.Business.Interface;
using Focus.Business.Products.Commands.CreateProductsAccount;
using Focus.Business.SyncRecords.Commands;
using Focus.Domain.Entities;

using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.Contacts.Queries
{
    public class CashCustomerToContactQuery : IRequest<Message>
    {
        public bool ChangeDisplayName { get; set; } 
        public bool CashCustomerToContact { get; set; }
        public bool InvoiceChange { get; set; }
        public string DocumentType { get; set; }
        public class Handler : IRequestHandler<CashCustomerToContactQuery, Message>
        {
            public readonly IApplicationDbContext Context;
            public readonly ILogger Logger;
            public readonly IMediator _Mediator;
            private string _code;
            public Handler(IApplicationDbContext context, ILogger<CashCustomerToContactQuery> logger, IMediator mediator)
            {
                Context = context;
                Logger = logger;
                _Mediator = mediator;
            }
            public async Task<Message> Handle(CashCustomerToContactQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    if(request.DocumentType== "Invoice")
                    {
                        var salesList = await Context.Sales.AsNoTracking().ToListAsync();
                        if (salesList.Count > 0)
                        {
                            foreach (var sale in salesList.Where(x => x.InvoiceType == 0))
                            {
                                sale.DocumentType = (DocumentType)8;
                            }
                            foreach (var sale in salesList.Where(x => x.InvoiceType == (InvoiceType)2 || x.InvoiceType == (InvoiceType)1))
                            {
                                sale.DocumentType = (DocumentType)1;
                            }
                            foreach (var sale in salesList.Where(x => x.InvoiceType == (InvoiceType)3))
                            {
                                sale.DocumentType = (DocumentType)1;
                            }
                            Context.Sales.UpdateRange(salesList);
                        }
                    }
                    else
                    {
                        if (request.ChangeDisplayName)
                        {
                            var contactsList = await Context.Contacts.AsNoTracking().ToListAsync();
                            var contacts = new List<Focus.Domain.Entities.Contact>();
                            foreach (var contact in contactsList)
                            {
                                if (contact.CustomerDisplayName == null || contact.CustomerDisplayName == "")
                                {
                                    string engName = (contact.EnglishName == null && contact.EnglishName == "") ? "" : contact.EnglishName;
                                    string arName = (contact.ArabicName == null && contact.ArabicName == "") ? "" : contact.ArabicName;

                                    contact.CustomerDisplayName = $"{engName} {arName}";
                                    contacts.Add(contact);

                                }
                            }
                            if(contacts.Count>0)
                            {
                                Context.Contacts.UpdateRange(contacts);


                            }

                        }
                        if (request.CashCustomerToContact)
                        {
                            var checkCashCustomer = Context.Contacts.AsNoTracking().Any(x => x.EnglishName == "Walk-In");
                            if (!checkCashCustomer)
                            {
                                var account = new Account();
                                var contactForSave = new Contact();
                                var accountCount = Context.Accounts.Include(x => x.CostCenter).Where(x => x.CostCenter.Code == "120000").OrderBy(x => x.Code).LastOrDefault();

                                if (accountCount != null)
                                {
                                    account = new Account
                                    {
                                        Name = "Walk-In",
                                        Description = "WC-00001",
                                        Code = (int.Parse(accountCount.Code) + 1).ToString(),
                                        CostCenterId = accountCount.CostCenterId,
                                        IsActive = true
                                    };
                                    Context.Accounts.Add(account);
                                }
                                contactForSave = new Contact
                                {
                                    Code = "WC-00001",
                                    EnglishName = "Walk-In",
                                    CustomerDisplayName = "Walk-In",
                                    AccountId = account.Id,
                                    IsActive = true,
                                    IsCashCustomer = true,
                                    IsRaw = false,
                                    IsCustomer = true

                                };
                                Context.Contacts.Add(contactForSave);
                                Context.SaveChanges();
                            }

                            
                            //var walkInCC =  Context.CashCustomer.FirstOrDefault(x => x.Name.ToLower() == ("Walk-In").ToLower());
                            //if(walkInCC != null)
                            //{
                            //    var walkInContact = Context.Contacts.FirstOrDefault(x => x.EnglishName.ToLower() == ("Walk-In").ToLower());
                            //    walkInCC.Id= walkInContact.Id;
                            //    Context.CashCustomer.Update(walkInCC);
                            //}


                            var cashCustomers = await Context.CashCustomer.AsNoTracking().ToListAsync();
                            var contacts = await Context.Contacts.AsNoTracking().ToListAsync();
                            var contactsList = new List<Focus.Domain.Entities.Contact>();
                            int i = 0;
                            var autoNos = await _Mediator.Send(new GetContactNumberQuery
                            {
                                isCustomer = true,
                                isCashCustomer = true,
                            });
                            var walkInAccount = Context.Accounts.AsNoTracking().FirstOrDefault(x => x.Name == "Walk-In");
                            var number = autoNos.CashCustomer;


                            foreach (var cash in cashCustomers)
                            {
                                var checkContacts = contacts.FirstOrDefault(x=> x.CashCustomerId == cash.Id);
                              
                                if (checkContacts == null && cash.Name.ToLower() != ("Walk-In").ToLower())
                                {
                                    if (i != 0)
                                    {
                                        string fetchNo = number.Substring(3);
                                        Int32 autoNo = Convert.ToInt32((fetchNo));
                                        var format = "00000";
                                        autoNo++;
                                        string prefix;

                                        {
                                            prefix = "WC-";
                                        }
                                        var newCode = prefix + autoNo.ToString(format);
                                        number = newCode;
                                    }
                                    i++;


                                    contactsList.Add(new Contact
                                    {
                                        CashCustomerId = cash.Id,
                                        Code = (number).ToString(),
                                        ArabicName = cash.Name,
                                        CustomerDisplayName = cash.Name,
                                        Address = cash.Address,
                                        VatNo = cash.CustomerId,
                                        IsActive = true,
                                        IsCustomer = true,
                                        IsRaw = false,
                                        AccountId = walkInAccount.Id,
                                        IsCashCustomer = true,
                                        ContactNo1 = cash.Mobile,
                                    });
                                }
                                await Context.Contacts.AddRangeAsync(contactsList);
                            }


                        }
                        if (request.InvoiceChange)
                        {
                            var walkInCC = Context.CashCustomer.FirstOrDefault(x => x.Name.ToLower() == ("Walk-In").ToLower());

                            if(walkInCC != null)
                            {
                                Random rnd = new Random();
                                for (int i = 0; i < 11; i++)
                                {
                                    _code += rnd.Next(0, 9).ToString();
                                }

                                var sales = await Context.Sales.AsNoTracking().Where(x => x.CashCustomerId == walkInCC.Id).ToListAsync();
                                var walkInContact = Context.Contacts.FirstOrDefault(x => x.EnglishName.ToLower() == ("Walk-In").ToLower());
                                foreach (var item in sales)
                                {
                                    item.CustomerId = walkInContact.Id;
                                     Context.Sales.Update(item);
                                }

                                if(walkInContact != null)
                                {
                                    walkInContact.CashCustomerId = walkInContact.Id; 
                                    Context.Contacts.Update(walkInContact);
                                }

                                await _Mediator.Send(new AddUpdateSyncRecordCommand()
                                {
                                    SyncRecordModels = Context.SyncLog(),
                                    Code = _code,
                                }, cancellationToken);

                                await Context.SaveChangesAsync(cancellationToken);
                            }





                            var invoiceList = await Context.Sales.AsNoTracking().Where(x => x.CashCustomerId != null && x.CustomerId == null).ToListAsync();
                            var contactList = await Context.Contacts.AsNoTracking().Where(x => x.CashCustomerId != null).Select(x => new
                            {
                                Id = x.Id,
                                CashCustomerId = x.CashCustomerId

                            }).ToListAsync();

                            foreach (var sale in invoiceList)
                            {
                                var contact = contactList.FirstOrDefault(x => x.CashCustomerId == sale.CashCustomerId);
                                if (contact != null)
                                {
                                    sale.CustomerId = contact.Id;
                                }
                            }
                            Context.Sales.UpdateRange(invoiceList);
                        }

                    }

                    Random rnd1 = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd1.Next(0, 9).ToString();
                    }

                    await _Mediator.Send(new AddUpdateSyncRecordCommand()
                    {
                        SyncRecordModels = Context.SyncLog(),
                        Code = _code,
                    }, cancellationToken);

                    await Context.SaveChangesAsync(cancellationToken);
                    return new Message
                    {
                        IsAddUpdate = "Data has been added successfully"
                    };

                }
                catch (Exception exception)
                {
                    Logger.LogError(exception.Message);
                    throw new ApplicationException("Unable to process your request please contact support");
                }
            }
        }
    }
}
