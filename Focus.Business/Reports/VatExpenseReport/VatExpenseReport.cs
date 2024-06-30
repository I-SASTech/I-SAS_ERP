using Focus.Business.DailyExpenses.Queries.GetDailyExpenseList;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.Reports.VatExpenseReport
{
    public class VatExpenseReport : IRequest<List<DailyExpenseLookUpModel>>
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string BranchId { get; set; }

        public class Handler : IRequestHandler<VatExpenseReport, List<DailyExpenseLookUpModel>>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<DailyExpenseLookUpModel> logger)
            {
                _context = context;
                _logger = logger;
            }
            public async Task<List<DailyExpenseLookUpModel>> Handle(VatExpenseReport request, CancellationToken cancellationToken)
            {
                try
                {
                    if (request.FromDate == null)
                        throw new NotFoundException("Please Select From Date ", null);

                    if (request.ToDate == null)
                        throw new NotFoundException("Please Select To Date ", null);

                    var toDate = DateTime.Parse(request.ToDate.Value.ToString("yyyy/MM/dd"));

                    var dailyExpenses = await _context.DailyExpenses
                        .Where(x => x.Date.Date >= request.FromDate && x.Date.Date <= toDate && x.ApprovalStatus == Domain.Enum.ApprovalStatus.Approved)
                        .ToListAsync(cancellationToken: cancellationToken);

                    if (!string.IsNullOrEmpty(request.BranchId))
                    {
                        var branchIdList = new List<string>(request.BranchId.Split(','));
                        dailyExpenses = dailyExpenses.Where(x => branchIdList.Contains(x.BranchId.ToString())).ToList();
                    }

                    var q1Query = dailyExpenses
                            .Select(dailyExpense => new DailyExpenseLookUpModel
                            {
                                Id = dailyExpense.Id,
                                Date = dailyExpense.Date.ToString("MM/dd/yyyy hh:mm tt"),
                                VoucherNo = dailyExpense.VoucherNo,
                                Description = dailyExpense.Description,
                                Reason = dailyExpense.Reason,
                                PaymentMode = dailyExpense.PaymentMode.ToString(),
                                ReferenceNo = dailyExpense.ReferenceNo,
                                TotalAmount = dailyExpense.DailyExpenseDetails.Sum(x => x.TaxMethod == "Exclusive" || x.TaxMethod == "غير شامل" ? ((x.Amount * x.TaxRate.Rate) / (100)) + x.Amount : x.Amount),
                                GrossAmount = dailyExpense.DailyExpenseDetails.Sum(x => x.TaxMethod == "Inclusive" || x.TaxMethod == "شامل" ? x.Amount - ((x.Amount * x.TaxRate.Rate) / (100 + (x.TaxRate.Rate))) : x.Amount),
                                TotalVat = dailyExpense.DailyExpenseDetails.Sum(x => x.TaxMethod == "Exclusive" || x.TaxMethod == "غير شامل" ? ((x.Amount * x.TaxRate.Rate) / (100)) : ((x.Amount * x.TaxRate.Rate) / (100 + x.TaxRate.Rate))),
                            }).ToList();
                    return q1Query;

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