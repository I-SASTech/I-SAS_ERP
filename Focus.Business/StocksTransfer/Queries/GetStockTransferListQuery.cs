using AutoMapper;
using Focus.Business.Common;
using Focus.Business.Interface;
using Focus.Business.StockRequests.Models;
using Focus.Business.StockRequests.Queries;
using Focus.Business.StocksTransfer.Models;
using Focus.Domain.Enum;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.StocksTransfer.Queries
{
    public class GetStockTransferListQuery : PagedRequest, IRequest<PagedResult<List<StockTransferLookupModel>>>
    {
        public string SearchTerm { get; set; }
        public bool IsDropDown { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public Guid? BranchId { get; set; }
        public bool IsMainBranch { get; set; }
        public ApprovalStatus Status { get; set; }

        public class Handler : IRequestHandler<GetStockTransferListQuery, PagedResult<List<StockTransferLookupModel>>>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private readonly IMapper _mapper;
            public Handler(IApplicationDbContext context, ILogger<GetStockTransferListQuery> logger, IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }

            public async Task<PagedResult<List<StockTransferLookupModel>>> Handle(GetStockTransferListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var query = await _context.StockTransfers.AsNoTracking().Include(x => x.StockRequest).ToListAsync();
                    var branches = await _context.Branches.AsNoTracking().ToListAsync();
                    if (request.IsDropDown)
                    {
                        var stockRequests = new List<StockTransferLookupModel>();

                        query = query.Where(x => x.StockTransferStatus == StockTransferStatus.Transit).ToList();

                        foreach (var item in query)
                        {
                            stockRequests.Add(new StockTransferLookupModel
                            {
                                Code = item.Code,
                                Id = item.Id,
                            });
                        }
                        return new PagedResult<List<StockTransferLookupModel>>
                        {
                            Results = stockRequests,
                        };
                    }

                    if(!request.IsMainBranch)
                    {
                        query = query.Where(x => x.StockTransferStatus == StockTransferStatus.Transit && x.StockRequesBranchtId == request.BranchId).ToList();
                    }

                    if (!string.IsNullOrEmpty(request.SearchTerm))
                    {
                        var searchTerm = request.SearchTerm;
                        query = query.Where(x => x.Code.Contains(searchTerm)).ToList();
                    }

                    if (request.Status == ApprovalStatus.Draft)
                    {
                        query = query.Where(x => x.ApprovalStatus == ApprovalStatus.Draft).ToList();
                    }
                    if (request.Status == ApprovalStatus.Approved)
                    {
                        query = query.Where(x => x.ApprovalStatus == ApprovalStatus.Approved).ToList();
                    }

                    if (request.FromDate != null)
                    {
                        query = query.Where(x => x.Date.Date >= request.FromDate.Value.Date && x.Date.Date <= request.ToDate.Value.Date).ToList();
                    }

                    var count = query.Count();
                    query = query.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize).ToList();

                    var list = new List<StockTransferLookupModel>();
                    foreach (var item in query)
                    {
                        list.Add(new StockTransferLookupModel
                        {
                            Id = item.Id == null?Guid.Empty:item.Id,
                            Code = item.Code,
                            Date = item.Date,
                            ApprovalStatus = item.ApprovalStatus,
                            BranchId = item.BranchId,
                            StockRequesBranchtId = item.StockRequesBranchtId,
                            VoucherId = item.VoucherId,
                            StockStatus = item.StockTransferStatus.ToString(),
                            MainBranch = branches.FirstOrDefault(x => x.Id == item.BranchId)?.MainBranch??false,
                            TotalAmount = item.TotalAmount,
                            StockRequestedBranch = branches.FirstOrDefault(x => x.Id == item.StockRequest.BranchId)?.Code + " - " + branches.FirstOrDefault(x => x.Id == item.StockRequest.BranchId)?.BranchName,
                            StockTransferBranch = branches.FirstOrDefault(x => x.Id == item.BranchId)?.Code + " - " + branches.FirstOrDefault(x => x.Id == item.BranchId)?.BranchName,
                            
                        });
                    }

                    return new PagedResult<List<StockTransferLookupModel>>
                    {
                        Results = list,
                        RowCount = count,
                        PageSize = request.PageSize,
                        CurrentPage = request.PageNumber,
                        PageCount = list.Count / request.PageSize
                    };
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
