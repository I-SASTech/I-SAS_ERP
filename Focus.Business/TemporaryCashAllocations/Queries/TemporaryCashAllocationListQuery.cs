using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Focus.Business.Common;
using Focus.Business.Interface;
using Focus.Business.TemporaryCashAllocations.Commands;
using Focus.Business.Users;
using Focus.Domain.Enum;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.TemporaryCashAllocations.Queries
{
    public class TemporaryCashAllocationListQuery : PagedRequest, IRequest<PagedResult<List<TemporaryCashAllocationLookUp>>>
    {
        public ApprovalStatus ApprovalStatus { get; set; }
        public string SearchTerm { get; set; }

        public class Handler : IRequestHandler<TemporaryCashAllocationListQuery, PagedResult<List<TemporaryCashAllocationLookUp>>>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private readonly IMapper _mapper;
            private readonly UserManager<ApplicationUser> _userManager;
            public Handler(IApplicationDbContext context, ILogger<TemporaryCashAllocationListQuery> logger, IMapper mapper, UserManager<ApplicationUser> userManager)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
                _userManager = userManager;
            }

            public async Task<PagedResult<List<TemporaryCashAllocationLookUp>>> Handle(TemporaryCashAllocationListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var query = _context.TemporaryCashAllocations.AsNoTracking()
                        .Include(x => x.Account)
                        .Where(x => x.ApprovalStatus == request.ApprovalStatus).AsQueryable();

                    if (!string.IsNullOrEmpty(request.SearchTerm))
                    {
                        var searchTerm = request.SearchTerm;
                        query = query.Where(x =>
                            x.VoucherNumber.Contains(searchTerm) || x.Account.Name.Contains(searchTerm) || x.Account.NameArabic.Contains(searchTerm) || x.Date.ToString().Contains(searchTerm));

                    }
                    query = query.OrderByDescending(x => x.VoucherNumber);
                    var count = await query.CountAsync(cancellationToken: cancellationToken);
                    query = query.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize);

                    var employee =  _context.EmployeeRegistrations.AsNoTracking().AsQueryable();


                    var pvouchers = new List<TemporaryCashAllocationLookUp>();
                    foreach (var item in query)
                    {
                        pvouchers.Add(new TemporaryCashAllocationLookUp
                        {
                            Id = item.Id,
                            Date = item.Date,
                            VoucherNumber = item.VoucherNumber,
                            Narration = item.Narration,
                            ChequeNumber = item.ChequeNumber,
                            ApprovalStatus = item.ApprovalStatus,
                            Amount = item.Amount,
                            PaymentVoucherType = item.PaymentVoucherType,
                            BankCashAccountId = item.BankCashAccountId,
                            BankCashAccountName = item.Account.Name,
                            UserName = employee.FirstOrDefault(x => x.Id == item.UserEmployeeId)?.EnglishName,
                            DraftBy = item.DraftBy,
                            ApprovedBy = item.ApprovedBy,
                            RejectBy = item.RejectBy,
                            VoidBy = item.UserName,
                            Reason = item.Reason,
                            DraftDate = item.DraftDate,
                            ApprovedDate = item.ApprovedDate,
                            RejectDate = item.RejectDate,
                            VoidDate = item.VoidDate,
                            PaymentMode = item.PaymentMode
                        });
                    }

                    return new PagedResult<List<TemporaryCashAllocationLookUp>>
                    {
                        Results = pvouchers,
                        RowCount = count,
                        PageSize = request.PageSize,
                        CurrentPage = request.PageNumber,
                        PageCount = pvouchers.Count / request.PageSize
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
