using DocumentFormat.OpenXml.Drawing.Charts;
using Focus.Business.Common;
using Focus.Business.Interface;
using Focus.Business.PurchasePosts.Queries.GetPurchasePostDetail;
using Focus.Business.SaleOrders.Queries.GetSaleOrderList;
using Focus.Business.Users;
using Focus.Domain.Entities;
using Focus.Domain.Interface;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.SaleOrders.Queries.GetSaleOrderTracking
{
    public class SaleOrderTrackingReportQuery : IRequest<List<SaleOrderLookUpModel>>
    {
        public Guid UserId { get; set; }
        public DateTime FromDate { get; set; }
        public bool IsMultiUnit { get; set; }
        public DateTime ToDate { get; set; }

        public class Handler : IRequestHandler<SaleOrderTrackingReportQuery, List<SaleOrderLookUpModel>>
        {
            private readonly IApplicationDbContext _context;
            public readonly ILogger Logger;
            private readonly IMediator _mediator;
            private readonly IUserHttpContextProvider _contextProvider;
            private readonly UserManager<ApplicationUser> _userManager;
            public Handler(IApplicationDbContext context, ILogger<SaleOrderTrackingReportQuery> logger, IMediator mediator, IUserHttpContextProvider contextProvider, UserManager<ApplicationUser> userManager)
            {
                _context = context;
                Logger = logger;
                _mediator = mediator;
                _userManager = userManager;
                _contextProvider = contextProvider;
            }

            public async Task<List<SaleOrderLookUpModel>> Handle(SaleOrderTrackingReportQuery request, CancellationToken cancellationToken)
            {
                try
                {
                   var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == request.UserId.ToString());
                    var query = _context.SaleOrders
                            .AsNoTracking()
                            .Include(x => x.SaleOrderVersions)
                            .Include(x => x.SaleOrderItems)
                            .ThenInclude(x => x.TaxRate)
                            .Include(x => x.SaleOrderItems)
                            .ThenInclude(x => x.Product)
                            .Include(x => x.Customer)
                            .Where(x => x.IsSaleOrderTracking && x.CreatedBy == request.UserId)
                            .AsQueryable();

                    if (request.FromDate != null)
                    {
                        query = query.Where(x => x.Date.Date >= request.FromDate.Date);
                    }
                    if (request.ToDate != null)
                    {
                        query = query.Where(x => x.Date.Date <= request.ToDate.Date.AddDays(1));
                    }
                    var number = 1;
                    var saleOrderList = new List<SaleOrderLookUpModel>();
                    foreach (var saleOrder in query)
                    {
                        var items = new List<SaleOrderItemLookupModel>();
                        foreach (var data in saleOrder.SaleOrderItems)
                        {
                            items.Add(new SaleOrderItemLookupModel
                            {
                                Id = data.Id,
                                ProductNameEn = data.Product.EnglishName,
                                ProductNameAr = data.Product.ArabicName,
                                TaxRateId = data.TaxRateId,
                                TaxRate = data.TaxRate.Rate.ToString(),
                                Discount = data.Discount,
                                Number = number++,
                                FixDiscount = data.FixDiscount,
                                Code = data.Product.Code.ToString(),
                                Quantity = (request.IsMultiUnit && !saleOrder.IsService) ? Convert.ToInt32(Convert.ToInt32(data.Quantity) % Convert.ToInt32(data.Product.UnitPerPack)) : data.Quantity,
                                HighQty = (request.IsMultiUnit && !saleOrder.IsService) ? Convert.ToInt32(Convert.ToInt32(data.Quantity) / Convert.ToInt32(data.Product.UnitPerPack)) : 0,
                                UnitPerPack = data.Product.UnitPerPack,
                                UnitPrice = data.UnitPrice,
                                TaxMethod = data.TaxMethod,
                                Serial = data.Serial,
                                IsSerial = saleOrder.IsService,
                                GuaranteeDate = data.GuaranteeDate,
                                Total = data.Quantity * data.UnitPrice,
                                DiscountAmount = ((saleOrder.TaxMethod == "Inclusive" || saleOrder.TaxMethod == "شامل") ? (data.Discount == 0 ? (data.FixDiscount + (data.FixDiscount * data.TaxRate.Rate / 100)) : (data.Quantity * data.UnitPrice) * data.Discount / 100) : (data.Discount == 0 ? data.FixDiscount : ((data.Quantity * data.UnitPrice) * data.Discount / 100))),
                                BatchNo = data.BatchNo,
                                ExpiryDate = data.ExpiryDate,
                            });
                        }
                        saleOrderList.Add(new SaleOrderLookUpModel
                        {
                            RegistrationNumber = saleOrder.RegistrationNo,
                            EmployeeName = user.FirstName ,
                            Date = saleOrder.Date.ToString("MM/dd/yyyy"),
                            SaleOrderItem = items,
                            NetAmount = !saleOrder.IsDiscountOnTransaction ? (((
                                saleOrder.SaleOrderItems.Sum(x => (x.TaxMethod == "Exclusive" || x.TaxMethod == "غير شامل") ? ((x.Discount == 0 ? ((x.Quantity * x.UnitPrice) - (x.FixDiscount)) : ((x.Quantity * x.UnitPrice) - ((x.Quantity * x.UnitPrice) * x.Discount / 100))) * (100 + x.TaxRate.Rate) / 100) :
                                (x.Discount == 0 ? ((x.Quantity * x.UnitPrice) - (x.FixDiscount + (x.FixDiscount * x.TaxRate.Rate / 100))) : ((x.Quantity * x.UnitPrice) - ((x.Quantity * x.UnitPrice) * x.Discount / 100))))) + saleOrder.Discount))
                                : Math.Round(saleOrder.SaleOrderItems.Sum(x => x.UnitPrice * x.Quantity) + ((saleOrder.TaxMethod == "Exclusive" || saleOrder.TaxMethod == "غير شامل") ? CalculateTotalVat(saleOrder) : 0) - CalculateTransactionLevelDiscount(saleOrder) + saleOrder.Discount, 2),
                        }) ;
                    }

                    return saleOrderList;
                }
                catch (Exception e)
                {
                    Logger.LogError(e.Message);
                    throw new ApplicationException("Unable to process your request please contact support");
                }
            }
            public decimal CalculateTotalVat(SaleOrder purchase)
            {
                var sumQuantity = purchase.SaleOrderItems.Sum(product => product.Quantity);
                var total = purchase.SaleOrderItems.Sum(prod => (((prod.TaxMethod == "Inclusive" || prod.TaxMethod == "شامل") ? ((((prod.Quantity * prod.UnitPrice) - (prod.FixDiscount + (prod.FixDiscount * prod.TaxRate.Rate / 100)) - (prod.Quantity * prod.UnitPrice * prod.Discount / 100)) - ((purchase.IsBeforeTax ? (((prod.Quantity * prod.UnitPrice) * purchase.TransactionLevelDiscount) / 100) : 0))) * prod.TaxRate.Rate) / (100 + prod.TaxRate.Rate) :
                ((((prod.Quantity * prod.UnitPrice) - prod.FixDiscount - (prod.Quantity * prod.UnitPrice * prod.Discount / 100)) - ((purchase.IsBeforeTax && !purchase.IsFixed && purchase.IsDiscountOnTransaction ? (((prod.Quantity * prod.UnitPrice) * purchase.TransactionLevelDiscount) / 100) : (purchase.IsBeforeTax && purchase.IsFixed && purchase.IsDiscountOnTransaction ? (purchase.TransactionLevelDiscount / sumQuantity * prod.Quantity) : 0)))) * prod.TaxRate.Rate) / 100)));

                return total;

            }
            public decimal CalculateTransactionLevelDiscount(SaleOrder purchase)
            {
                var sumQuantity = purchase.SaleOrderItems.Sum(product => product.Quantity);
                var total = (purchase.IsBeforeTax && purchase.IsDiscountOnTransaction) ? (purchase.TaxMethod == "Inclusive" || purchase.TaxMethod == "شامل") ? (purchase.TransactionLevelDiscount * (purchase.SaleOrderItems.Sum(x => (x.UnitPrice * x.Quantity))) / 100) : (purchase.IsFixed ? purchase.TransactionLevelDiscount : purchase.TransactionLevelDiscount * purchase.SaleOrderItems.Sum(x => x.UnitPrice * x.Quantity) / 100) : (purchase.IsFixed ? purchase.TransactionLevelDiscount : purchase.TransactionLevelDiscount * (purchase.SaleOrderItems.Sum(x => x.UnitPrice * x.Quantity) + ((purchase.TaxMethod == "Inclusive" || purchase.TaxMethod == "شامل") ? 0 : CalculateTotalVat(purchase))) / 100);

                return total;
            }
        }
    }
}
