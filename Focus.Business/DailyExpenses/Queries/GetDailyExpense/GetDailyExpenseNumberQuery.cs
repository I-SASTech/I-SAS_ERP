using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Interface;
using Focus.Domain.Enum;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.DailyExpenses.Queries.GetDailyExpense
{
    public class GetDailyExpenseNumberQuery : IRequest<string>
    {
        public Guid? BranchId { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }

        public class Handler : IRequestHandler<GetDailyExpenseNumberQuery, string>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<GetDailyExpenseNumberQuery> logger)
            {
                _context = context;
                _logger = logger;
            }

            public async Task<string> Handle(GetDailyExpenseNumberQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    string autoCode;
                    string branchCode = "";
                    if (request.BranchId != null)
                    {
                        autoCode = await _context.DailyExpenses
                            .Where(x => x.ApprovalStatus == request.ApprovalStatus && x.BranchId == request.BranchId)
                            .Select(x => x.VoucherNo).OrderBy(x => x)
                            .LastOrDefaultAsync(cancellationToken: cancellationToken);

                        var branch = await _context.Branches
                            .Where(x => x.Id == request.BranchId)
                            .Select(x => x.Code)
                            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

                        string firstLetter = branch.Substring(0, 1);
                        var fetchNo = Convert.ToInt32(branch.Substring(2));
                        branchCode = firstLetter + fetchNo + "-";
                    }
                    else
                    {
                        autoCode = await _context.DailyExpenses
                            .Where(x => x.ApprovalStatus == request.ApprovalStatus && x.BranchId == null)
                            .Select(x => x.VoucherNo).OrderBy(x => x)
                            .LastOrDefaultAsync(cancellationToken: cancellationToken);
                    }

                    if (autoCode != null)
                    {
                        if (string.IsNullOrEmpty(autoCode))
                        {
                            return GenerateCodeFirstTime(branchCode, request.ApprovalStatus);
                        }
                        return GenerateNewCode(autoCode, branchCode, request.ApprovalStatus);
                    }

                    return GenerateCodeFirstTime(branchCode, request.ApprovalStatus);
                }
                catch (Exception e)
                {
                    _logger.LogError(e.Message);
                    throw;
                }
            }

            private string GenerateCodeFirstTime(string branchCode, ApprovalStatus approvalStatus)
            {
                if (approvalStatus == ApprovalStatus.Draft)
                {
                    return branchCode + "ED-00001";
                }
                else
                {
                    return branchCode + "DE-00001";
                }
            }

            private string GenerateNewCode(string soNumber, string branchCode, ApprovalStatus approvalStatus)
            {
                string fetchNo = string.IsNullOrEmpty(branchCode) ? soNumber.Substring(3) : soNumber.Substring(3 + branchCode.Length);
                Int32 autoNo = Convert.ToInt32((fetchNo));
                var format = "00000";
                autoNo++;
                var newCode = approvalStatus == ApprovalStatus.Draft ? "ED-" + autoNo.ToString(format) : "DE-" + autoNo.ToString(format);
                return branchCode + newCode;
            }
        }

    }
}