using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.BranchVouchers.Models;
using Focus.Business.Common;
using Focus.Business.Interface;
using Focus.Domain.Enum;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.BranchVouchers.Queries
{
    public class GetBranchVoucherListQuery : PagedRequest, IRequest<PagedResult<List<BranchVoucherModel>>>
    {
        public string SearchTerm { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }
        public Guid? BranchId { get; set; }

        public class Handler : IRequestHandler<GetBranchVoucherListQuery, PagedResult<List<BranchVoucherModel>>>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<GetBranchVoucherListQuery> logger)
            {
                _context = context;
                _logger = logger;
            }

            public async Task<PagedResult<List<BranchVoucherModel>>> Handle(GetBranchVoucherListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var query = await _context.BranchVouchers
                        .Select(x => new BranchVoucherModel
                        {
                            Id = x.Id,
                            Date = x.Date,
                            VoucherNumber = x.VoucherNumber,
                            Amount = x.Amount,
                            ApprovalStatus = x.ApprovalStatus,
                            PaymentMode = x.PaymentMode,
                            PaymentMethod = x.PaymentMethod,
                            BranchId = x.BranchId,
                            BankName = x.Account.Name,
                            ContactName = x.ContactAccount.Name,
                        }).OrderByDescending(x => x.VoucherNumber)
                        .ToListAsync(cancellationToken: cancellationToken);

                    if (request.BranchId!=null)
                    {
                        query = query.Where(x => x.BranchId == request.BranchId).ToList();

                    }

                    query = query.Where(x => x.ApprovalStatus == request.ApprovalStatus).ToList();

                    if (!string.IsNullOrEmpty(request.SearchTerm))
                    {
                        var searchTerm = request.SearchTerm;
                        query = query.Where(x => x.VoucherNumber.Contains(searchTerm) ).ToList();

                    }
                    var count = query.Count();
                    query = query.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize).ToList();
                    
                    return new PagedResult<List<BranchVoucherModel>>
                    {
                        Results = query,
                        RowCount = count,
                        PageSize = request.PageSize,
                        CurrentPage = request.PageNumber,
                        PageCount = count / request.PageSize

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
