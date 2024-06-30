using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Interface;
using Focus.Business.Reports.SaleInvoiceReport;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace Focus.Business.Reports.PurchaseInvoiceReport
{
    public class GetPurchaseInvoiceQuery : IRequest<List<SaleInvoiceModel>>
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string BranchId { get; set; }
        public string CustomerId { get; set; }

        public class Handler : IRequestHandler<GetPurchaseInvoiceQuery, List<SaleInvoiceModel>>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<GetPurchaseInvoiceQuery> logger)
            {
                _context = context;
                _logger = logger;
            }
            public async Task<List<SaleInvoiceModel>> Handle(GetPurchaseInvoiceQuery request, CancellationToken cancellationToken)
            {
                try
                {

                    var list = await _context.PurchasePosts
                        .Where(x => x.Date >= request.FromDate.Date && x.Date <= request.ToDate.Date.AddDays(1))
                        .Include(x => x.PurchasePostItems)
                        .Include(x => x.Supplier)
                        .Select(x => new SaleInvoiceModel
                        {
                            InvoiceNo = x.RegistrationNo,
                            Date = x.Date,
                            EnglishName = x.Supplier == null ? "" : x.Supplier.CustomerDisplayName,
                            ArabicName = x.Supplier.ArabicName,
                            IsSaleReturnPost = x.IsPurchaseReturn,
                            Amount = x.TotalWithOutAmount,
                            TotalAmount = x.TotalAmount,
                            OverAllDiscount = x.Discount,
                            Discount = x.DiscountAmount,
                            VATamount = x.VatAmount,
                            BranchId = x.BranchId,
                            SupplierId = x.SupplierId
                        }).ToListAsync(cancellationToken: cancellationToken);


                    if (!string.IsNullOrEmpty(request.CustomerId) && request.CustomerId != "null" && request.CustomerId != "undefined")
                    {
                        list = list.Where(x => x.SupplierId == Guid.Parse(request.CustomerId)).ToList();
                    }


                    if (!string.IsNullOrEmpty(request.BranchId))
                    {
                        var branchIdList = new List<string>(request.BranchId.Split(','));
                        list = list.Where(x => branchIdList.Contains(x.BranchId.ToString())).ToList();
                    }

                    return list;
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception.Message);
                    throw new ApplicationException(exception.Message);
                }
            }


        }
    }
}
