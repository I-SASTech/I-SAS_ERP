using Focus.Business.Contacts.Queries.GetContactDetails;
using Focus.Business.Interface;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Focus.Business.Sales.Queries.GetSaleList;
using System.Collections.Generic;
using Focus.Business.SaleOrders.Queries.NetAmount;
using Focus.Domain.Entities;

namespace Focus.Business.Contacts.Queries.GetContactLedgerDetails
{
    public class GetContactLedgerDetailQuery : IRequest<ContactDetailLookUpModel>
    {
        public Guid Id { get; set; }
        public string DocumentType { get; set; }
        public bool CashCustomer { get; set; }
        public bool IsService { get; set; }
        public bool LastThreeMonth { get; set; }
        public bool IsSupplierQuotation { get; set; }

        public class Handler : IRequestHandler<GetContactLedgerDetailQuery, ContactDetailLookUpModel>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<GetContactLedgerDetailQuery> logger)
            {
                _context = context;
                _logger = logger;
            }
            public async Task<ContactDetailLookUpModel> Handle(GetContactLedgerDetailQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var invoiceList = new List<SaleListLookupModel>();

                    if (request.DocumentType== "SaleInvoice" && request.CashCustomer)
                    {
                        var cashCustomer = await _context.CashCustomer
                            .AsNoTracking()
                            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);

                        var sales = await _context.Sales
                            .AsNoTracking()
                            .Where(x => x.IsService == request.IsService && x.CashCustomerId == cashCustomer.Id && (x.InvoiceType == InvoiceType.Paid || x.InvoiceType == InvoiceType.Credit))
                            .ToListAsync(cancellationToken: cancellationToken);
                        

                        foreach (var invoice in sales)
                        {
                            invoiceList.Add(new SaleListLookupModel()
                            {
                                Id = invoice.Id,
                                RegistrationNumber = invoice.RegistrationNo,
                                Date = invoice.Date,
                                NetAmount = invoice.TotalAmount,

                            });
                        }
                        var contactDetail = new ContactDetailLookUpModel
                        {
                            Id = cashCustomer.Id,
                            Code = cashCustomer.Code,
                            EnglishName = cashCustomer.Name,
                            ArabicName = cashCustomer.NameArabic,
                            Email = cashCustomer.Email,
                            ContactNo1 = cashCustomer.Mobile,
                            CommercialRegistrationNo = cashCustomer.CustomerId,
                            InvoiceList = invoiceList
                        };
                        return contactDetail;
                    }
                    
                    if (request.DocumentType == "SaleReturn" && request.CashCustomer)
                    {
                        var cashCustomer = await _context.CashCustomer
                            .AsNoTracking()
                            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);

                        var sales = await _context.Sales
                            .AsNoTracking()
                            .Where(x => x.IsService == request.IsService && x.CashCustomerId == cashCustomer.Id && x.InvoiceType == InvoiceType.SaleReturn)
                            .ToListAsync(cancellationToken: cancellationToken);

