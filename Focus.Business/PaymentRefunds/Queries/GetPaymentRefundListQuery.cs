using Focus.Business.Common;
using Focus.Business.Interface;
using Focus.Domain.Enum;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.PaymentRefunds.Models;

namespace Focus.Business.PaymentRefunds.Queries
{
    public class GetPaymentRefundListQuery : PagedRequest, IRequest<PagedResult<List<PaymentRefundModel>>>
    {
        public string SearchTerm { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }

        public class Handler : IRequestHandler<GetPaymentRefundListQuery, PagedResult<List<PaymentRefundModel>>>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<GetPaymentRefundListQuery> logger)
            {
                _context = context;
                _logger = logger;
            }

            public async Task<PagedResult<List<PaymentRefundModel>>> Handle(GetPaymentRefundListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var query = await _context.PaymentRefunds
                        .Select(x => new PaymentRefundModel
                        {
                            Id = x.Id,
                            Date = x.Date,
                            VoucherNumber = x.VoucherNumber,
                            Amount = x.Amount,
                            ApprovalStatus = x.ApprovalStatus,
                            PaymentMode = x.PaymentMode,
                            PaymentMethod = x.PaymentMethod,
                        }).OrderByDescending(x => x.VoucherNumber)
                        .ToListAsync(cancellationToken: cancellationToken);
                    
                    query = query.Where(x => x.ApprovalStatus == request.ApprovalStatus).ToList();

                    if (!string.IsNullOrEmpty(request.SearchTerm))
                    {
                        var searchTerm = request.SearchTerm;
                        query = query.Where(x => x.VoucherNumber.Contains(searchTerm) ).ToList();

                    }
                    var count = query.Count();
                    query = query.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize).ToList();
                    
                    return new PagedResult<List<PaymentRefundModel>>
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
