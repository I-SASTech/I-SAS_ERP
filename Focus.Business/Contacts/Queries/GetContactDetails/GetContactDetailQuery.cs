using Focus.Business.Attachments.Commands;
using Focus.Business.HR.Payroll.Contributions.Commands;
using Focus.Business.Interface;
using Focus.Business.PriceRecordChanges.Model;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.Contacts.Queries.GetContactDetails
{
    public class GetContactDetailQuery : IRequest<ContactDetailLookUpModel>
    {
        public Guid Id { get; set; }
        public bool IsMultipleAddress { get; set; }

        public class Handler : IRequestHandler<GetContactDetailQuery, ContactDetailLookUpModel>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<GetContactDetailQuery> logger)
            {
                _context = context;
                _logger = logger;
            }
            public async Task<ContactDetailLookUpModel> Handle(GetContactDetailQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var contact = new Contact();
                    var contactBankAccounts = new List<ContactBankAccount>();
                    var deliveryAddresses = new List<AddressLookUpModel>();
                    var contactPersons = new List<ContactPerson>();



                    if (request.IsMultipleAddress)
                    {
                        contact = await _context.Contacts
                       .AsNoTracking()
                       .Include(x => x.Currency)
                       .Include(x => x.TaxRate)
                       .Include(x => x.ContactPersons)
                       .Include(x => x.ContactBankAccounts)
                       .Include(x => x.DeliveryAddresses)
                       .ThenInclude(x => x.DeliveryHolidays)
                       .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);

                        if (contact.DeliveryAddresses != null)
                        {
                            foreach (var item in contact.DeliveryAddresses)
                            {
                                deliveryAddresses.Add(new AddressLookUpModel
                                {
                                    Id=item.Id,
                                    ContactId = contact.Id,
                                    Type = item.Type,
                                    Address = item.Address,
                                    Address2 = item.Address2,
                                    IsActive = item.IsActive,
                                    IsDefault = item.IsDefault,
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
                                    AllHour = item.AllHour,
                                    AllDaySelection = item.AllDaySelection,
                                    DeliveryHolidays = item.DeliveryHolidays.Select(x => new DeliveryHolidayLookUpModel
                                    {
                                        Id = x.Id,
                                        DeliveryAddressId = x.DeliveryAddressId,
                                        WeekHolidayId = x.WeekHolidayId,
                                    }).ToList(),

                                }); ;
                            }
                        }

                       
                    }
                    else
                    {
                        contact = await _context.Contacts
                      .AsNoTracking()
                      .Include(x => x.Currency)
                      .Include(x => x.TaxRate)
                      .Include(x => x.ContactPersons)
                      .Include(x => x.ContactBankAccounts)
                      .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);
                    }

                    foreach (var item in contact.ContactPersons)
                    {
                        contactPersons.Add(new ContactPerson
                        {
                            Id = item.Id,
                            ContactId = contact.Id,
                            Prefix = item.Prefix,
                            FirstName = item.FirstName,
                            LastName = item.LastName,
                            Email = item.Email,
                            Phone = item.Phone,
                            Mobile = item.Mobile
                        });
                    }
                    foreach (var item in contact.ContactBankAccounts)
                    {
                        contactBankAccounts.Add(new ContactBankAccount
                        {
                            Id = item.Id,
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
                        });
                    }

                  

                    var attachmentList = await _context.Attachments.AsNoTracking().Where(x => x.DocumentId == request.Id).Select(x => new AttachmentLookUpModel
                    {
                        Id = x.Id,
                        DocumentId = x.DocumentId,
                        Date = x.Date,
                        Description = x.Description,
                        FileName = x.FileName,
                        Path = x.Path
                    }).ToListAsync(cancellationToken: cancellationToken);

                    var priceRecord = await _context.PriceRecords.AsNoTracking().Include(x => x.Product).Where(x => x.CustomerId == contact.Id).ToListAsync(cancellationToken: cancellationToken);

                    var priceRecordList = new List<PriceLabelsProuctsListLookupModel>();
                    if(priceRecord != null)
                    {
                        foreach (var item in priceRecord)
                        {
                            priceRecordList.Add(new PriceLabelsProuctsListLookupModel()
                            {
                                Code = item.Product.Code,
                                ProductName = item.Product.EnglishName == null ? item.Product.ArabicName : item.Product.EnglishName,
                                ProductId = item.ProductId,
                                Price = item.NewPrice,
                                SalePrice = item.SalePrice ,
                                PurchasePrice = item.PurchasePrice,
                                CostPrice = item.CostPrice,
                                IsActive = item.IsActive,
                                NewPrice = item.NewPrice,
                                CustomerId = contact.Id,
                                IsCustomer= item.IsCustomer,
                            });
                        }
                       
                    }

                    var contactDetail = new ContactDetailLookUpModel
                    {
                        Id = contact.Id,
                        Code = contact.Code,
                        IsAddressOnAll = contact.IsAddressOnAll,
                        IsCashCustomer = contact.IsCashCustomer,
                        RegionId = contact.RegionId,
                        Prefix = contact.Prefix,
                        EnglishName = contact.EnglishName,
                        ArabicName = contact.ArabicName,
                        CompanyNameEnglish = contact.CompanyNameEnglish,
                        CompanyNameArabic = contact.CompanyNameArabic,
                        CustomerDisplayName = contact.CustomerDisplayName,
                        Telephone = contact.Telephone,
                        Email = contact.Email,
                        RegistrationDate = contact.RegistrationDate,
                        Category = contact.Category,
                        CustomerType = contact.CustomerType,
                        CustomerGroup = contact.CustomerGroup,
                        ContactNo1 = contact.ContactNo1,
                        PriceLabelId = contact.PriceLabelId,
                        CommercialRegistrationNo = contact.CommercialRegistrationNo,
                        VatNo = contact.VatNo,
             
                        CurrencyId = contact.CurrencyId,
                        TaxRateId = contact.TaxRateId,
                        Website = contact.Website,

                        BillingAttention = contact.BillingAttention,
                        BillingCountry = contact.Country,
                        BillingZipCode = contact.BillingZipCode,
                        BillingPhone = contact.BillingPhone,
                        BillingArea = contact.Area,
                        BillingAddress = contact.Address,
                        BillingCity = contact.City,
                        BillingFax = contact.BillingFax,

                        CustomerGroupId = contact.CustomerGroupId,

                        ShippingAttention = contact.ShippingAttention,
                        ShippingCountry = contact.ShippingCountry,
                        ShippingZipCode = contact.ShippingZipCode,
                        ShippingPhone = contact.ShippingPhone,
                        ShippingArea = contact.ShippingArea,
                        ShippingAddress = contact.ShippingAddress,
                        ShippingCity = contact.ShippingCity,
                        ShippingFax = contact.ShippingFax,

                        Remarks = contact.Remarks,
                        IsCustomer = contact.IsCustomer,
                        IsActive = contact.IsActive,

                        PaymentTerms = contact.PaymentTerms,
                        CustomerCode = contact.CustomerCode,
                        DeliveryTerm = contact.DeliveryTerm,
                        CreditLimit = contact.CreditLimit,
                        CreditPeriod = contact.CreditPeriod,
                        CreditDays = contact.CreditDays,
                        //End

                        SupplierType = contact.SupplierType,
                        ExpiryDate = contact.ExpiryDate,
                        Status = contact.Status,
                        AccountId = contact.AccountId,
                        IsRaw = contact.IsRaw,

                        CurrencyName = contact.Currency?.Name,
                        TaxRateName = contact.TaxRate?.Name,

                        ProductList = priceRecordList,
                        AttachmentList = attachmentList,
                        DeliveryAddress = contact.DeliveryAddress,
                        ShippingOther = contact.ShippingOther,
                        DeliveryOther = contact.DeliveryOther,
                        DeliveryAddressList = deliveryAddresses,
                        ContactPersonList = contactPersons,
                        ContactBankAccountList = contactBankAccounts,
                        WhatsAppNumber = contact.WhatsAppNumber,
                        Date = contact.Date,
                        IsAllowEmail = contact.IsAllowEmail,
                        IsAutoEmail = contact.IsAutoEmail,
                    };

                    return contactDetail;
                }
                catch (Exception e)
                {
                    _logger.LogError(e.Message);

                    throw new ApplicationException("Unable to process your request please contact support");
                }

            }
        }
    }
}
