using AutoMapper;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Domain.Entities;
using Focus.Domain.Enum;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Org.BouncyCastle.Asn1.X509;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.PaymentVouchers.Queries.GetAdvanceBalance
{
    public class GetSaleToVoucherQuery : IRequest<AdvanceBalanceLookupModel>
    {
        public Guid Id { get; set; }
        public Guid? BranchId { get; set; }
        public bool IsPurchase { get; set; }
        public string FormName { get; set; }

        public class Handler : IRequestHandler<GetSaleToVoucherQuery, AdvanceBalanceLookupModel>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                ILogger<GetSaleToVoucherQuery> logger,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<AdvanceBalanceLookupModel> Handle(GetSaleToVoucherQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    if (request.FormName == "SupplierQuotation")
                    {
                        // Single Proforma Invoice
                        var saleOrder = await _context.PurchaseOrders.AsNoTracking().Include(x => x.Supplier).ThenInclude(x => x.Account).FirstOrDefaultAsync(x => x.Id == request.Id);

                        // Payment Voucher Amount Against Sale Invoice Id 
                        var payamentVoucherAmount = await _context.PaymentVouchers.Where(x => x.DocumentId == request.Id && x.ApprovalStatus == ApprovalStatus.Approved).SumAsync(x => x.Amount);
                        decimal remainingAmount = saleOrder.TotalAmount - payamentVoucherAmount;


                        if (saleOrder == null)
                            throw new NotFoundException("Supplier Quotation Not Found", "");

                        return new AdvanceBalanceLookupModel
                        {
                            AccountId = saleOrder.Supplier.Account.Id,
                            SaleId = saleOrder.Id,
                            NetSaleAmount = saleOrder.TotalAmount,
                            RemainingAmount = remainingAmount,
                            IsSaleInvoice = request.FormName == "SaleInvoice" ? false : true,
                            FormName = request.FormName,
                            IsForm = true,
                        };
                    }
                    else if (request.FormName == "PurchaseOrder")
                    {
                        // Single Proforma Invoice
                        var saleOrder = await _context.PurchaseOrders.AsNoTracking().Include(x => x.Supplier).ThenInclude(x => x.Account).FirstOrDefaultAsync(x => x.Id == request.Id);

                        // Payment Voucher Amount Against Sale Invoice Id 
                        var payamentVoucherAmount = await _context.PaymentVouchers.Where(x => x.DocumentId == request.Id && x.ApprovalStatus == ApprovalStatus.Approved).SumAsync(x => x.Amount);
                        decimal remainingAmount = saleOrder.TotalAmount - payamentVoucherAmount;


                        if (saleOrder == null)
                            throw new NotFoundException("Purchase Order Not Found", "");

                        return new AdvanceBalanceLookupModel
                        {
                            AccountId = saleOrder.Supplier.Account.Id,
                            SaleId = saleOrder.Id,
                            NetSaleAmount = saleOrder.TotalAmount,
                            RemainingAmount = remainingAmount,
                            IsSaleInvoice = request.FormName == "SaleInvoice" ? false : true,
                            FormName = request.FormName,
                            IsForm = true,
                        };
                    }
                    else if (request.FormName == "ServiceSaleOrder")
                    {
                        // Single Proforma Invoice
                        var saleOrder = await _context.SaleOrders.AsNoTracking().Include(x => x.Customer).ThenInclude(x => x.Account).FirstOrDefaultAsync(x => x.Id == request.Id);

                        // Payment Voucher Amount Against Sale Invoice Id 
                        var payamentVoucherAmount = await _context.PaymentVouchers.Where(x => x.DocumentId == request.Id && x.ApprovalStatus == ApprovalStatus.Approved).SumAsync(x => x.Amount);
                        decimal remainingAmount = saleOrder.TotalAmount - payamentVoucherAmount;


                        if (saleOrder == null)
                            throw new NotFoundException("Sale Order Not Found", "");

                        return new AdvanceBalanceLookupModel
                        {
                            AccountId = saleOrder.Customer.Account.Id,
                            SaleId = saleOrder.Id,
                            NetSaleAmount = saleOrder.TotalAmount,
                            RemainingAmount = remainingAmount,
                            IsSaleInvoice = request.FormName == "SaleInvoice" ? false : true,
                            FormName = request.FormName,
                            IsForm = true,
                        };
                    }
                    else if (request.FormName == "ServiceQuotation")
                    {
                        // Single Proforma Invoice
                        var saleOrder = await _context.SaleOrders.AsNoTracking().Include(x => x.Customer).ThenInclude(x => x.Account).FirstOrDefaultAsync(x => x.Id == request.Id);

                        // Payment Voucher Amount Against Sale Invoice Id 
                        var payamentVoucherAmount = await _context.PaymentVouchers.Where(x => x.DocumentId == request.Id && x.ApprovalStatus == ApprovalStatus.Approved).SumAsync(x => x.Amount);
                        decimal remainingAmount = saleOrder.TotalAmount - payamentVoucherAmount;


                        if (saleOrder == null)
                            throw new NotFoundException("Quotation Not Found", "");

                        return new AdvanceBalanceLookupModel
                        {
                            AccountId = saleOrder.Customer.Account.Id,
                            SaleId = saleOrder.Id,
                            NetSaleAmount = saleOrder.TotalAmount,
                            RemainingAmount = remainingAmount,
                            IsSaleInvoice = request.FormName == "SaleInvoice" ? false : true,
                            FormName = request.FormName,
                            IsForm = true,
                        };
                    }
                    else if (request.FormName == "ProfomaInvoice")
                    {
                        // Single Proforma Invoice
                        var sale = await _context.Sales.AsNoTracking().Include(x => x.Customer).ThenInclude(x => x.Account).FirstOrDefaultAsync(x => x.Id == request.Id);

                        // Payment Voucher Amount Against Sale Invoice Id 
                        var payamentVoucherAmount = await _context.PaymentVouchers.Where(x => x.DocumentId == request.Id && x.ApprovalStatus == ApprovalStatus.Approved).SumAsync(x => x.Amount);
                        decimal remainingAmount = sale.TotalAmount - payamentVoucherAmount;


                        if (sale == null)
                            throw new NotFoundException("Sale Not Found", "");

                        return new AdvanceBalanceLookupModel
                        {
                            AccountId = sale.Customer.Account.Id,
                            SaleId = sale.Id,
                            NetSaleAmount = sale.TotalAmount,
                            RemainingAmount = remainingAmount,
                            IsSaleInvoice = request.FormName == "SaleInvoice" ? false : true,
                            FormName = request.FormName,
                            IsForm = true,
                        };
                    }
                    else if (request.IsPurchase)
                    {
                        var purchase = await _context.PurchasePosts.AsNoTracking().Include(x => x.Supplier).ThenInclude(x => x.Account).FirstOrDefaultAsync(x => x.Id == request.Id);

                        decimal remainingAmount = 0;

                        if (purchase == null)
                            throw new NotFoundException("Purchase Not Found", "");

                        // Payment Voucher Amount Against Sale Invoice Id 
                        var payamentVoucherAmount = await _context.PaymentVouchers.Where(x => x.PurchaseInvoice == request.Id && x.ApprovalStatus == ApprovalStatus.Approved).SumAsync(x => x.Amount);
                        var paymentVoucherItems = await _context.PaymentVouchers.Include(x => x.PaymentVoucherItems).Where(x => x.PurchaseInvoice == null).ToListAsync();
                        decimal total = 0;
                        if(paymentVoucherItems.Count > 0)
                        {
                            foreach (var item in paymentVoucherItems)
                            {
                                var pur = item.PaymentVoucherItems.Where(x => x.PurchaseInvoiceId == request.Id).Sum(x => x.PayAmount);
                                total = total + pur;
                            }
                        }

                        remainingAmount = purchase.TotalAmount - (payamentVoucherAmount + total);

                        return new AdvanceBalanceLookupModel
                        {
                            AccountId = purchase.Supplier.Account.Id,
                            SaleId = purchase.Id,
                            NetSaleAmount = purchase.TotalAmount,
                            RemainingAmount = remainingAmount,
                            IsSaleInvoice = request.FormName == "SaleInvoice" ? true : false,
                            IsPurchaseInvoice = request.IsPurchase ? true : false,
                            FormName = request.FormName,
                            IsForm = false
                        };
                    }
                    else if(request.FormName == "SaleInvoice")
                    {
                        // Single Sale Invoice
                        var sale = await _context.Sales.AsNoTracking().Include(x => x.Customer).ThenInclude(x => x.Account).FirstOrDefaultAsync(x => x.Id == request.Id);

                        // Payment Voucher Amount Against Sale Invoice Id 
                        var payamentVoucherAmount = await _context.PaymentVouchers.Where(x => x.SaleInvoice == request.Id && x.ApprovalStatus == ApprovalStatus.Approved).SumAsync(x => x.Amount);

                        decimal remainingAmount = 0;

                        if (sale == null)
                            throw new NotFoundException("Sale Not Found", "");

                        var paymentVoucherItems = await _context.PaymentVouchers.Include(x => x.PaymentVoucherItems).Where(x => x.SaleInvoice == null).ToListAsync();
                        decimal total = 0;
                        if (paymentVoucherItems.Count > 0)
                        {
                            foreach (var item in paymentVoucherItems)
                            {
                                var pur = item.PaymentVoucherItems.Where(x => x.SaleInvoiceId == request.Id).Sum(x => x.PayAmount);
                                total = total + pur;
                            }
                        }

                        remainingAmount = sale.TotalAmount - (payamentVoucherAmount + total);

                        return new AdvanceBalanceLookupModel
                        {
                            AccountId = sale.Customer.Account.Id,
                            SaleId = sale.Id,
                            NetSaleAmount = sale.TotalAmount,
                            RemainingAmount = remainingAmount,
                            IsSaleInvoice = request.FormName == "SaleInvoice" ? true : false,
                            FormName = request.FormName,
                            IsForm = false
                        };
                    }
                    else
                    {
                        return new AdvanceBalanceLookupModel();
                    }
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
