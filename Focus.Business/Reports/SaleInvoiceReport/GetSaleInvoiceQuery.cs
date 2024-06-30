using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Interface;

using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.Reports.SaleInvoiceReport
{
    public class GetSaleInvoiceQuery : IRequest<List<SaleInvoiceModel>>
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string BranchId { get; set; }
        public string CustomerId { get; set; }

        public class Handler : IRequestHandler<GetSaleInvoiceQuery, List<SaleInvoiceModel>>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context,
                ILogger<GetSaleInvoiceQuery> logger)
            {
                _context = context;
                _logger = logger;

            }
            public async Task<List<SaleInvoiceModel>> Handle(GetSaleInvoiceQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var list =await _context.Sales
                                .Where(x => x.Date >= request.FromDate.Date && x.Date <= request.ToDate.Date.AddDays(1) && (x.InvoiceType == Domain.Entities.InvoiceType.Paid || x.InvoiceType == Domain.Entities.InvoiceType.Credit || x.InvoiceType == Domain.Entities.InvoiceType.SaleReturn))
                                             .Select(x => new SaleInvoiceModel  
                                             {
                                                 InvoiceNo = x.RegistrationNo,
                                                 Date = x.Date,
                                                 CustomerName = x.Customer==null?"":x.Customer.CustomerDisplayName,
                                                 IsSaleReturnPost = x.IsSaleReturnPost,
                                                 Amount = x.TotalWithOutAmount,
                                                 TotalAmount = x.TotalAmount,
                                                 Discount = x.DiscountAmount,
                                                 TotalWithOutAmount = x.TotalWithOutAmount,
                                                 VATamount = x.VatAmount,
                                                 DocumentType=x.DocumentType,
                                                 BranchId = x.BranchId,
                                                 CustomerId = x.CustomerId,
                                             }).ToListAsync(cancellationToken: cancellationToken);

                    if(!string.IsNullOrEmpty(request.CustomerId) && request.CustomerId != "null" && request.CustomerId != "undefined")
                    {
                        list = list.Where(x => x.CustomerId == Guid.Parse(request.CustomerId)).ToList();
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
