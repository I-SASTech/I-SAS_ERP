using AutoMapper;
using Focus.Business.Common;
using Focus.Business.Interface;
using Focus.Business.PriceRecords.Queries.GetPriceRecordList;
using Focus.Business.PriceRecords;
using Focus.Business.StockRequests.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Focus.Business.Branches.Models;
using Focus.Business.LeaveManagement.LeavePeriod.Model;
using Focus.Domain.Entities;
using Focus.Domain.Enum;

namespace Focus.Business.StockRequests.Queries
{
    public class GetStockRequestListQuery : PagedRequest, IRequest<PagedResult<List<StockRequestLookupModel>>>
    {
        public string SearchTerm { get; set; }
        public bool IsDropDown { get; set; }
        public bool IsStockTransfer { get; set; }
        public bool IsStockReceived { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public Guid? BranchId { get; set; }
        public ApprovalStatus Status { get; set; }
        public class Handler : IRequestHandler<GetStockRequestListQuery, PagedResult<List<StockRequestLookupModel>>>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            private readonly IMapper _mapper;
            public Handler(IApplicationDbContext context, ILogger<GetStockRequestListQuery> logger, IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }

            public async Task<PagedResult<List<StockRequestLookupModel>>> Handle(GetStockRequestListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var query = await _context.StockRequests.AsNoTracking().Include(x => x.StockTransfer).ToListAsync();
                    var branches = await _context.Branches.AsNoTracking().ToListAsync();

                    if (request.IsDropDown)
                    {
                        var stockRequests = new List<StockRequestLookupModel>();

                        if(request.IsStockReceived)
                        {
                            query = query.Where(x => x.ApprovalStatus == ApprovalStatus.Approved && x.StockRequestStatus == StockRequestStatus.Partially || x.StockRequestStatus == StockRequestStatus.Default || x.StockRequestStatus == StockRequestStatus.Fully).ToList();
                        }
                        else if(request.IsStockTransfer)
                        {
                            query = query.Where(x => x.ApprovalStatus == ApprovalStatus.Approved && x.StockRequestStatus == StockRequestStatus.Partially || x.StockRequestStatus == StockRequestStatus.Default).ToList();
                        }
                        else
                        {
                            query = query.Where(x => x.ApprovalStatus == ApprovalStatus.Approved && x.StockTransfer.Any(y => y.StockTransferStatus == StockTransferStatus.Transit)).ToList();
                        }
                        

                        foreach (var item in query)
                        {
                            stockRequests.Add(new StockRequestLookupModel
                            {
                                Code = item.Code + " " + branches.FirstOrDefault(x => x.Id == item.BranchId)?.BranchName + " " + item.Date.ToString("dd MMMM,yy"),
                                Id = item.Id,
                            });
                        }
                        return new PagedResult<List<StockRequestLookupModel>>
                        {
                            Results = stockRequests,
                        };
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

                    if(request.FromDate != null)
                    {
                        query = query.Where(x => x.Date.Date >= request.FromDate.Value.Date && x.Date.Date <= request.ToDate.Value.Date).ToList();
                    }

                    if(request.BranchId != null)
                    {
                        query = query.Where(x => x.BranchId == request.BranchId).ToList();
                    }

                    var count = query.Count();
                    query = query.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize).ToList();

                    var list = new List<StockRequestLookupModel>();
                    foreach (var item in query)
                    {
                        list.Add(new StockRequestLookupModel
                        {
                            Id = item.Id,
                            Code = item.Code,
                            Date = item.Date,
                            ApprovalStatus = item.ApprovalStatus,
                            BranchId = item.BranchId,
                            ToBranchId = item.ToBranchId,
                            FromBranchId = item.FromBranchId,
                            StockRequestStatus = item.StockRequestStatus,
                            ToBranchName = branches.FirstOrDefault(x => x.Id == item.ToBranchId)?.Code + " " + branches.FirstOrDefault(x => x.Id == item.ToBranchId)?.BranchName,
                            FromBranchName = branches.FirstOrDefault(x => x.Id == item.FromBranchId)?.Code + " " + branches.FirstOrDefault(x => x.Id == item.FromBranchId)?.BranchName,
                        });
                    }

                    return new PagedResult<List<StockRequestLookupModel>>
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
