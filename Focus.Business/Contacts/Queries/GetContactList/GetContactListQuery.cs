using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Focus.Business.Common;
using Focus.Business.Interface;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.Contacts.Queries.GetContactList
{
    public class GetContactListQuery : PagedRequest, IRequest<PagedResult<List<ContactLookUpModel>>>
    {
        public bool isCustomer;
        public string SearchTerm { get; set; }
        public bool IsDropDown { get; set; }
        public bool IsActive { get; set; }
        public bool IsRaw { get; set; }
        public bool IsMultipleAddress { get; set; }
        public bool IsCashCustomer { get; set; }
        public string PaymentTerms { get; set; }
        public string PaymentTerms2 { get; set; }
        public string CustomerCategory { get; set; }
        public string Category { get; set; }
        public Guid? CustomerId { get; set; }
        public Guid? CustomerGroupId { get; set; }


        public class Handler : IRequestHandler<GetContactListQuery, PagedResult<List<ContactLookUpModel>>>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                ILogger<GetContactListQuery> logger,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<PagedResult<List<ContactLookUpModel>>> Handle(GetContactListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    if (request.IsDropDown)
                    {
                        if (request.PaymentTerms == null || request.PaymentTerms == "null")
                        {
                            IQueryable<Contact> contact;
                            if (request.IsMultipleAddress)
                            {
                                contact = _context.Contacts
                               .AsNoTracking()
                               .Include(x => x.DeliveryAddresses)
                               .Where(x => x.IsCustomer == request.isCustomer && x.IsActive == request.IsActive && x.IsRaw == request.IsRaw)
                               .AsQueryable();
                            }
                            else
                            {
                                contact = _context.Contacts
                              .AsNoTracking()
                              .Where(x => x.IsCustomer == request.isCustomer && x.IsActive == request.IsActive && x.IsRaw == request.IsRaw)
                              .AsQueryable();
                            }

                            if (!request.IsCashCustomer)
                            {
                                contact = contact.Where(x => !x.IsCashCustomer);
                            }


                            if (!string.IsNullOrEmpty(request.SearchTerm))
                            {
                                var searchTerm = request.SearchTerm;
                                contact = contact.Where(x =>
                                    x.CustomerDisplayName.Contains(searchTerm) || x.Telephone.Contains(searchTerm) ||
                                    x.VatNo.Contains(searchTerm) || x.ContactNo1.Contains(searchTerm) || x.Code.Contains(searchTerm)
                                );

                            }

                            var contactList = await contact
                                .ProjectTo<ContactLookUpModel>(_mapper.ConfigurationProvider)
                                .ToListAsync(cancellationToken);
                            return new PagedResult<List<ContactLookUpModel>>
                            {
                                Results = contactList
                            };
                        }
                        else
                        {
                            var query = _context.Contacts
                                .AsNoTracking()
                                .Where(x => x.IsCustomer == request.isCustomer
                                            && x.IsActive == request.IsActive
                                            && x.IsRaw == request.IsRaw
                                            && (x.PaymentTerms == "Credit" || x.PaymentTerms == "أجل"));

                            if (!request.IsCashCustomer)
                            {
                                query = query.Where(x => !x.IsCashCustomer);
                            }

                            if (request.PaymentTerms2 == "Credit" || request.PaymentTerms2 == "Cash")
                            {
                                query = query.Where(x => x.PaymentTerms == request.PaymentTerms2);
                            }

                            if (request.CustomerId != null && request.CustomerId != Guid.Empty)
                            {
                                query = query.Where(x => x.Id == request.CustomerId);
                            }

                            if (request.CustomerGroupId != null && request.CustomerGroupId != Guid.Empty)
                            {
                                query = query.Where(x => x.CustomerGroupId == request.CustomerGroupId);
                            }

                            if (request.Category != null)
                            {
                                query = query.Where(x => x.Category == request.Category);
                            }

                            if (request.CustomerCategory != null)
                            {
                                query = query.Where(x => x.CustomerType == request.CustomerCategory);
                            }

                            if (!string.IsNullOrEmpty(request.SearchTerm))
                            {
                                var searchTerm = request.SearchTerm;
                                query = query.Where(x => x.Code.Contains(searchTerm) ||
                                                         x.CustomerDisplayName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
                            }

                            var contacts = await query
                                .Select(contact => new ContactLookUpModel
                                {
                                    Id = contact.Id,
                                    Code = contact.Code,
                                    Category = contact.Category,
                                    EnglishName = contact.EnglishName,
                                    ArabicName = contact.ArabicName,
                                    CommercialRegistrationNo = contact.CommercialRegistrationNo,
                                    VatNo = contact.VatNo,
                                    ContactPerson1 = contact.ContactPerson1,
                                    ContactPerson2 = contact.ContactPerson2,
                                    ContactNo1 = contact.ContactNo1,
                                    ContactNo2 = contact.ContactNo2,
                                    Address = contact.Address,
                                    AccountId = contact.AccountId,
                                    City = contact.City,
                                    PaymentTerms = contact.PaymentTerms,
                                    Remarks = contact.Remarks,
                                    RegistrationDate = contact.RegistrationDate,
                                    SupplierType = contact.SupplierType.GetValueOrDefault(),
                                    Email = contact.Email,
                                    ContactNumber = contact.ContactNumber,
                                    Country = contact.Country,
                                    DeliveryTerm = contact.DeliveryTerm,
                                    CreditDays = contact.CreditDays,
                                    CreditLimit = contact.CreditLimit,
                                    CreditPeriod = contact.CreditPeriod,
                                    IsExpire = contact.IsExpire,
                                    IsActive = contact.IsActive,
                                    IsCustomer = contact.IsCustomer,
                                    Status = contact.Status,
                                    Reason = contact.Reason,
                                    AdvanceAccountId = contact.AdvanceAccountId,
                                    CustomerDisplayName = contact.CustomerDisplayName ?? "",
                                    RegionId = contact.RegionId,
                                    CustomerType = contact.CustomerType,
                                    CustomerGroupId = contact.CustomerGroupId,
                                    CompanyNameEnglish = contact.CompanyNameEnglish,
                                    CompanyNameArabic = contact.CompanyNameArabic,
                                    prefix = contact.Prefix,
                                })
                                .ToListAsync(cancellationToken: cancellationToken);


                            //var contacts = await _context.Contacts
                            //    .AsNoTracking()
                            //    .Where(x => x.IsCustomer == request.isCustomer &&  x.IsActive == request.IsActive  && x.IsRaw == request.IsRaw && (x.PaymentTerms == "Credit" || x.PaymentTerms == "أجل"))
                            //    .Select(contact => new ContactLookUpModel
                            //    {
                            //        Id = contact.Id,
                            //        Code = contact.Code,
                            //        Category = contact.Category,
                            //        EnglishName = contact.EnglishName,
                            //        ArabicName = contact.ArabicName,
                            //        CommercialRegistrationNo = contact.CommercialRegistrationNo,
                            //        VatNo = contact.VatNo,
                            //        ContactPerson1 = contact.ContactPerson1,
                            //        ContactPerson2 = contact.ContactPerson2,
                            //        ContactNo1 = contact.ContactNo1,
                            //        ContactNo2 = contact.ContactNo2,
                            //        Address = contact.Address,
                            //        AccountId = contact.AccountId,
                            //        City = contact.City,
                            //        PaymentTerms = contact.PaymentTerms,
                            //        Remarks = contact.Remarks,
                            //        RegistrationDate = contact.RegistrationDate,
                            //        SupplierType = contact.SupplierType.GetValueOrDefault(),
                            //        Email = contact.Email,
                            //        ContactNumber = contact.ContactNumber,
                            //        Country = contact.Country,
                            //        DeliveryTerm = contact.DeliveryTerm,
                            //        CreditDays = contact.CreditDays,
                            //        CreditLimit = contact.CreditLimit,
                            //        CreditPeriod = contact.CreditPeriod,
                            //        IsExpire = contact.IsExpire,
                            //        IsActive = contact.IsActive,
                            //        IsCustomer = contact.IsCustomer,
                            //        Status = contact.Status,
                            //        Reason = contact.Reason,
                            //        AdvanceAccountId = contact.AdvanceAccountId,
                            //        CustomerDisplayName = contact.CustomerDisplayName??"",
                            //        RegionId = contact.RegionId,
                            //        CustomerType = contact.CustomerType,
                            //        CustomerGroupId = contact.CustomerGroupId,
                            //        CompanyNameEnglish = contact.CompanyNameEnglish,
                            //        CompanyNameArabic = contact.CompanyNameArabic,
                            //        prefix = contact.Prefix,
                            //    }).ToListAsync(cancellationToken: cancellationToken);


                            //if (!request.IsCashCustomer)
                            //{
                            //    contacts = contacts.Where(x => !x.IsCashCustomer).ToList();
                            //}

                            //if (request.PaymentTerms2 == "Credit")
                            //{
                            //    contacts = contacts.Where(x => x.PaymentTerms == "Credit").ToList();
                            //}
                            //if (request.PaymentTerms2 == "Cash")
                            //{
                            //    contacts = contacts.Where(x => x.PaymentTerms == "Cash").ToList();
                            //}
                            //if (request.CustomerId != null && request.CustomerId != Guid.Empty)
                            //{
                            //    contacts = contacts.Where(x => x.Id == request.CustomerId).ToList();
                            //}
                            //if (request.CustomerGroupId != null && request.CustomerGroupId != Guid.Empty)
                            //{
                            //    contacts = contacts.Where(x => x.CustomerGroupId == request.CustomerGroupId).ToList();
                            //}
                            //if (request.Category != null)
                            //{
                            //    contacts = contacts.Where(x => x.Category == request.Category).ToList();
                            //}
                            //if (request.CustomerCategory != null)
                            //{
                            //    contacts = contacts.Where(x => x.CustomerType == request.CustomerCategory).ToList();
                            //}

                            //if (!string.IsNullOrEmpty(request.SearchTerm))
                            //{
                            //    var searchTerm = request.SearchTerm;
                            //    contacts = contacts.Where(x => x.Code.Contains(searchTerm) || x.CustomerCode.Contains(searchTerm) ||
                            //                                   x.CustomerDisplayName.ToLower().Contains(searchTerm)).ToList();
                            //}

                            return new PagedResult<List<ContactLookUpModel>>
                            {
                                Results = contacts
                            };
                        }
                    }
                    else
                    {
                        var checkCashCustomer =await _context.Contacts.AsNoTracking().AnyAsync(x => x.EnglishName == "Walk-In", cancellationToken: cancellationToken);
                        if (!checkCashCustomer)
                        {
                            var account = new Account();
                            var contactForSave = new Contact();
                            var accountCount = _context.Accounts.Include(x => x.CostCenter).Where(x => x.CostCenter.Code == "120000").OrderBy(x => x.Code).LastOrDefault();

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
                                _context.Accounts.Add(account);
                            }
                            contactForSave = new Contact
                            {
                                Code = "WC-00001",
                                EnglishName = "Walk-In",
                                CustomerDisplayName = "Walk-In",
                                AccountId = account.Id,
                                Category = "B2C – Business to Client",
                                IsActive = true,
                                IsCashCustomer = true,
                                IsRaw = false,
                                IsCustomer = true

                            };
                            _context.Contacts.Add(contactForSave);
                            _context.SaveChanges();



                        }

                        // Initial query with basic filters
                        var query = _context.Contacts.AsNoTracking()
                            .Where(x => x.IsCustomer == request.isCustomer && x.IsRaw == request.IsRaw);

                        // Consolidate CashCustomer filter
                        query = request.IsCashCustomer ? query.Where(x => x.IsCashCustomer) : query.Where(x => !x.IsCashCustomer);

                        // Consolidate PaymentTerms filter
                        if (request.PaymentTerms2 == "Credit" || request.PaymentTerms2 == "Cash")
                        {
                            query = query.Where(x => x.PaymentTerms == request.PaymentTerms2);
                        }
                        // Consolidate ID filters
                        if (request.CustomerId != null && request.CustomerId != Guid.Empty)
                        {
                            query = query.Where(x => x.Id == request.CustomerId);
                        }
                        if (request.CustomerGroupId != null && request.CustomerGroupId != Guid.Empty)
                        {
                            query = query.Where(x => x.CustomerGroupId == request.CustomerGroupId);
                        }

                        // Consolidate other filters
                        if (request.Category != null)
                        {
                            query = query.Where(x => x.Category == request.Category);
                        }
                        if (request.CustomerCategory != null)
                        {
                            query = query.Where(x => x.CustomerType == request.CustomerCategory);
                        }

                        // Search Term filter with case-insensitive comparisons
                        if (!string.IsNullOrEmpty(request.SearchTerm))
                        {
                            var searchTerm = request.SearchTerm.ToLower();
                            query = query.Where(x =>
                                    EF.Functions.Like(x.CustomerDisplayName, $"%{searchTerm}%") ||
                                    EF.Functions.Like(x.CustomerCode, $"%{searchTerm}%") 
                                // ... other fields
                            );
                        }


                        // Pagination and sorting
                        var count = await query.CountAsync(cancellationToken: cancellationToken);
                        query = query.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize);
                        
                        // Projection
                        var contactList = await query.Select(contact => new ContactLookUpModel
                        {
                            Id = contact.Id,
                            Code = contact.Code,
                            Category = contact.Category,
                            EnglishName = contact.EnglishName,
                            ArabicName = contact.ArabicName,
                            CommercialRegistrationNo = contact.CommercialRegistrationNo,
                            VatNo = contact.VatNo,
                            ContactPerson1 = contact.ContactPerson1,
                            ContactPerson2 = contact.ContactPerson2,
                            ContactNo1 = contact.ContactNo1,
                            ContactNo2 = contact.ContactNo2,
                            Address = contact.Address,
                            AccountId = contact.AccountId,
                            City = contact.City,
                            PaymentTerms = contact.PaymentTerms,
                            Remarks = contact.Remarks,
                            RegistrationDate = contact.RegistrationDate,
                            SupplierType = contact.SupplierType.GetValueOrDefault(),
                            Email = contact.Email,
                            ContactNumber = contact.ContactNumber,
                            Country = contact.Country,
                            DeliveryTerm = contact.DeliveryTerm,
                            CreditDays = contact.CreditDays,
                            CreditLimit = contact.CreditLimit,
                            CreditPeriod = contact.CreditPeriod,
                            IsExpire = contact.IsExpire,
                            IsActive = contact.IsActive,
                            IsCustomer = contact.IsCustomer,
                            Status = contact.Status,
                            Reason = contact.Reason,
                            AdvanceAccountId = contact.AdvanceAccountId,
                            CustomerDisplayName = contact.CustomerDisplayName,
                            RegionId = contact.RegionId,
                            WhatsAppNumber = contact.WhatsAppNumber,
                            Date = contact.Date,
                            CompanyNameEnglish = contact.CompanyNameEnglish,
                            CompanyNameArabic = contact.CompanyNameArabic,
                            CustomerCode = contact.CustomerCode,
                            CustomerType = contact.CustomerType,
                        }).ToListAsync(cancellationToken);

                        //var query = _context.Contacts.AsNoTracking().Where(x => x.IsCustomer == request.isCustomer  && x.IsRaw == request.IsRaw).AsQueryable();

                        //if (request.IsCashCustomer)
                        //{
                        //    query = query.Where(x => x.IsCashCustomer);
                        //}


                        //else
                        //{
                        //    query = query.Where(x => !x.IsCashCustomer);
                        //}

                        //if (request.PaymentTerms2 == "Credit")
                        //{
                        //    query = query.Where(x => x.PaymentTerms == "Credit");
                        //}
                        //if (request.PaymentTerms2 == "Cash")
                        //{
                        //    query = query.Where(x => x.PaymentTerms == "Cash");
                        //}
                        //if (request.CustomerId != null && request.CustomerId != Guid.Empty)
                        //{
                        //    query = query.Where(x => x.Id == request.CustomerId);
                        //}
                        //if (request.CustomerGroupId != null && request.CustomerGroupId != Guid.Empty)
                        //{
                        //    query = query.Where(x => x.CustomerGroupId == request.CustomerGroupId);
                        //}
                        //if (request.Category != null)
                        //{
                        //    query = query.Where(x => x.Category == request.Category);
                        //}
                        //if (request.CustomerCategory != null)
                        //{
                        //    query = query.Where(x => x.CustomerType == request.CustomerCategory);
                        //}

                        //if (!string.IsNullOrEmpty(request.SearchTerm))
                        //{
                        //    var searchTerm = request.SearchTerm.ToLower();
                        //    query = query.Where(x =>
                        //        x.CustomerDisplayName.ToLower().Contains(searchTerm) ||
                        //        x.CustomerCode.ToLower().Contains(searchTerm) ||
                        //        x.Telephone.ToLower().Contains(searchTerm) ||
                        //        x.VatNo.ToLower().Contains(searchTerm) ||
                        //        x.ContactNo1.ToLower().Contains(searchTerm) ||
                        //        x.Code.ToLower().Contains(searchTerm)||
                        //        x.CommercialRegistrationNo.ToLower().Contains(searchTerm) ||
                        //        x.Email.ToLower().Contains(searchTerm) ||
                        //        );

                        //}
                        //query = query.OrderByDescending(x => x.Id);

                        //var count = await query.CountAsync(cancellationToken: cancellationToken);
                        //query = query.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize);

                        //var contactList = new List<ContactLookUpModel>();
                        //foreach (var contact in query)
                        //{
                        //    var lookUpModel = new ContactLookUpModel
                        //    {
                        //        Id = contact.Id,
                        //        Code = contact.Code,
                        //        Category = contact.Category,
                        //        EnglishName = contact.EnglishName,
                        //        ArabicName = contact.ArabicName,
                        //        CommercialRegistrationNo = contact.CommercialRegistrationNo,
                        //        VatNo = contact.VatNo,
                        //        ContactPerson1 = contact.ContactPerson1,
                        //        ContactPerson2 = contact.ContactPerson2,
                        //        ContactNo1 = contact.ContactNo1,
                        //        ContactNo2 = contact.ContactNo2,
                        //        Address = contact.Address,
                        //        AccountId = contact.AccountId,
                        //        City = contact.City,
                        //        PaymentTerms = contact.PaymentTerms,
                        //        Remarks = contact.Remarks,
                        //        RegistrationDate = contact.RegistrationDate,
                        //        SupplierType = contact.SupplierType.GetValueOrDefault(),
                        //        Email = contact.Email,
                        //        ContactNumber = contact.ContactNumber,
                        //        Country = contact.Country,
                        //        DeliveryTerm = contact.DeliveryTerm,
                        //        CreditDays = contact.CreditDays,
                        //        CreditLimit = contact.CreditLimit,
                        //        CreditPeriod = contact.CreditPeriod,
                        //        IsExpire = contact.IsExpire,
                        //        IsActive = contact.IsActive,
                        //        IsCustomer = contact.IsCustomer,
                        //        Status = contact.Status,
                        //        Reason = contact.Reason,
                        //        AdvanceAccountId = contact.AdvanceAccountId,
                        //        CustomerDisplayName = contact.CustomerDisplayName,
                        //        RegionId = contact.RegionId,
                        //        WhatsAppNumber = contact.WhatsAppNumber,
                        //        Date = contact.Date,
                        //        CompanyNameEnglish = contact.CompanyNameEnglish,
                        //        CompanyNameArabic = contact.CompanyNameArabic,
                        //        CustomerCode = contact.CustomerCode,
                        //        CustomerType = contact.CustomerType,


                        //    };
                        //    contactList.Add(lookUpModel);
                        //}



                        return new PagedResult<List<ContactLookUpModel>>
                        {
                            Results = contactList,
                            RowCount = count,
                            PageSize = request.PageSize,
                            CurrentPage = request.PageNumber,
                            PageCount = contactList.Count / request.PageSize
                        };
                    }
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception.Message);
                    throw new ApplicationException("List Error");
                }
            }
        }
    }
}
