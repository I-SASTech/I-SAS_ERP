using Focus.Business.Interface;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.PurchasePosts.Queries.GetPurchasePostDetail
{
    public class SupplierPurchaseReportQuery : IRequest<List<PurchasePostLookupModel>>
    {
        public Guid SupplierId { get; set; }
        public bool IsMultiUnit { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string BranchId { get; set; }

        public class Handler : IRequestHandler<SupplierPurchaseReportQuery, List<PurchasePostLookupModel>>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private readonly IMediator _mediator;

            public Handler(IApplicationDbContext context, ILogger<SupplierPurchaseReportQuery> logger, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
            }

            public async Task<List<PurchasePostLookupModel>> Handle(SupplierPurchaseReportQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var po = await _context.PurchasePosts
                        .AsNoTracking()
                        .Include(x => x.PurchasePostItems)
                        .Include(x => x.PurchasePostItems)
                        .ThenInclude(x => x.TaxRate)
                        .Include(x => x.PurchasePostItems)
                        .ThenInclude(x => x.Product)
                        .Include(x => x.Supplier)
                        .Where(x => x.SupplierId == request.SupplierId).ToListAsync(cancellationToken: cancellationToken);

                    if (!string.IsNullOrEmpty(request.BranchId))
                    {
                        var branchIdList = new List<string>(request.BranchId.Split(','));
                        po = po.Where(x => branchIdList.Contains(x.BranchId.ToString())).ToList();
                    }

                    if (request.FromDate != default)
                    {
                        po = po.Where(x => x.Date.Date >= request.FromDate.Date).ToList();
                    }
                    if (request.ToDate != default)
                    {
                        po = po.Where(x => x.Date.Date <= request.ToDate.Date.AddDays(1)).ToList();
                    }

                    //if (request.FromDate != null)
                    //{
                    //    var date = DateTime.Parse(request.FromDate.Value.ToString("yyyy/MM/dd"));
                    //}
                    //var toDate = '';
                    //if (request.ToDate != null)
                    //{
                    //    toDate = DateTime.Parse(request.ToDate.Value.ToString("yyyy/MM/dd"));
                    //}

                    //po = po.Where(x => x.Date.Date >= request.FromDate && x.Date.Date <= toDate).ToList();


                    //decimal discount = 0;
                    //decimal amountWithDiscount = 0;


                    var list = new List<PurchasePostLookupModel>();


                    foreach (var item in po)
                    {
                        var items = new List<PurchasePostItemLookupModel>();
                        foreach (var data in item.PurchasePostItems)
                        {
                            items.Add(new PurchasePostItemLookupModel
                            {
                                Id = data.Id,
                                ProductId = data.ProductId,
                                ProductName = string.IsNullOrEmpty(data.Product?.DisplayNameForPrint) ? data.Product?.EnglishName : data.Product?.DisplayNameForPrint,
                                ProductNameArabic = string.IsNullOrEmpty(data.Product?.DisplayNameForPrint) ? data.Product?.ArabicName : data.Product?.DisplayNameForPrint,
                                TaxRateId = data.TaxRateId,
                                TaxRate = data.TaxRate.Rate,
                                Discount = data.Discount,
                                FixDiscount = data.FixDiscount,
                                Quantity = (request.IsMultiUnit && !data.IsService) ? Convert.ToInt32(Convert.ToInt32(data.Quantity) % Convert.ToInt32(data.UnitPerPack)) : data.Quantity,
                                HighQty = (request.IsMultiUnit && !data.IsService) ? Convert.ToInt32(Convert.ToInt32(data.Quantity) / Convert.ToInt32(data.UnitPerPack)) : 0,
                                UnitPerPack = data.UnitPerPack ?? 0,
                                RemainingQuantity = data.RemainingQuantity,
                                UnitPrice = data.UnitPrice,
                                TaxMethod = data.TaxMethod,
                                PurchaseId = data.PurchasePostId,
                                SerialNo = data.SerialNo,
                                IsService = data.IsService,
                                GuaranteeDate = data.GuaranteeDate,
                                Total = data.Quantity * data.UnitPrice,
                                DiscountAmount = ((item.TaxMethod == "Inclusive" || item.TaxMethod == "شامل") ? (data.Discount == 0 ? (data.FixDiscount + (data.FixDiscount * data.TaxRate.Rate / 100)) : (data.Quantity * data.UnitPrice) * data.Discount / 100) : (data.Discount == 0 ? data.FixDiscount : ((data.Quantity * data.UnitPrice) * data.Discount / 100))),
                                BatchNo = data.BatchNo,
                                ExpiryDate = data.ExpiryDate,
                            });
                        }
                        list.Add(new PurchasePostLookupModel()
                        {
                            InvoiceNo = item.RegistrationNo,
                            Dates = item.Date.ToString("MM/dd/yyyy"),
                            PurchasePostItems = items,
                            NetAmount = !item.IsDiscountOnTransaction ? (((
                                item.PurchasePostItems.Sum(x => (x.TaxMethod == "Exclusive" || x.TaxMethod == "غير شامل") ? ((x.Discount == 0 ? ((x.Quantity * x.UnitPrice) - (x.FixDiscount)) : ((x.Quantity * x.UnitPrice) - ((x.Quantity * x.UnitPrice) * x.Discount / 100))) * (100 + x.TaxRate.Rate) / 100) :
                                (x.Discount == 0 ? ((x.Quantity * x.UnitPrice) - (x.FixDiscount + (x.FixDiscount * x.TaxRate.Rate / 100))) : ((x.Quantity * x.UnitPrice) - ((x.Quantity * x.UnitPrice) * x.Discount / 100))))) + item.Discount))
                                : Math.Round(item.PurchasePostItems.Sum(x => x.UnitPrice * x.Quantity) + ((item.TaxMethod == "Exclusive" || item.TaxMethod == "غير شامل") ? CalculateTotalVat(item) : 0) - CalculateTransactionLevelDiscount(item) + item.Discount, 2),
                        });
                    }
                    return list;
                }
                catch (Exception e)
                {
                    _logger.LogError(e.Message);
                    throw new ApplicationException("Unable to process your request please contact support");
                }
            }
            public decimal CalculateTotalVat(PurchasePost purchase)
            {
                var sumQuantity = purchase.PurchasePostItems.Sum(product => product.Quantity);
                var total = purchase.PurchasePostItems.Sum(prod => (((prod.TaxMethod == "Inclusive" || prod.TaxMethod == "شامل") ? ((((prod.Quantity * prod.UnitPrice) - (prod.FixDiscount + (prod.FixDiscount * prod.TaxRate.Rate / 100)) - (prod.Quantity * prod.UnitPrice * prod.Discount / 100)) - ((purchase.IsBeforeTax ? (((prod.Quantity * prod.UnitPrice) * purchase.TransactionLevelDiscount) / 100) : 0))) * prod.TaxRate.Rate) / (100 + prod.TaxRate.Rate) :
                ((((prod.Quantity * prod.UnitPrice) - prod.FixDiscount - (prod.Quantity * prod.UnitPrice * prod.Discount / 100)) - ((purchase.IsBeforeTax && !purchase.IsFixed && purchase.IsDiscountOnTransaction ? (((prod.Quantity * prod.UnitPrice) * purchase.TransactionLevelDiscount) / 100) : (purchase.IsBeforeTax && purchase.IsFixed && purchase.IsDiscountOnTransaction ? (purchase.TransactionLevelDiscount / sumQuantity * prod.Quantity) : 0)))) * prod.TaxRate.Rate) / 100)));

                return total;

            }
            public decimal CalculateTransactionLevelDiscount(PurchasePost purchase)
            {
                var sumQuantity = purchase.PurchasePostItems.Sum(product => product.Quantity);
                var total = (purchase.IsBeforeTax && purchase.IsDiscountOnTransaction) ? (purchase.TaxMethod == "Inclusive" || purchase.TaxMethod == "شامل") ? (purchase.TransactionLevelDiscount * (purchase.PurchasePostItems.Sum(x => (x.UnitPrice * x.Quantity))) / 100) : (purchase.IsFixed ? purchase.TransactionLevelDiscount : purchase.TransactionLevelDiscount * purchase.PurchasePostItems.Sum(x => x.UnitPrice * x.Quantity) / 100) : (purchase.IsFixed ? purchase.TransactionLevelDiscount : purchase.TransactionLevelDiscount * (purchase.PurchasePostItems.Sum(x => x.UnitPrice * x.Quantity) + ((purchase.TaxMethod == "Inclusive" || purchase.TaxMethod == "شامل") ? 0 : CalculateTotalVat(purchase))) / 100);

                return total;
            }
        }

    }

}
