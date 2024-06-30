using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.BranchUsers.Models;
using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.BranchVouchers.Queries
{
    public class GetBranchVoucherNumberQuery : IRequest<string>
    {
        public Guid? BranchId { get; set; }

        public class Handler : IRequestHandler<GetBranchVoucherNumberQuery, string>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<GetBranchVoucherNumberQuery> logger)
            {
                _context = context;
                _logger = logger;
            }

            public async Task<string> Handle(GetBranchVoucherNumberQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var autoCode = await _context.BranchVouchers.OrderByDescending(x => x.VoucherNumber).FirstOrDefaultAsync(x => x.BranchId == request.BranchId, cancellationToken: cancellationToken);

                    var branch = await _context.Branches
                        .Where(x => x.Id == request.BranchId)
                        .Select(x => x.Code)
                        .FirstOrDefaultAsync(cancellationToken: cancellationToken);

                    string firstLetter = branch.Substring(0, 1);
                    var fetchNo = Convert.ToInt32(branch.Substring(2));
                    var branchCode = firstLetter + fetchNo + "-";

                    if (autoCode != null)
                    {
                        if (string.IsNullOrEmpty(autoCode.VoucherNumber))
                        {
                            return GenerateCodeFirstTime(branchCode);
                        }
                        return GenerateNewCode(autoCode.VoucherNumber, branchCode);
                    }

                    return GenerateCodeFirstTime(branchCode);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    throw new ApplicationException("Unable to process your request please contact support");
                }
            }

            private string GenerateCodeFirstTime(string branchCode)
            {
                return branchCode + "BV-00001";
            }

            private string GenerateNewCode(string soNumber, string branchCode)
            {
                var fetchNo = soNumber.Substring(soNumber.Length - 5);
                var autoNo = Convert.ToInt32(fetchNo);
                var format = "00000";
                autoNo++;
                var prefix = "BV-";
                var newCode = prefix + autoNo.ToString(format);
                return branchCode + newCode;
            }
        }

    }
}