                        foreach (var invoice in sales)
                        {
                            invoiceList.Add(new SaleListLookupModel()
                            {
                                Id = invoice.Id,
                                RegistrationNumber = invoice.RegistrationNo,
                                Date = invoice.Date,
                                NetAmount = invoice.TotalAmount,

                            });
                        }
                        var contactDetail = new ContactDetailLookUpModel
                        {
                            Id = cashCustomer.Id,
                            Code = cashCustomer.Code,
                            EnglishName = cashCustomer.Name,
                            ArabicName = cashCustomer.NameArabic,
                            Email = cashCustomer.Email,
                            ContactNo1 = cashCustomer.Mobile,
                            CommercialRegistrationNo = cashCustomer.CustomerId,
                            InvoiceList = invoiceList
                        };
                        return contactDetail;
                    }
                    
                    var contact = await _context.Contacts.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);
                    var runningBalance = await _context.Transactions.AsNoTracking().Where(x => x.AccountId == contact.AccountId).SumAsync(x => x.Debit - x.Credit, cancellationToken: cancellationToken);

                    if (request.DocumentType == "SaleInvoice" && !request.CashCustomer)
                    {
                        var query = await _context.Sales
                            .AsNoTracking()
                            .Where(x => x.IsService == request.IsService && x.CustomerId == contact.Id && (x.InvoiceType == InvoiceType.Paid || x.InvoiceType == InvoiceType.Credit))
                            .ToListAsync(cancellationToken: cancellationToken);

                        if (request.LastThreeMonth)
                        {
                            DateTime threeMonthsAgo = DateTime.Now.AddMonths(-3).Date;
                            DateTime today = DateTime.Now.Date.AddDays(1);

                            query = query.Where(x => x.Date >= threeMonthsAgo && x.Date <= today).ToList();
                        }
                        foreach (var invoice in query)
                        {
                            invoiceList.Add(new SaleListLookupModel()
                            {
                                Id = invoice.Id,
                                RegistrationNumber = invoice.RegistrationNo,
                                Date = invoice.Date,
                                NetAmount = invoice.TotalAmount,
                            });
                        }
                    }

                    else if(request.DocumentType == "SaleReturn" && !request.CashCustomer)
                    {
                        var query = await _context.Sales
                            .AsNoTracking()
                            .Where(x => x.IsService == request.IsService && x.CustomerId == contact.Id && x.InvoiceType == InvoiceType.SaleReturn)
                            .ToListAsync(cancellationToken: cancellationToken);

                        foreach (var invoice in query)
                        {
                            invoiceList.Add(new SaleListLookupModel()
                            {
                                Id = invoice.Id,
                                RegistrationNumber = invoice.RegistrationNo,
                                Date = invoice.Date,
                                NetAmount = invoice.TotalAmount,

                            });
                        }
                    }

                    else if (request.DocumentType == "ProformaInvoices")
                    {
                        var query = await _context.Sales
                            .AsNoTracking()
                            .Where(x => x.IsService == request.IsService && x.CustomerId == contact.Id && x.InvoiceType == InvoiceType.ProformaInvoice)
                            .ToListAsync(cancellationToken: cancellationToken);
                        if (request.LastThreeMonth)
                        {
                            DateTime threeMonthsAgo = DateTime.Now.AddMonths(-3).Date;
                            DateTime today = DateTime.Now.Date.AddDays(1);

                            query = query.Where(x => x.Date >= threeMonthsAgo && x.Date <= today).ToList();
                        }

                        foreach (var invoice in query)
                        {
                            invoiceList.Add(new SaleListLookupModel()
                            {
                                Id = invoice.Id,
                                RegistrationNumber = invoice.RegistrationNo,
                                Date = invoice.Date,
                                NetAmount = invoice.TotalAmount,

                            });
                        }
                    }
                    else if (request.DocumentType == "PurchaseOrder")
                    {
                        var query = await _context.Sales
                            .AsNoTracking()
                            .Where(x => x.IsService == request.IsService && x.CustomerId == contact.Id && x.InvoiceType == InvoiceType.PurchaseOrder)
                            .ToListAsync(cancellationToken: cancellationToken);
                        if (request.LastThreeMonth)
                        {
                            DateTime threeMonthsAgo = DateTime.Now.AddMonths(-3).Date;
                            DateTime today = DateTime.Now.Date.AddDays(1);

                            query = query.Where(x => x.Date >= threeMonthsAgo && x.Date <= today).ToList();
                        }

                        foreach (var invoice in query)
                        {
                            invoiceList.Add(new SaleListLookupModel()
                            {
                                Id = invoice.Id,
                                RegistrationNumber = invoice.RegistrationNo,
                                Date = invoice.Date,
                                NetAmount = invoice.TotalAmount,

                            });
                        }
                    }

                    else if (request.DocumentType == "Quotation" || request.DocumentType == "ServiceQuotation")
                    {
                        var query = await _context.SaleOrders
                            .AsNoTracking()
                            .Where(x => x.IsService == request.IsService && x.IsQuotation && x.CustomerId == contact.Id)
                            .ToListAsync(cancellationToken: cancellationToken);

                        if (request.LastThreeMonth)
                        {
                            DateTime threeMonthsAgo = DateTime.Now.AddMonths(-3).Date;
                            DateTime today = DateTime.Now.Date.AddDays(1);

                            query = query.Where(x => x.Date >= threeMonthsAgo && x.Date <= today).ToList();
                        }

                        var netAmount = new TotalAmount();
                        foreach (var invoice in query)
                        {
                            invoiceList.Add(new SaleListLookupModel()
                            {
                                Id = invoice.Id,
                                RegistrationNumber = invoice.RegistrationNo,
                                Date = invoice.Date,
                                NetAmount = netAmount.CalculateTotalAmount(invoice)
                            });
                        }
                    }

                    else if(request.DocumentType == "SaleOrder" || request.DocumentType == "ServiceSaleOrder")
                    {
                        var query = await _context.SaleOrders
                            .AsNoTracking()
                            .Where(x => x.IsService == request.IsService && !x.IsQuotation && x.CustomerId == contact.Id)
                            .ToListAsync(cancellationToken: cancellationToken);

                        if (request.LastThreeMonth)
                        {
                            DateTime threeMonthsAgo = DateTime.Now.AddMonths(-3).Date;
                            DateTime today = DateTime.Now.Date;

                            query = query.Where(x => x.Date >= threeMonthsAgo && x.Date <= today).ToList();
                        }

                        var netAmount = new TotalAmount();
                        foreach (var invoice in query)
                        {
                            invoiceList.Add(new SaleListLookupModel()
                            {
                                Id = invoice.Id,
                                RegistrationNumber = invoice.RegistrationNo,
                                Date = invoice.Date,
                                NetAmount = invoice.TotalAmount,

                            });
                        }
                    }

                    else if (request.DocumentType == "PurchaseInvoice")
                    {
                        var query = await _context.PurchasePosts
                            .AsNoTracking()
                            .Where(x => x.SupplierId == contact.Id && !x.IsPurchaseReturn)
                            .ToListAsync(cancellationToken: cancellationToken);

                        if (request.LastThreeMonth)
                        {
                            DateTime threeMonthsAgo = DateTime.Now.AddMonths(-3).Date;
                            DateTime today = DateTime.Now.Date.AddDays(1);

                            query = query.Where(x => x.Date >= threeMonthsAgo && x.Date <= today).ToList();
                        }

                        var netAmount = new TotalAmount();
                        foreach (var invoice in query)
                        {
                            invoiceList.Add(new SaleListLookupModel()
                            {
                                Id = invoice.Id,
                                RegistrationNumber = invoice.RegistrationNo,
                                Date = invoice.Date,
                                NetAmount = invoice.TotalAmount,

                            });
                        }
                    }
                    else if (request.DocumentType == "PurchaseReturn")
                    {
                        var query = await _context.PurchasePosts
                            .AsNoTracking()
                            .Where(x => x.SupplierId == contact.Id && x.IsPurchaseReturn)
                            .ToListAsync(cancellationToken: cancellationToken);

                        if (request.LastThreeMonth)
                        {
                            DateTime threeMonthsAgo = DateTime.Now.AddMonths(-3).Date;
                            DateTime today = DateTime.Now.Date.AddDays(1);

                            query = query.Where(x => x.Date >= threeMonthsAgo && x.Date <= today).ToList();
                        }

                        var netAmount = new TotalAmount();
                        foreach (var invoice in query)
                        {
                            invoiceList.Add(new SaleListLookupModel()
                            {
                                Id = invoice.Id,
                                RegistrationNumber = invoice.RegistrationNo,
                                Date = invoice.Date,
                                NetAmount = invoice.TotalAmount,

                            });
                        }
                    }
                    else if (request.DocumentType == "GoodReceive")
                    {
                        var query = await _context.GoodReceives
                            .AsNoTracking()
                            .Where(x => x.SupplierId == contact.Id )
                            .ToListAsync(cancellationToken: cancellationToken);

                       

                        if (request.LastThreeMonth)
                        {
                            DateTime threeMonthsAgo = DateTime.Now.AddMonths(-3).Date;
                            DateTime today = DateTime.Now.Date.AddDays(1);

                            query = query.Where(x => x.Date >= threeMonthsAgo && x.Date <= today).ToList();
                        }

                        var netAmount = new TotalAmount();
                        foreach (var invoice in query)
                        {
                            invoiceList.Add(new SaleListLookupModel()
                            {
                                Id = invoice.Id,
                                RegistrationNumber = invoice.RegistrationNo,
                                Date = invoice.Date,
                                NetAmount = invoice.TotalAmount,

                            });
                        }
                    }

                    else if (request.DocumentType == "PurchaseOrders")
                    {
                        var query = await _context.PurchaseOrders
                            .AsNoTracking()
                            .Where(x => x.SupplierId == contact.Id)
                            .ToListAsync(cancellationToken: cancellationToken);

                        if(request.IsSupplierQuotation)
                        {
                            query = query.Where(x => x.DocumentType == "SupplierQuotation" && x.SupplierId == contact.Id).ToList();
                        }
                        else
                        {
                            query = query.Where(x => x.DocumentType != "SupplierQuotation" && x.SupplierId == contact.Id).ToList();
                        }

                        if (request.LastThreeMonth)
                        {
                            DateTime threeMonthsAgo = DateTime.Now.AddMonths(-3).Date;
                            DateTime today = DateTime.Now.Date.AddDays(1);

                            query = query.Where(x => x.Date >= threeMonthsAgo && x.Date <= today).ToList();
                        }

                        var netAmount = new TotalAmount();
                        foreach (var invoice in query)
                        {
                            invoiceList.Add(new SaleListLookupModel()
                            {
                                Id = invoice.Id,
                                RegistrationNumber = invoice.RegistrationNo,
                                Date = invoice.Date,
                                NetAmount = invoice.TotalAmount,

                            });
                        }
                    }

                    //else if (request.DocumentType == "DeliveryChallan")
                    //{
                    //    var query = await _context.DeliveryChallans
                    //        .AsNoTracking()
                    //        .Where(x => x.IsService == request.IsService &&  x.CustomerId == contact.Id)
                    //        .ToListAsync(cancellationToken: cancellationToken);

                    //    var netAmount = new TotalAmount();
                    //    foreach (var invoice in query)
                    //    {
                    //        invoiceList.Add(new SaleListLookupModel()
                    //        {
                    //            Id = invoice.Id,
                    //            RegistrationNumber = invoice.RegistrationNo,
                    //            Date = invoice.Date,
                    //            NetAmount = invoice.TotalAmount,

                    //        });
                    //    }
                    //}

                    return new ContactDetailLookUpModel
                    {
                        Id = contact.Id,
                        Code = contact.Code,
                        Prefix = contact.Prefix,
                        EnglishName = contact.EnglishName,
                        ArabicName = contact.ArabicName,
                        Telephone = contact.Telephone,
                        Email = contact.Email,
                        ContactNo1 = contact.ContactNo1,
                        CommercialRegistrationNo = contact.CommercialRegistrationNo,
                        VatNo = contact.VatNo,
                        RunningBalance = runningBalance < 0 ? "Cr " + Math.Abs(runningBalance).ToString("#,##0.00") : "Dr " + Math.Abs(runningBalance).ToString("#,##0.00"),
                        InvoiceList = invoiceList
                    };
                }
                catch (Exception e)
                {
                    _logger.LogError(e.Message);

                    throw new ApplicationException("Unable to process your request please contact support");
                }

            }
            public decimal CalculateTotalVat(ICollection<SaleItem> invoice, bool IsBeforeTax, bool IsFixed, bool IsDiscountOnTransaction, decimal TransactionLevelDiscount)
            {
                var sumQuantity = invoice.AsEnumerable().Sum(product => product.IsFree ? 0 : product.Quantity);
                var total = invoice.AsEnumerable().Sum(prod => (prod.IsFree ? 0 : ((prod.TaxMethod == "Inclusive" || prod.TaxMethod == "شامل") ? ((((prod.Quantity * prod.UnitPrice) - (prod.FixDiscount + (prod.FixDiscount * prod.TaxRate.Rate / 100)) - (prod.Quantity * prod.UnitPrice * prod.Discount / 100)) - (prod.IsFree ? 0 : (IsBeforeTax ? (((prod.Quantity * prod.UnitPrice) * TransactionLevelDiscount) / 100) : 0))) * prod.TaxRate.Rate) / (100 + prod.TaxRate.Rate) :
                ((((prod.Quantity * prod.UnitPrice) - prod.FixDiscount - (prod.Quantity * prod.UnitPrice * prod.Discount / 100)) - (prod.IsFree ? 0 : (IsBeforeTax && !IsFixed && IsDiscountOnTransaction ? (((prod.Quantity * prod.UnitPrice) * TransactionLevelDiscount) / 100) : (IsBeforeTax && IsFixed && IsDiscountOnTransaction ? (TransactionLevelDiscount / sumQuantity * prod.Quantity) : 0)))) * prod.TaxRate.Rate) / 100)));

                return total;
            }
            public decimal CalculateTransactionLevelDiscount(ICollection<SaleItem> sale, bool IsBeforeTax, bool IsFixed, bool IsDiscountOnTransaction, decimal TransactionLevelDiscount, string TaxMethod)
            {
                var sumQuantity = sale.AsEnumerable().Sum(product => product.IsFree ? 0 : product.Quantity);
                var total = (IsBeforeTax && IsDiscountOnTransaction) ? (TaxMethod == "Inclusive" || TaxMethod == "شامل") ? (TransactionLevelDiscount * (sale.AsEnumerable().Sum(x => x.IsFree ? 0 : (x.UnitPrice * x.Quantity))) / 100) : (IsFixed ? TransactionLevelDiscount : TransactionLevelDiscount * sale.AsEnumerable().Sum(x => x.IsFree ? 0 : x.UnitPrice * x.Quantity) / 100) : (IsFixed ? TransactionLevelDiscount : TransactionLevelDiscount * (sale.AsEnumerable().Sum(x => x.IsFree ? 0 : x.UnitPrice * x.Quantity) + ((TaxMethod == "Inclusive" || TaxMethod == "شامل") ? 0 : CalculateTotalVat(sale, IsBeforeTax, IsFixed, IsDiscountOnTransaction, TransactionLevelDiscount))) / 100);

                return total;
            }
        }
    }
}
