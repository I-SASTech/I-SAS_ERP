using Focus.Business.Common;
using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.Purchases.Queries.GetPurchaseOrderList
{
    public class GetPurchaseListQuery : PagedRequest, IRequest<PagedResult<List<PurchaseLookUpModel>>>
    {
        public string SearchTerm { get; set; }
        public bool IsDropDown { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

        public class Handler : IRequestHandler<GetPurchaseListQuery, PagedResult<List<PurchaseLookUpModel>>>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<GetPurchaseListQuery> logger)
            {
                _context = context;
                _logger = logger;
            }

            public async Task<PagedResult<List<PurchaseLookUpModel>>> Handle(GetPurchaseListQuery request, CancellationToken cancellationToken)
            {
                try
                {

                    if (request.IsDropDown)
                    {
                        var purchaseOrders = _context.Purchases.AsQueryable().AsNoTracking()
                            .Include(x=>x.PurchaseItems)
                            .Include(x => x.Supplier);
                        var purchaseOrderList = new List<PurchaseLookUpModel>();
                        foreach (var purchaseOrder in purchaseOrders)
                        {
                            var sOrder = PurchaseLookUpModel.Create(purchaseOrder);
                            purchaseOrderList.Add(sOrder);
                        }
                        return new PagedResult<List<PurchaseLookUpModel>>
                        {
                            Results = purchaseOrderList
                        };

                    }
                    else
                    {
                        var query = _context.Purchases
                            .Where(x=>x.IsPost==false)
                            .AsNoTracking()
                            .Include(x=>x.PurchaseItems)
                            .ThenInclude(x=>x.TaxRate)
                            .Include(x => x.PurchaseItems)
                            .ThenInclude(x => x.Product)
                            .Include(x=>x.Supplier)
                            .AsQueryable();

                        if (request.FromDate != null)
                        {
                            query = query.Where(x => x.Date.Date >= request.FromDate);
                        }
                        if (request.ToDate != null)
                        {
                            query = query.Where(x => x.Date.Date <= request.ToDate);
                        }

                        if (!string.IsNullOrEmpty(request.SearchTerm))
                        {
                            var searchTerm = request.SearchTerm;
                           
                           
                                query = query.Where(x =>
                                    x.RegistrationNo.Contains(searchTerm)||
                                    x.Supplier.EnglishName.Contains(searchTerm) ||
                                    x.Supplier.ArabicName.Contains(searchTerm) ||
                                     x.Date.ToString("dd/MM/yyyy").Contains(searchTerm));

                        }
                        query = query.OrderBy(x => x.RegistrationNo);
                        var count = await query.CountAsync(cancellationToken: cancellationToken);
                        query = query.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize);
                        
                        var purchaseOrderList = new List<PurchaseLookUpModel>();
                        foreach (var purchaseOrder in query)
                        {
                            var sOrder = PurchaseLookUpModel.Create(purchaseOrder);
                            purchaseOrderList.Add(sOrder);
                        }

                        return new PagedResult<List<PurchaseLookUpModel>>
                        {
                            Results = purchaseOrderList,
                            RowCount = count,
                            PageSize = request.PageSize,
                            CurrentPage = request.PageNumber,
                            PageCount = purchaseOrderList.Count / request.PageSize
                        };
                    }

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
