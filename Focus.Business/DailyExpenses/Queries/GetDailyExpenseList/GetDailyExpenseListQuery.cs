using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Focus.Business.Common;
using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Focus.Domain.Interface;
using Focus.Business.Users;
using Focus.Domain.Enum;
using Microsoft.AspNetCore.Identity;

namespace Focus.Business.DailyExpenses.Queries.GetDailyExpenseList
{
    public class GetDailyExpenseListQuery : PagedRequest, IRequest<PagedResult<List<DailyExpenseLookUpModel>>>
    {
        public string SearchTerm { get; set; }
        public bool IsDropDown { get; set; }
        public ApprovalStatus Status { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string PaymentMode { get; set; }
        public string ReferenceNo { get; set; }
        public decimal? TotalAmount { get; set; }
        public Guid? BranchId { get; set; }

        public class Handler : IRequestHandler<GetDailyExpenseListQuery, PagedResult<List<DailyExpenseLookUpModel>>>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private readonly IMapper _mapper;
            private readonly IUserHttpContextProvider _contextProvider;
            private readonly UserManager<ApplicationUser> _userManager;

            public Handler(IApplicationDbContext context, ILogger<GetDailyExpenseListQuery> logger, IMapper mapper,
                IUserHttpContextProvider contextProvider, UserManager<ApplicationUser> userManager)
            {
                _userManager = userManager;
                _context = context;
                _logger = logger;
                _mapper = mapper;
                _contextProvider = contextProvider;
            }

            public async Task<PagedResult<List<DailyExpenseLookUpModel>>> Handle(GetDailyExpenseListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    if (request.IsDropDown)
                    {
                        var dailyExpenses = _context.DailyExpenses.AsQueryable();

                        if (request.BranchId != null)
                        {
                            dailyExpenses = dailyExpenses.Where(x => x.BranchId == request.BranchId);
                        }

                        var dailyExpenseList = await dailyExpenses.OrderBy(x => x.VoucherNo)
                         .ProjectTo<DailyExpenseLookUpModel>(_mapper.ConfigurationProvider)
                         .ToListAsync(cancellationToken);

                        return new PagedResult<List<DailyExpenseLookUpModel>>
                        {
                            Results = dailyExpenseList
                        };

                    }
                    else
                    {
                        var counterId = _contextProvider.GetCounterId();

                        var user = await _context.LoginPermissions
                            .Select(x => new
                            {
                                x.UserId,
                                x.AllowAll,
                            }).FirstOrDefaultAsync(x => x.UserId == _contextProvider.GetUserId(), cancellationToken: cancellationToken);

                        _context.DisableTenantFilter = true;

                        var query =await _context.DailyExpenses
                            .Where(x => x.ApprovalStatus == request.Status)
                            .Select(x => new
                            {
                                x.Id,
                                x.BranchId,
                                x.Date,
                                x.VoucherNo,
                                x.PaymentMode,
                                x.ReferenceNo,
                                x.Description,
                                x.Reason,
                                x.TotalAmount,
                                x.TotalVat,
                                x.ExpenseDate,
                                CounterId = EF.Property<Guid?>(x, "CounterId"),
                                UserId = EF.Property<Guid?>(x, "UserId"),
                                CreatedById = EF.Property<string>(x, "CreatedById")
                            }).ToListAsync(cancellationToken: cancellationToken);

                        if (request.BranchId != null)
                        {
                            query = query.Where(x => x.BranchId == request.BranchId).ToList();
                        }

                        if (request.FromDate != null)
                        {
                            query = query.Where(x => x.Date.Date >= request.FromDate.Value.Date).ToList();
                        }

                        if (request.ToDate != null)
                        {
                            query = query.Where(x => x.Date.Date <= request.ToDate.Value.Date).ToList();
                        }

                        if (!user.AllowAll)
                        {
                            query = query.Where(x => x.CounterId == counterId).ToList();
                        }

                        if (!string.IsNullOrEmpty(request.SearchTerm))
                        {
                            var searchTerm = request.SearchTerm.ToLower();
                            query = query.Where(x => x.VoucherNo.ToLower().Contains(searchTerm)).ToList();
                        }

                        if (!string.IsNullOrEmpty(request.PaymentMode))
                        {
                            var searchTerm1 = request.PaymentMode;
                            query = query.Where(x => x.PaymentMode.ToString() == searchTerm1).ToList();
                        }

                        if (!string.IsNullOrEmpty(request.ReferenceNo))
                        {
                            var searchTerm2 = request.ReferenceNo.ToLower();
                            query = query.Where(x => x.ReferenceNo.ToLower().Contains(searchTerm2)).ToList();
                        }

                        query = query.OrderByDescending(x => x.VoucherNo).ToList();
                        var count = query.Count();
                        query = query.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize).ToList();

                        var branchList = await _context.Branches.AsNoTracking().ToListAsync(cancellationToken: cancellationToken);
                        var terminals = await _context.Terminals.AsNoTracking().ToListAsync(cancellationToken: cancellationToken);
                        var users = await _userManager.Users.AsNoTracking().ToListAsync(cancellationToken: cancellationToken);

                        var q1Query = query.Select(dailyExpense => new DailyExpenseLookUpModel
                        {
                            Id= dailyExpense.Id,
                            Date = dailyExpense.Date.ToString("MM/dd/yyyy"),
                            VoucherNo = dailyExpense.VoucherNo,
                            Description = dailyExpense.Description,
                            Reason = dailyExpense.Reason,
                            PaymentMode = dailyExpense.PaymentMode.ToString(),
                            ReferenceNo = dailyExpense.ReferenceNo,
                            TotalAmount = dailyExpense.TotalAmount,
                            TotalVat = dailyExpense.TotalVat,
                            DateOfExpense= dailyExpense.ExpenseDate != null ? dailyExpense.ExpenseDate?.ToString("dd/MM/yyyy") : null,
                            CounterName = terminals.FirstOrDefault(x => x.Id == dailyExpense.CounterId)?.Code,
                            BranchCode = branchList.FirstOrDefault(x => x.Id == dailyExpense.BranchId)?.Code,
                            UserName = users.FirstOrDefault(x => x.Id == dailyExpense.UserId.ToString())?.UserName,
                            CreatedUser = users.FirstOrDefault(x => x.Id == dailyExpense.CreatedById)?.UserName
                        }).ToList();

                        if (request.TotalAmount != null && request.TotalAmount != 0)
                        {
                            q1Query = q1Query.Where(x => x.TotalAmount.ToString("0.00").Contains(request.TotalAmount.ToString())).ToList();
                        }


                        return new PagedResult<List<DailyExpenseLookUpModel>>
                        {
                            Results = q1Query,
                            RowCount = count,
                            PageSize = request.PageSize,
                            CurrentPage = request.PageNumber,
                            PageCount = q1Query.Count / request.PageSize
                        };
                    }

                }
                catch (Exception exception)
                {
                    _logger.LogError(exception.Message);
                    throw new ApplicationException(exception.Message);
                }
                finally
                {
                    _context.DisableTenantFilter = false;
                }
            }

        }
    }
}
