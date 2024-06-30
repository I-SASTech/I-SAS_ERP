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

namespace Focus.Business.WareHouseTransfers.Queries.GetWareHouseTransferList
{
    public class GetWareHouseTransferListQuery : PagedRequest, IRequest<PagedResult<List<WareHouseTransferLookUpModel>>>
    {
        public string SearchTerm { get; set; }
        public bool IsDropDown { get; set; }
        public ApprovalStatus Status { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public Guid? BranchId { get; set; }

        public class Handler : IRequestHandler<GetWareHouseTransferListQuery, PagedResult<List<WareHouseTransferLookUpModel>>>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<GetWareHouseTransferListQuery> logger)
            {
                _context = context;
                _logger = logger;
            }

            public async Task<PagedResult<List<WareHouseTransferLookUpModel>>> Handle(GetWareHouseTransferListQuery request, CancellationToken cancellationToken)
            {
                try
                {

                    if (request.IsDropDown)
                    {
                        var wareHouseTransferOrders = _context.WareHouseTransfers
                            .AsNoTracking()
                            .Include(x => x.WareHouseTransferItems)
                            .AsQueryable();

                        if (request.BranchId != null)
                        {
                            wareHouseTransferOrders = wareHouseTransferOrders.Where(x => x.BranchId == request.BranchId);
                        }

                        var wareHouseTransferOrderList = new List<WareHouseTransferLookUpModel>();
                        foreach (var wareHouseTransferOrder in wareHouseTransferOrders)
                        {
                            var lookUpModel = new WareHouseTransferLookUpModel
                            {
                                Id = wareHouseTransferOrder.Id,
                                Code = wareHouseTransferOrder.Code,
                                FromWareHouse = wareHouseTransferOrder.Warehouse.Name,
                                ToWareHouse = _context.WareHouseTransfers.FirstOrDefault(x => x.ToWareHouseId == wareHouseTransferOrder.ToWareHouseId)?.Warehouse.Name,
                                Date = wareHouseTransferOrder.Date.ToString("MM/dd/yyyy hh:mm tt")
                            };
                            wareHouseTransferOrderList.Add(lookUpModel);
                        }
                        return new PagedResult<List<WareHouseTransferLookUpModel>>
                        {
                            Results = wareHouseTransferOrderList
                        };

                    }
                    else
                    {
                        var query = _context.WareHouseTransfers
                            .AsNoTracking()
                            .Include(x=>x.Warehouse)
                            .Include(x=>x.WareHouseTransferItems)
                            .AsQueryable();

                        var branches = await _context.Branches.AsNoTracking().ToListAsync();

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
                        if (Enum.IsDefined(typeof(ApprovalStatus), request.Status))
                        {
                            query = query.Where(x => x.ApprovalStatus == request.Status);
                        }
                        if (!string.IsNullOrEmpty(request.SearchTerm))
                        {
                            var searchTerm = request.SearchTerm;
                           
                           
                                query = query.Where(x =>
                                    x.Code.Contains(searchTerm) || x.Date.ToString().Contains(searchTerm));
                            
                        }
                        query = query.OrderByDescending(x => x.Code);
                        var count = await query.CountAsync(cancellationToken: cancellationToken);
                        query = query.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize);
                        
                        var wareHouseTransferOrderList = new List<WareHouseTransferLookUpModel>();
                        foreach (var wareHouseTransferOrder in query)
                        {
                            var lookUpModel = new WareHouseTransferLookUpModel
                            {
                                Id = wareHouseTransferOrder.Id,
                                Code = wareHouseTransferOrder.Code,
                                FromWareHouse = _context.Warehouses.FirstOrDefault(x => x.Id == wareHouseTransferOrder.FromWareHouseId)?.Name,
                                ToWareHouse =  _context.Warehouses.FirstOrDefault(x=>x.Id == wareHouseTransferOrder.ToWareHouseId)?.Name,
                                Date = wareHouseTransferOrder.Date.ToString("MM/dd/yyyy hh:mm tt"),
                                FromBranch = branches.FirstOrDefault(x => x.Id == wareHouseTransferOrder.FromBranchId)?.Code + " " + branches.FirstOrDefault(x => x.Id == wareHouseTransferOrder.FromBranchId)?.BranchName,
                                ToBranch = branches.FirstOrDefault(x => x.Id == wareHouseTransferOrder.ToBranchId)?.Code + " " + branches.FirstOrDefault(x => x.Id == wareHouseTransferOrder.ToBranchId)?.BranchName,

                            };
                            wareHouseTransferOrderList.Add(lookUpModel);
                        }

                        return new PagedResult<List<WareHouseTransferLookUpModel>>
                        {
                            Results = wareHouseTransferOrderList,
                            RowCount = count,
                            PageSize = request.PageSize,
                            CurrentPage = request.PageNumber,
                            PageCount = wareHouseTransferOrderList.Count / request.PageSize
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
