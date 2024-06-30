using Focus.Business.Attachments.Commands;
using Focus.Business.Common;
using Focus.Business.Contacts.Queries;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.Models;
using Focus.Business.PriceRecordChanges.Model;
using Focus.Business.SyncRecords.Commands;
using Focus.Domain.Entities;
using Focus.Domain.Enum;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.Contacts.Commands.AddUpdateContact
{
    public class AddUpdateContactCommand : IRequest<Message>
    {
        //Get all variable in entity
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string CustomerType { get; set; }
        public string CustomerCode { get; set; }
        public string Category { get; set; }
        public string EnglishName { get; set; }
        public string ArabicName { get; set; }
        public string CommercialRegistrationNo { get; set; }
        public string VatNo { get; set; }
        public string ContactNo1 { get; set; }
        public string PaymentTerms { get; set; }
        public string CreditDays { get; set; }
        public string CreditLimit { get; set; }
        public bool IsExpire { get; set; }
        public bool IsUpdate { get; set; }
        public bool IsAddressOnAll { get; set; }
        public bool IsCashCustomer { get; set; }
        public bool MultipleAddress { get; set; }
        public string Remarks { get; set; }
        public string RegistrationDate { get; set; }
        public SupplierType? SupplierType { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string Email { get; set; }
        public string DeliveryTerm { get; set; }
        public bool IsActive { get; set; }
        public bool Status { get; set; }
        public DateTime? ActiveDate { get; set; }
        public DateTime? CaptureDate { get; set; }
        public string Reason { get; set; }
        public bool IsCustomer { get; set; }
        public Guid? RegionId { get; set; }
        public Guid? AccountId { get; set; }
        public string CreditPeriod { get; set; }
        public string Telephone { get; set; }
        public string Website { get; set; }
        public bool IsRaw { get; set; }
        public string CustomerGroup { get; set; }
        public List<AttachmentLookUpModel> AttachmentList { get; set; }
        public string Prefix { get; set; }
        public string CompanyNameArabic { get; set; }
        public string CompanyNameEnglish { get; set; }
        public string CustomerDisplayName { get; set; }
        public Guid? CurrencyId { get; set; }
        public Guid? TaxRateId { get; set; }

        //Billing
        public string BillingAttention { get; set; }
        public string BillingCountry { get; set; }
        public string BillingZipCode { get; set; }
        public string BillingPhone { get; set; }
        public string BillingArea { get; set; }
        public string BillingAddress { get; set; }
        public string BillingCity { get; set; }
        public string BillingFax { get; set; }
        //Shipping
        public string ShippingAttention { get; set; }
        public string ShippingCountry { get; set; }
        public string ShippingZipCode { get; set; }
        public string ShippingPhone { get; set; }
        public string ShippingArea { get; set; }
        public string ShippingAddress { get; set; }
        public string ShippingCity { get; set; }
        public Guid? CustomerGroupId { get; set; }
        public string ShippingFax { get; set; }
        public string DeliveryAddress { get; set; }
        public string ShippingOther { get; set; }
        public string DeliveryOther { get; set; }
        public Guid? PriceLabelId { get; set; }
        public List<ContactBankAccount> ContactBankAccountList { get; set; }
        public List<AddressLookUpModel> DeliveryAddressList { get; set; }
        public List<ContactPerson> ContactPersonList { get; set; }
        public List<PriceLabelsProuctsListLookupModel> ProductList { get; set; }
        public string WhatsAppNumber { get; set; }
        public DateTime? Date { get; set; }
        public bool IsAllowEmail { get; set; }
        public bool IsAutoEmail { get; set; }
        public class Handler : IRequestHandler<AddUpdateContactCommand, Message>
        {
            //Create variable of IApplicationDbContext for Add and update database
            private readonly IApplicationDbContext _context;

            //Create logger interface variable for log error
            private readonly ILogger _logger;
            private string _code;
            private readonly IMediator _mediator;

            public Handler(IApplicationDbContext context, ILogger<AddUpdateContactCommand> logger, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
            }


            public async Task<Message> Handle(AddUpdateContactCommand request, CancellationToken cancellationToken)
            {

                try
                {
                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }

                    if (request.Id != Guid.Empty)
                    {
                        if (request.IsUpdate)
                        {

                            var contact1 = await _context.Contacts.FindAsync(request.Id);
                            if (contact1 == null)
                            {
                                throw new NotFoundException("Contact not found ", request.AccountId);
                            }
                            contact1.CommercialRegistrationNo = request.CommercialRegistrationNo;
                            contact1.VatNo = request.VatNo;
                            contact1.ContactNo1 = request.ContactNo1;
                            contact1.Email = request.Email;
                            contact1.Address = request.BillingAddress;
                            contact1.DeliveryAddress = request.DeliveryAddress;
                            contact1.DeliveryOther = request.DeliveryOther;
                            contact1.ShippingOther = request.ShippingOther;
                            contact1.ShippingAddress = request.ShippingAddress;
                            contact1.CustomerCode = request.CustomerCode;
                            contact1.IsAllowEmail = request.IsAllowEmail;
                            contact1.IsAutoEmail = request.IsAutoEmail;
                            if (request.MultipleAddress)
                            {
                                //Add Delivery Address
                                if (request.IsCustomer && request.DeliveryAddressList != null && request.DeliveryAddressList.Any(x => x.IsNew))
                                {
                                    request.DeliveryAddressList = request.DeliveryAddressList.Where(x => x.IsNew).ToList();
                                    foreach (var item in request.DeliveryAddressList)
                                    {
                                        var delivery = new DeliveryAddress
                                        {
                                            Id = item.Id.Value,
                                            ContactId = contact1.Id,
                                            Type = item.Type,
                                            Address = item.Address,
                                            Address2 = item.Address2,
                                            ContactName = item.ContactName,
                                            Langitutue = item.Langitutue,
                                            Latitude = item.Latitude,
                                            GoogleLocation = item.GoogleLocation,
                                            NearBy = item.NearBy,
                                            FromTime = item.FromTime,
                                            ToTime = item.ToTime,
                                            IsOffice = false,
                                            Country = item.Country,
                                            BillingZipCode = item.BillingZipCode,
                                            ContactNumber = item.ContactNumber,
                                            Area = item.Area,
                                            City = item.City,
                                            BillingFax = item.BillingFax,
                                            IsActive = item.IsActive,
                                            IsDefault = item.IsDefault,
                                            AllHour = item.AllHour,
                                            AllDaySelection = item.AllDaySelection,
                                        };

                                        await _context.DeliveryAddresses.AddAsync(delivery, cancellationToken);

                                        foreach (var week in item.DeliveryHolidays)
                                        {
                                            var weekHoliday = new DeliveryHoliday
                                            {
                                                DeliveryAddressId = delivery.Id,
                                                WeekHolidayId = week.WeekHolidayId,
                                            };
                                            await _context.DeliveryHolidays.AddAsync(weekHoliday, cancellationToken);
                                        }
                                    }
                                }
                            }


                            var log = _context.SyncLog();
                            var branches = await _context.Branches.AsNoTracking().ToListAsync(cancellationToken: cancellationToken);
                            var list = new List<SyncRecordModel>();
                            if (branches.Any())
                            {
                                foreach (var branch in branches)
                                {
                                    var syncRecords = log.Select(x => new SyncRecordModel
                                    {
                                        Table = x.Table,
                                        ColumnId = x.ColumnId,
                                        CompanyId = x.CompanyId,
                                        ColumnType = x.ColumnType,
                                        Push = x.Push,
                                        Pull = x.Pull,
                                        Action = x.Action,
                                        ChangeDate = DateTime.Now,
                                        PullDate = x.PullDate,
                                        PushDate = x.PushDate,
                                        ColumnName = x.ColumnName,
                                        BranchId = branch.Id,
                                        Code = _code,
                                    }).ToList();

                                    list.AddRange(syncRecords);
                                }
                            }
                            else
                            {
                                var syncRecords = log.Select(x => new SyncRecordModel
                                {
                                    Table = x.Table,
                                    ColumnId = x.ColumnId,
                                    CompanyId = x.CompanyId,
                                    ColumnType = x.ColumnType,
                                    Push = x.Push,
                                    Pull = x.Pull,
                                    Action = x.Action,
                                    ChangeDate = DateTime.Now,
                                    PullDate = x.PullDate,
                                    PushDate = x.PushDate,
                                    ColumnName = x.ColumnName,
                                    //BranchId = branch.Id,
                                    Code = _code,
                                }).ToList();

                                list.AddRange(syncRecords);
                            }


                            await _mediator.Send(new AddUpdateSyncRecordCommand()
                            {
                                SyncRecordModels = list,
                                IsServer = true
                            }, cancellationToken);

                            await _context.SaveChangesAsync(cancellationToken);

                            var message1 = new Message
                            {
                                Id = contact1.Id,
                                IsAddUpdate = "Data has been updated successfully"
                            };
                            return message1;
                        }


                        var account = await _context.Accounts.FindAsync(request.AccountId);
                        if (account == null)
                            throw new NotFoundException("Account Id ", request.AccountId);

                        account.Name = request.CustomerDisplayName;
                        account.NameArabic = request.CustomerDisplayName;
                        account.Description = request.CustomerDisplayName;
                        account.IsActive = request.IsActive;

                        var contact = await _context.Contacts.FindAsync(request.Id);
                        if (contact == null)
                            throw new NotFoundException("contact Id ", request.AccountId);


                        if (request.EnglishName != "" && request.ArabicName != "")
                        {
                            //bool isContact = await Context.Contacts.AnyAsync(x => (x.EnglishName == request.EnglishName || x.ArabicName == request.ArabicName) && x.IsCustomer==request.IsCustomer, cancellationToken);
                            //if (contact.EnglishName != request.EnglishName && isContact)
                            //    throw new ObjectAlreadyExistsException("Contact Name " + request.EnglishName + " Already Exist");
                            //if (contact.ArabicName != request.ArabicName && isContact)
                            //    throw new ObjectAlreadyExistsException("Contact Name " + request.ArabicName + " Already Exist");

                        }
                        else if (request.EnglishName != "")
                        {
                            //bool isContact = await Context.Contacts.AnyAsync(x => x.ContactNo1 == request.ContactNo1 && x.IsCustomer == request.IsCustomer, cancellationToken);
                            //if (contact.EnglishName != request.EnglishName && isContact)
                            //    throw new ObjectAlreadyExistsException("Mobile Number " + request.EnglishName + " Already Exist");
                        }
                        else
                        {
                            bool isContact = await _context.Contacts.AnyAsync(x => x.ArabicName == request.ArabicName && x.IsCustomer == request.IsCustomer, cancellationToken);
                            if (contact.ArabicName != request.ArabicName && isContact)
                                throw new ObjectAlreadyExistsException("Contact Name " + request.ArabicName + " Already Exist");
                        }





                        contact.RegionId = request.RegionId;
                        contact.Code = request.Code;
                        contact.CustomerCode = request.CustomerCode;
                        contact.Prefix = request.Prefix;
                        contact.EnglishName = request.EnglishName;
                        contact.ArabicName = request.ArabicName;
                        contact.CompanyNameEnglish = request.CompanyNameEnglish;
                        contact.CompanyNameArabic = request.CompanyNameArabic;
                        contact.CustomerDisplayName = request.CustomerDisplayName;
                        contact.Telephone = request.Telephone;
                        contact.Email = request.Email;
                        contact.RegistrationDate = request.RegistrationDate;
                        contact.Category = request.Category;
                        contact.CustomerType = request.CustomerType;
                        contact.CustomerGroup = request.CustomerGroup;
                        contact.ContactNo1 = request.ContactNo1;
                        contact.IsCashCustomer = request.IsCashCustomer;
                        contact.CustomerGroupId = request.CustomerGroupId;

                        contact.CommercialRegistrationNo = request.CommercialRegistrationNo;
                        contact.VatNo = request.VatNo;
                        contact.IsAddressOnAll = request.IsAddressOnAll;

                        contact.CurrencyId = request.CurrencyId;
                        contact.TaxRateId = request.TaxRateId;
                        contact.Website = request.Website;
                        contact.PriceLabelId = request.PriceLabelId;
                        contact.BillingAttention = request.BillingAttention;
                        contact.Country = request.BillingCountry;
                        contact.BillingZipCode = request.BillingZipCode;
                        contact.BillingPhone = request.BillingPhone;
                        contact.Area = request.BillingArea;
                        contact.Address = request.BillingAddress;
                        contact.City = request.BillingCity;
                        contact.BillingFax = request.BillingFax;

                        contact.DeliveryAddress = request.DeliveryAddress;
                        contact.DeliveryOther = request.DeliveryOther;
                        contact.ShippingOther = request.ShippingOther;
                        contact.ShippingAttention = request.ShippingAttention;
                        contact.ShippingCountry = request.ShippingCountry;
                        contact.ShippingZipCode = request.ShippingZipCode;
                        contact.ShippingPhone = request.ShippingPhone;
                        contact.ShippingArea = request.ShippingArea;
                        contact.ShippingAddress = request.ShippingAddress;
                        contact.ShippingCity = request.ShippingCity;
                        contact.ShippingFax = request.ShippingFax;

                        contact.Remarks = request.Remarks;
                        contact.IsCustomer = request.IsCustomer;
                        contact.IsActive = request.IsActive;

                        contact.PaymentTerms = request.PaymentTerms;
                        contact.DeliveryTerm = request.DeliveryTerm;
                        contact.CreditLimit = request.CreditLimit;
                        contact.CreditPeriod = request.CreditPeriod;
                        contact.CreditDays = request.CreditDays;
                        //end

                        contact.SupplierType = request.SupplierType;
                        contact.ExpiryDate = request.ExpiryDate;
                        contact.Status = request.Status;
                        contact.AccountId = account.Id;
                        contact.IsRaw = request.IsRaw;
                        contact.WhatsAppNumber = request.WhatsAppNumber;
                        contact.Date = request.Date;
                        contact.IsAllowEmail = request.IsAllowEmail;
                        contact.IsAutoEmail = request.IsAutoEmail;

                        if (!request.IsActive)
                        {
                            contact.StatusDate = DateTime.UtcNow;
                        }


                        if (request.MultipleAddress)
                        {
                            foreach (var x in contact.DeliveryAddresses)
                            {
                                if (x.DeliveryHolidays != null && x.DeliveryHolidays.Any())
                                    _context.DeliveryHolidays.RemoveRange(x.DeliveryHolidays);


                            }
                            _context.DeliveryAddresses.RemoveRange(contact.DeliveryAddresses);

                            //Add Delivery Address
                            if ( request.DeliveryAddressList != null && request.DeliveryAddressList.Any())
                            {
                                foreach (var item in request.DeliveryAddressList)
                                {
                                    var delivery = new DeliveryAddress
                                    {
                                        ContactId = contact.Id,
                                        Type = item.Type,
                                        Address = item.Address,
                                        Address2 = item.Address2,
                                        ContactName = item.ContactName,
                                        Langitutue = item.Langitutue,
                                        Latitude = item.Latitude,
                                        GoogleLocation = item.GoogleLocation,
                                        NearBy = item.NearBy,
                                        FromTime = item.FromTime,
                                        ToTime = item.ToTime,
                                        IsOffice = false,
                                        Country = item.Country,
                                        BillingZipCode = item.BillingZipCode,
                                        ContactNumber = item.ContactNumber,
                                        Area = item.Area,
                                        City = item.City,
                                        BillingFax = item.BillingFax,
                                        IsActive = item.IsActive,
                                        IsDefault = item.IsDefault,
                                        AllHour = item.AllHour,
                                        AllDaySelection = item.AllDaySelection,
                                    };

                                    await _context.DeliveryAddresses.AddAsync(delivery, cancellationToken);

                                    foreach (var week in item.DeliveryHolidays)
                                    {
                                        var weekHoliday = new DeliveryHoliday
                                        {
                                            DeliveryAddressId = delivery.Id,
                                            WeekHolidayId = week.WeekHolidayId,
                                        };
                                        await _context.DeliveryHolidays.AddAsync(weekHoliday, cancellationToken);
                                    }
                                }
                            }
                        }


                        _context.ContactBankAccounts.RemoveRange(contact.ContactBankAccounts);

                        //Add Contact Person
                        if (request.ContactPersonList != null && request.ContactPersonList.Any())
                        {
                            foreach (var contactPerson in request.ContactPersonList.Select(item => new ContactPerson()
                            {
                                ContactId = contact.Id,
                                Prefix = item.Prefix,
                                FirstName = item.FirstName,
                                LastName = item.LastName,
                                Email = item.Email,
                                Phone = item.Phone,
                                Mobile = item.Mobile,
                            }))
                            {
                                //Add ContactBankAccounts to database
                                await _context.ContactPersons.AddAsync(contactPerson, cancellationToken);
                            }
                        }

                        //Add Contact Bank Account
                        if (!request.IsCustomer && request.ContactBankAccountList != null && request.ContactBankAccountList.Any())
                        {
                            foreach (var bankAccount in request.ContactBankAccountList.Select(item => new ContactBankAccount()
                            {
                                ContactId = contact.Id,
                                AccountTitle = item.AccountTitle,
                                AccountNo = item.AccountNo,
                                Iban = item.Iban,
                                NameOfBank = item.NameOfBank,
                                BranchName = item.BranchName,
                                RoutingCode = item.RoutingCode,
                                City = item.City,
                                Country = item.Country,
                                Currency = item.Currency,
                                Address = item.Address
                            }))
                            {
                                //Add ContactBankAccounts to database
                                await _context.ContactBankAccounts.AddAsync(bankAccount, cancellationToken);
                            }
                        }

                        var attachments = _context.Attachments
                            .AsNoTracking()
                            .Where(x => x.DocumentId == contact.Id)
                            .AsQueryable();

                        if (attachments.Any())
                        {
                            _context.Attachments.RemoveRange(attachments);
                        }
                        if (request.AttachmentList != null && request.AttachmentList.Any())
                        {
                            foreach (var attachment in request.AttachmentList.Select(item => new Attachment()
                            {
                                Date = item.Date,
                                DocumentId = contact.Id,
                                Description = item.Description,
                                FileName = item.FileName,
                                Path = item.Path
                            }))
                            {
                                //Add Attachments to database
                                await _context.Attachments.AddAsync(attachment, cancellationToken);
                            }
                        }



                        if (request.PriceLabelId == null)
                        {
                            var priceRecord = await _context.PriceRecords.AsNoTracking().Where(x => x.CustomerId == contact.Id).ToListAsync();

                            if (priceRecord.Count > 0)
                            {
                                _context.PriceRecords.RemoveRange(priceRecord);

                                var priceRecordProducts = new List<PriceRecord>();

                                foreach (var item in request.ProductList)
                                {
                                    priceRecordProducts.Add(new PriceRecord()
                                    {
                                        ProductId = item.ProductId,
                                        Price = 0,
                                        SalePrice = item.SalePrice ?? 0,
                                        PurchasePrice = item.PurchasePrice ?? 0,
                                        CostPrice = item.CostPrice ?? 0,
                                        IsActive = item.IsActive,
                                        NewPrice = item.Price ?? 0,
                                        CustomerId = contact.Id,
                                        IsCustomer = true
                                    });
                                }


                                await _context.PriceRecords.AddRangeAsync(priceRecordProducts, cancellationToken);

                            }
                            else
                            {
                                var priceRecordProducts = new List<PriceRecord>();

                                foreach (var item in request.ProductList)
                                {
                                    priceRecordProducts.Add(new PriceRecord()
                                    {
                                        ProductId = item.ProductId,
                                        Price = 0,
                                        SalePrice = item.SalePrice ?? 0,
                                        PurchasePrice = item.PurchasePrice ?? 0,
                                        CostPrice = item.CostPrice ?? 0,
                                        IsActive = item.IsActive,
                                        NewPrice = item.Price ?? 0,
                                        CustomerId = contact.Id,
                                        IsCustomer = true
                                    });
                                }


                                await _context.PriceRecords.AddRangeAsync(priceRecordProducts, cancellationToken);
                            }
                        }

                        var log1 = _context.SyncLog();
                        var branches1 = await _context.Branches.AsNoTracking().ToListAsync(cancellationToken: cancellationToken);
                        var list1 = new List<SyncRecordModel>();
                        if (branches1.Any())
                        {
                            foreach (var branch in branches1)
                            {
                                var syncRecords = log1.Select(x => new SyncRecordModel
                                {
                                    Table = x.Table,
                                    ColumnId = x.ColumnId,
                                    CompanyId = x.CompanyId,
                                    ColumnType = x.ColumnType,
                                    Push = x.Push,
                                    Pull = x.Pull,
                                    Action = x.Action,
                                    ChangeDate = DateTime.Now,
                                    PullDate = x.PullDate,
                                    PushDate = x.PushDate,
                                    ColumnName = x.ColumnName,
                                    BranchId = branch.Id,
                                    Code = _code,
                                }).ToList();

                                list1.AddRange(syncRecords);
                            }
                        }
                        else
                        {
                            var syncRecords = log1.Select(x => new SyncRecordModel
                            {
                                Table = x.Table,
                                ColumnId = x.ColumnId,
                                CompanyId = x.CompanyId,
                                ColumnType = x.ColumnType,
                                Push = x.Push,
                                Pull = x.Pull,
                                Action = x.Action,
                                ChangeDate = DateTime.Now,
                                PullDate = x.PullDate,
                                PushDate = x.PushDate,
                                ColumnName = x.ColumnName,
                                //BranchId = branch.Id,
                                Code = _code,
                            }).ToList();

                            list1.AddRange(syncRecords);
                        }

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = list1,
                            IsServer = true
                        }, cancellationToken);

                        //Save changes to database
                        await _context.SaveChangesAsync(cancellationToken);
                        var message = new Message
                        {
                            Id = contact.Id,
                            IsAddUpdate = "Data has been updated successfully"
                        };
                        return message;
                    }
                    else
                    {

                        if (request.EnglishName != "" && request.ArabicName != "")
                        {
                            var isContact = await _context.Contacts.AnyAsync(x => (x.EnglishName == request.EnglishName || x.ArabicName == request.ArabicName) && x.IsCustomer == request.IsCustomer, cancellationToken);
                            if (isContact)
                                throw new ObjectAlreadyExistsException("Contact Name " + request.EnglishName + " " + request.ArabicName + " Already Exist");
                        }
                        else if (request.EnglishName != "")
                        {
                            var isContact = await _context.Contacts.AnyAsync(x => x.EnglishName == request.EnglishName && x.IsCustomer == request.IsCustomer, cancellationToken);
                            if (isContact)
                                throw new ObjectAlreadyExistsException("Contact Name " + request.EnglishName + " Already Exist");
                        }
                        else if (request.ArabicName != "")
                        {
                            var isContact = await _context.Contacts.AnyAsync(x => x.ArabicName == request.ArabicName && x.IsCustomer == request.IsCustomer, cancellationToken);
                            if (isContact)
                                throw new ObjectAlreadyExistsException("Contact Name " + request.ArabicName + " Already Exist");
                        }

                        if (!string.IsNullOrEmpty(request.ContactNo1))
                        {
                            bool isContact = await _context.Contacts.AnyAsync(x => x.ContactNo1 == request.ContactNo1 && x.IsCustomer == request.IsCustomer, cancellationToken);
                            if (isContact)
                                throw new ObjectAlreadyExistsException("Mobile Number " + request.ContactNo1 + " Already Exist");
                        }

                        if (!string.IsNullOrEmpty(request.Code))
                        {
                            bool isContact = await _context.Contacts.AnyAsync(x => x.Code == request.Code && x.IsCustomer == request.IsCustomer, cancellationToken);
                            if (isContact)
                            {
                                var result = await _mediator.Send(new GetContactNumberQuery
                                {
                                    isCustomer = request.IsCustomer,
                                    isCashCustomer= request.IsCashCustomer
                                });
                                if (request.IsCashCustomer)
                                {
                                    request.Code=result.CashCustomer;
                                }
                                else
                                {
                                    request.Code=result.Contact;
                                }
                            }
                        }
                        else
                        {
                            var result = await _mediator.Send(new GetContactNumberQuery
                            {
                                isCustomer = request.IsCustomer,
                                isCashCustomer= request.IsCashCustomer
                            });
                            if (request.IsCashCustomer)
                            {
                                request.Code=result.CashCustomer;
                            }
                            else
                            {
                                request.Code=result.Contact;
                            }
                        }

                        // COA 
                        var account = new Account();
                        if (request.IsCustomer)
                        {
                            if (request.IsCashCustomer)
                            {
                                var accountCount = await _context.Accounts.AsNoTracking().FirstOrDefaultAsync(x => x.Name == "Walk-In", cancellationToken: cancellationToken);
                                if (accountCount != null)
                                {
                                    account = accountCount;
                                }
                            }
                            else
                            {
                                var accountCount = await _context.Accounts.Include(x => x.CostCenter).Where(x => x.CostCenter.Code == "120000").OrderBy(x => x.Code).LastOrDefaultAsync(cancellationToken: cancellationToken);

                                if (accountCount != null)
                                {
                                    account = new Account
                                    {
                                        Name = request.CustomerDisplayName,
                                        Description = request.Code + "  " + request.CustomerDisplayName + "  " + request.ContactNo1,
                                        Code = (int.Parse(accountCount.Code) + 1).ToString(),
                                        CostCenterId = accountCount.CostCenterId,
                                        NameArabic = request.CustomerDisplayName,
                                        IsActive = request.IsActive
                                    };
                                }
                            }

                        }
                        else
                        {
                            var accountCount = await _context.Accounts.Include(x => x.CostCenter).Where(x => x.CostCenter.Code == "200000").OrderBy(x => x.Code).LastOrDefaultAsync(cancellationToken);

                            if (accountCount != null)
                            {
                                account = new Account
                                {
                                    Name = request.CustomerDisplayName,
                                    Description = request.CustomerDisplayName,
                                    Code = (int.Parse(accountCount.Code) + 1).ToString(),
                                    CostCenterId = accountCount.CostCenterId,
                                    IsActive = request.IsActive,
                                    NameArabic = request.CustomerDisplayName
                                };
                            }
                        }
                        if (!request.IsCashCustomer)
                        {
                            await _context.Accounts.AddAsync(account, cancellationToken);

                        }



                        var contact = new Contact
                        {
                            Code = request.Code,
                            Prefix = request.Prefix,
                            CustomerCode = request.CustomerCode,
                            EnglishName = request.EnglishName,
                            ArabicName = request.ArabicName,
                            CompanyNameEnglish = request.CompanyNameEnglish,
                            CompanyNameArabic = request.CompanyNameArabic,
                            CustomerDisplayName = request.CustomerDisplayName,
                            Telephone = request.Telephone,
                            Email = request.Email,
                            RegistrationDate = request.RegistrationDate,
                            Category = request.Category,
                            CustomerType = request.CustomerType,
                            CustomerGroup = request.CustomerGroup,
                            ContactNo1 = request.ContactNo1,
                            RegionId = request.RegionId,
                            IsCashCustomer = request.IsCashCustomer,
                            WhatsAppNumber = request.WhatsAppNumber,
                            CustomerGroupId = request.CustomerGroupId,
                            Date = request.Date,
                            CommercialRegistrationNo = request.CommercialRegistrationNo,
                            VatNo = request.VatNo,

                            CurrencyId = request.CurrencyId,
                            TaxRateId = request.TaxRateId,
                            Website = request.Website,

                            BillingAttention = request.BillingAttention,
                            Country = request.BillingCountry,
                            BillingZipCode = request.BillingZipCode,
                            BillingPhone = request.BillingPhone,
                            Area = request.BillingArea,
                            Address = request.BillingAddress,
                            City = request.BillingCity,
                            BillingFax = request.BillingFax,
                            PriceLabelId = request.PriceLabelId,
                            DeliveryOther = request.DeliveryOther,
                            DeliveryAddress = request.DeliveryAddress,
                            ShippingOther = request.ShippingOther,
                            ShippingAttention = request.ShippingAttention,
                            ShippingCountry = request.ShippingCountry,
                            ShippingZipCode = request.ShippingZipCode,
                            ShippingPhone = request.ShippingPhone,
                            ShippingArea = request.ShippingArea,
                            ShippingAddress = request.ShippingAddress,
                            ShippingCity = request.ShippingCity,
                            ShippingFax = request.ShippingFax,

                            Remarks = request.Remarks,
                            IsCustomer = request.IsCustomer,
                            IsActive = request.IsActive,

                            PaymentTerms = request.PaymentTerms,
                            DeliveryTerm = request.DeliveryTerm,
                            CreditLimit = request.CreditLimit,
                            CreditPeriod = request.CreditPeriod,
                            CreditDays = request.CreditDays,
                            //End

                            SupplierType = request.SupplierType,
                            ExpiryDate = request.ExpiryDate,
                            Status = request.Status,
                            AccountId = account.Id,
                            IsRaw = request.IsRaw,
                            IsAddressOnAll = request.IsAddressOnAll,
                            IsAllowEmail = request.IsAllowEmail,
                            IsAutoEmail = request.IsAutoEmail
                        };
                        if (!request.IsActive)
                        {
                            contact.StatusDate = DateTime.UtcNow;
                        }

                        //Add Contact to database
                        await _context.Contacts.AddAsync(contact, cancellationToken);


                        //Add Attachment
                        if (request.AttachmentList != null && request.AttachmentList.Any())
                        {
                            foreach (var attachment in request.AttachmentList.Select(item => new Attachment()
                            {
                                Date = item.Date,
                                DocumentId = contact.Id,
                                Description = item.Description,
                                FileName = item.FileName,
                                Path = item.Path
                            }))
                            {
                                //Add Attachments to database
                                await _context.Attachments.AddAsync(attachment, cancellationToken);
                            }
                        }

                        //Add Contact Person
                        if (request.ContactPersonList != null && request.ContactPersonList.Any())
                        {
                            foreach (var contactPerson in request.ContactPersonList.Select(item => new ContactPerson()
                            {
                                ContactId = contact.Id,
                                Prefix = item.Prefix,
                                FirstName = item.FirstName,
                                LastName = item.LastName,
                                Email = item.Email,
                                Phone = item.Phone,
                                Mobile = item.Mobile,
                            }))
                            {
                                //Add ContactBankAccounts to database
                                await _context.ContactPersons.AddAsync(contactPerson, cancellationToken);
                            }
                        }

                        //Add Contact Bank Account
                        if (!request.IsCustomer && request.ContactBankAccountList != null && request.ContactBankAccountList.Any())
                        {
                            foreach (var bankAccount in request.ContactBankAccountList.Select(item => new ContactBankAccount()
                            {
                                ContactId = contact.Id,
                                AccountTitle = item.AccountTitle,
                                AccountNo = item.AccountNo,
                                Iban = item.Iban,
                                NameOfBank = item.NameOfBank,
                                BranchName = item.BranchName,
                                RoutingCode = item.RoutingCode,
                                City = item.City,
                                Country = item.Country,
                                Currency = item.Currency,
                                Address = item.Address
                            }))
                            {
                                //Add ContactBankAccounts to database
                                await _context.ContactBankAccounts.AddAsync(bankAccount, cancellationToken);
                            }
                        }
                        if (request.MultipleAddress)
                        {
                            if ( request.DeliveryAddressList != null && request.DeliveryAddressList.Any())
                            {
                                foreach (var item in request.DeliveryAddressList)
                                {
                                    var delivery = new DeliveryAddress
                                    {
                                        ContactId = contact.Id,
                                        Type = item.Type,
                                        Address = item.Address,
                                        ContactName = item.ContactName,
                                        Langitutue = item.Langitutue,
                                        Latitude = item.Latitude,
                                        GoogleLocation = item.GoogleLocation,
                                        NearBy = item.NearBy,
                                        FromTime = item.FromTime,
                                        ToTime = item.ToTime,
                                        IsOffice = false,
                                        Country = item.Country,
                                        BillingZipCode = item.BillingZipCode,
                                        ContactNumber = item.ContactNumber,
                                        Area = item.Area,
                                        City = item.City,
                                        BillingFax = item.BillingFax,
                                        IsActive = item.IsActive,
                                        IsDefault = item.IsDefault,
                                        AllHour = item.AllHour,
                                        AllDaySelection = item.AllDaySelection,
                                    };

                                    await _context.DeliveryAddresses.AddAsync(delivery, cancellationToken);

                                    foreach (var week in item.DeliveryHolidays)
                                    {
                                        var weekHoliday = new DeliveryHoliday
                                        {
                                            DeliveryAddressId = delivery.Id,
                                            WeekHolidayId = week.WeekHolidayId,
                                        };
                                        await _context.DeliveryHolidays.AddAsync(weekHoliday, cancellationToken);
                                    }
                                }
                            }
                        }

                        if (request.ProductList != null)
                        {
                            var priceRecordProducts = new List<PriceRecord>();

                            foreach (var item in request.ProductList)
                            {
                                priceRecordProducts.Add(new PriceRecord()
                                {
                                    ProductId = item.ProductId,
                                    Price = 0,
                                    SalePrice = item.SalePrice ?? 0,
                                    PurchasePrice = item.PurchasePrice ?? 0,
                                    CostPrice = item.CostPrice ?? 0,
                                    IsActive = item.IsActive,
                                    NewPrice = item.NewPrice ?? 0,
                                    CustomerId = contact.Id,
                                    IsCustomer = true
                                });
                            }

                            await _context.PriceRecords.AddRangeAsync(priceRecordProducts, cancellationToken);
                        }

                        var log = _context.SyncLog();
                        var branches = await _context.Branches.AsNoTracking().ToListAsync(cancellationToken: cancellationToken);
                        var list = new List<SyncRecordModel>();
                        if (branches.Any())
                        {
                            foreach (var branch in branches)
                            {
                                var syncRecords = log.Select(x => new SyncRecordModel
                                {
                                    Table = x.Table,
                                    ColumnId = x.ColumnId,
                                    CompanyId = x.CompanyId,
                                    ColumnType = x.ColumnType,
                                    Push = x.Push,
                                    Pull = x.Pull,
                                    Action = x.Action,
                                    ChangeDate = DateTime.Now,
                                    PullDate = x.PullDate,
                                    PushDate = x.PushDate,
                                    ColumnName = x.ColumnName,
                                    BranchId = branch.Id,
                                    Code = _code,
                                }).ToList();

                                list.AddRange(syncRecords);
                            }
                        }
                        else
                        {
                            var syncRecords = log.Select(x => new SyncRecordModel
                            {
                                Table = x.Table,
                                ColumnId = x.ColumnId,
                                CompanyId = x.CompanyId,
                                ColumnType = x.ColumnType,
                                Push = x.Push,
                                Pull = x.Pull,
                                Action = x.Action,
                                ChangeDate = DateTime.Now,
                                PullDate = x.PullDate,
                                PushDate = x.PushDate,
                                ColumnName = x.ColumnName,
                                //BranchId = branch.Id,
                                Code = _code,
                            }).ToList();

                            list.AddRange(syncRecords);
                        }

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = list,
                            IsServer = true
                        }, cancellationToken);

                        await _context.SaveChangesAsync(cancellationToken);
                        var message = new Message
                        {
                            Id = contact.Id,
                            IsAddUpdate = "Data has been Added successfully"
                        };
                        return message;
                    }

                }
                catch (ObjectAlreadyExistsException exception)
                {
                    _logger.LogError(exception.Message);
                    throw new ObjectAlreadyExistsException(exception.Message);
                }
                catch (NotFoundException exception)
                {
                    _logger.LogError(exception.Message);
                    throw new NotFoundException(exception.Message, "");
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception.Message);
                    throw new ApplicationException("Unable to process your request please contact support");
                }
            }
        }
    }
}
