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
using Focus.Domain.Enum;

namespace Focus.Business.StockAdjustments.Queries.GetStockAdjustmentList
{
    public class GetStockAdjustmentListQuery : PagedRequest, IRequest<PagedResult<List<StockAdjustmentLookUpModel>>>
    {
        public string SearchTerm { get; set; }
        public bool Status { get; set; }
        public bool IsDropDown { get; set; }
        public StockAdjustmentType StockAdjustmentType { get;  set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public Guid? BranchId { get; set; }

        public class Handler : IRequestHandler<GetStockAdjustmentListQuery, PagedResult<List<StockAdjustmentLookUpModel>>>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<GetStockAdjustmentListQuery> logger)
            {
                _context = context;
                _logger = logger;
            }

            public async Task<PagedResult<List<StockAdjustmentLookUpModel>>> Handle(GetStockAdjustmentListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    if (request.IsDropDown)
                    {
                        var po = _context.StockAdjustments
                            .AsNoTracking()
                            .Include(x => x.StockAdjustmentDetails)
                            .Include(x => x.TaxRate)
                            .Include(x => x.Warehouse)
                            .Where(x => x.StockAdjustmentType == request.StockAdjustmentType && x.isDraft == request.Status)
                            .AsQueryable();

                        if (request.BranchId != null)
                        {
                            po = po.Where(x => x.BranchId == request.BranchId);
                        }


                        var stockAdjustmentList = new List<StockAdjustmentLookUpModel>();
                        foreach (var stockAdjustment in po)
                        {
                            var sOrder = StockAdjustmentLookUpModel.Create(stockAdjustment);
                            stockAdjustmentList.Add(sOrder);
                        }
                        return new PagedResult<List<StockAdjustmentLookUpModel>>
                        {
                            Results = stockAdjustmentList
                        };

                    }
                    else
                    {
                        var query = _context.StockAdjustments
                            .AsNoTracking()
                            .Include(x=>x.Warehouse)
                            .Include(x=>x.TaxRate)
                            .Include(x => x.StockAdjustmentDetails)
                            .Where(x => x.StockAdjustmentType == request.StockAdjustmentType && x.isDraft == request.Status)
                            .AsQueryable();

                        if (request.BranchId != null)
                        {
                            query = query.Where(x => x.BranchId == request.BranchId);
                        }

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
                                x.Code.Contains(searchTerm) || x.Warehouse.Name.Contains(searchTerm) ||x.Date.ToString().Contains(searchTerm) );

                        }
                        query = query.OrderByDescending(x => x.Code);
                        var count = await query.CountAsync(cancellationToken: cancellationToken);
                        query = query.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize);

                        var stockAdjustmentList = new List<StockAdjustmentLookUpModel>();
                        foreach (var stockAdjustment in query)
                        {
                            var sOrder = StockAdjustmentLookUpModel.Create(stockAdjustment);
                            //sOrder.WarehouseName=_context.Warehouses.FirstOrDefault(x => x.Id == sOrder.WarehouseId).Name;
                            stockAdjustmentList.Add(sOrder);
                        }

                        return new PagedResult<List<StockAdjustmentLookUpModel>>
                        {
                            Results = stockAdjustmentList,
                            RowCount = count,
                            PageSize = request.PageSize,
                            CurrentPage = request.PageNumber,
                            PageCount = stockAdjustmentList.Count / request.PageSize
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
