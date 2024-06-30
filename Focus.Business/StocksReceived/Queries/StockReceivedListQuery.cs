using AutoMapper;
using Focus.Business.Common;
using Focus.Business.Interface;
using Focus.Business.StocksReceived.Models;
using Focus.Domain.Enum;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.StocksReceived.Queries
{
    public class StockReceivedListQuery : PagedRequest, IRequest<PagedResult<List<StockReceivedLookupModel>>>
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public Guid? BranchId { get; set; }
        public string SearchTerm { get; set; }
        public bool IsDropDown { get; set; }
        public ApprovalStatus Status { get; set; }


        public class Handler : IRequestHandler<StockReceivedListQuery, PagedResult<List<StockReceivedLookupModel>>>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            private readonly IMapper _mapper;
            public Handler(IApplicationDbContext context, ILogger<StockReceivedListQuery> logger, IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }

            public async Task<PagedResult<List<StockReceivedLookupModel>>> Handle(StockReceivedListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var query = await _context.StockReceived.AsNoTracking().Include(x => x.StockTransfer).ToListAsync();
                    var branches = await _context.Branches.AsNoTracking().ToListAsync();

                   

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

                    if (request.BranchId != null)
                    {
                        query = query.Where(x => x.BranchId == request.BranchId).ToList();
                    }

                    var count = query.Count();
                    query = query.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize).ToList();

                    var list = new List<StockReceivedLookupModel>();
                    foreach (var item in query)
                    {
                        list.Add(new StockReceivedLookupModel
                        {
                            Id = item.Id,
                            Code = item.Code,
                            Date = item.Date,
                            ApprovalStatus = item.ApprovalStatus,
                            BranchId = item.BranchId,
                            TotalAmount = item.TotalAmount,
                            StockTransferBranch = branches.Count > 0 ?  branches.FirstOrDefault(x => x.Id == item.StockTransfer.BranchId)?.Code + " - " + branches.FirstOrDefault(x => x.Id == item.StockTransfer.BranchId)?.BranchName : "",
                        });
                    }

                    return new PagedResult<List<StockReceivedLookupModel>>
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
