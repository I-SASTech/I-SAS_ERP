using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Common;
using Focus.Business.Interface;
using Focus.Business.TemporaryCashRequests.Queries;
using Focus.Domain.Enum;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.TemporaryCashIssues.Queries
{
    public class TemporaryCashIssueListQuery : PagedRequest, IRequest<PagedResult<List<TemporaryCashRequestListLookUp>>>
    {
        public string SearchTerm { get; set; }
        public ApprovalStatus Status { get; set; }
        public bool IsDropDown { get; set; }
        public bool IsQuotation { get; set; }
        public bool IsService { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

        public class Handler : IRequestHandler<TemporaryCashIssueListQuery, PagedResult<List<TemporaryCashRequestListLookUp>>>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<TemporaryCashIssueListQuery> logger)
            {
                _context = context;
                _logger = logger;
            }

            public async Task<PagedResult<List<TemporaryCashRequestListLookUp>>> Handle(TemporaryCashIssueListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    if (request.IsDropDown)
                    {
                        var query = _context.TemporaryCashIssues
                            .AsNoTracking()
                            .Include(x => x.TemporaryCashIssueItems)
                            .AsQueryable();

                        var listEmployee = _context.EmployeeRegistrations.AsNoTracking().AsQueryable();
                        var listCashRequestUsers = _context.CashRequestUsers.AsNoTracking().AsQueryable();

                        var temporaryCash = new List<TemporaryCashRequestListLookUp>();
                        foreach (var item in query)
                        {
                            var userName = item.IsCashRequesterUser ?
                                listCashRequestUsers.FirstOrDefault(x => x.Id == item.UserId)?.Name :
                                listEmployee.FirstOrDefault(x => x.Id == item.UserId)?.EnglishName;
                            temporaryCash.Add(new TemporaryCashRequestListLookUp()
                            {
                                Id = item.Id,
                                Date = item.Date.ToString("d"),
                                RegistrationNo = item.RegistrationNo,
                                UserName = userName,
                                Amount = item.TemporaryCashIssueItems.Sum(x => x.Amount)
                            });
                        }
                        return new PagedResult<List<TemporaryCashRequestListLookUp>>
                        {
                            Results = temporaryCash
                        };

                    }
                    else
                    {
                        var query = _context.TemporaryCashIssues
                            .AsNoTracking()
                            .Include(x => x.TemporaryCashIssueExpenses)
                            .Include(x => x.TemporaryCashReturns)
                            .Include(x => x.TemporaryCashIssueItems)
                            .Where(x => x.ApprovalStatus == request.Status)
                            .AsQueryable();

                        var listEmployee = _context.EmployeeRegistrations.AsNoTracking().AsQueryable();
                        var listCashRequestUsers = _context.CashRequestUsers.AsNoTracking().AsQueryable();

                        if (!string.IsNullOrEmpty(request.SearchTerm))
                        {
                            var searchTerm = request.SearchTerm;
                            query = query.Where(x =>
                                x.RegistrationNo.Contains(searchTerm) || x.Date.ToString("d").Contains(searchTerm));
                        }



                        query = query.OrderByDescending(x => x.RegistrationNo);
                        var count = await query.CountAsync(cancellationToken: cancellationToken);
                        query = query.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize);

                        var account = await _context.Accounts.AsNoTracking().FirstOrDefaultAsync(x => x.Name == "Temporary Cash", cancellationToken: cancellationToken);



                        var temporaryCash = new List<TemporaryCashRequestListLookUp>();
                        foreach (var item in query)
                        {
                            var temporaryCashIssueExpenses = new List<TemporaryCashIssueExpensesLookUp>();
                            if (item.TemporaryCashIssueExpenses.Count > 0)
                            {
                                foreach (var expense in item.TemporaryCashIssueExpenses)
                                {
                                    if (expense.DocumentType == "Expenses")
                                    {
                                        var dailyExpense = await _context.DailyExpenses.FindAsync(expense.DocumentId);

                                        temporaryCashIssueExpenses.Add(new TemporaryCashIssueExpensesLookUp()
                                        {
                                            Id = expense.Id,
                                            Date = dailyExpense?.Date.ToString("d"),
                                            DocumentType = expense.DocumentType,
                                            DocumentNo = dailyExpense?.VoucherNo,
                                            Amount = dailyExpense?.DailyExpenseDetails.Sum(x => x.TaxMethod == "Exclusive" || x.TaxMethod == "غير شامل" ? ((x.Amount * x.TaxRate.Rate) / (100)) + x.Amount : x.Amount) ?? 0,
                                        });
                                    }
                                    if (expense.DocumentType == "Supplier Payment")
                                    {
                                        var voucher = await _context.PaymentVouchers
                                                .AsNoTracking()
                                                .FirstOrDefaultAsync(x => x.Id == expense.DocumentId, cancellationToken: cancellationToken);
                                        temporaryCashIssueExpenses.Add(new TemporaryCashIssueExpensesLookUp()
                                        {
                                            Id = expense.Id,
                                            Date = voucher?.Date.ToString("d"),
                                            DocumentType = expense.DocumentType,
                                            DocumentNo = voucher?.VoucherNumber,
                                            Amount = voucher?.Amount ?? 0,
                                        });
                                    }
                                }
                            }

                            var userName = item.IsCashRequesterUser ?
                                listCashRequestUsers.FirstOrDefault(x => x.Id == item.UserId)?.Name :
                                listEmployee.FirstOrDefault(x => x.Id == item.UserId)?.EnglishName;
                            temporaryCash.Add(new TemporaryCashRequestListLookUp()
                            {
                                Id = item.Id,
                                Date = item.Date.ToString("d"),
                                UserName = userName,
                                RegistrationNo = item.RegistrationNo,
                                UserId = item.UserId,
                                IsCashRequesterUser = item.IsCashRequesterUser,
                                TemporaryCashAccountId = account?.Id,
                                TemporaryCashIssueExpenses = temporaryCashIssueExpenses,
                                Amount = item.TemporaryCashIssueItems.Sum(x => x.Amount),
                                ReturnAmount = item.TemporaryCashReturns.Sum(x => x.Amount),
                                VoucherAmount = temporaryCashIssueExpenses.Sum(x => x.Amount),
                            });
                        }

                        return new PagedResult<List<TemporaryCashRequestListLookUp>>
                        {
                            Results = temporaryCash,
                            RowCount = count,
                            PageSize = request.PageSize,
                            CurrentPage = request.PageNumber,
                            PageCount = temporaryCash.Count / request.PageSize
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
