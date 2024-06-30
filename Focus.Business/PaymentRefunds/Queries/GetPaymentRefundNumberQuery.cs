using Focus.Business.Interface;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Focus.Business.PaymentRefunds.Queries
{
    public class GetPaymentRefundNumberQuery : IRequest<string>
    {
        public class Handler : IRequestHandler<GetPaymentRefundNumberQuery, string>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<GetPaymentRefundNumberQuery> logger)
            {
                _context = context;
                _logger = logger;
            }

            public async Task<string> Handle(GetPaymentRefundNumberQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var autoCode = await _context.PaymentRefunds.OrderByDescending(x => x.VoucherNumber).FirstOrDefaultAsync(cancellationToken);
                    if (autoCode != null)
                    {
                        if (string.IsNullOrEmpty(autoCode.VoucherNumber))
                        {
                            return GenerateCodeFirstTime();
                        }
                        return GenerateNewCode(autoCode.VoucherNumber);
                    }

                    return GenerateCodeFirstTime();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    throw new ApplicationException("Unable to process your request please contact support");
                }
            }

            private string GenerateCodeFirstTime()
            {
                return "RP-00001";
            }

            private string GenerateNewCode(string soNumber)
            {
                var fetchNo = soNumber.Substring(5);
                var autoNo = Convert.ToInt32(fetchNo);
                var format = "00000";
                autoNo++;
                var prefix = "RP-";
                var newCode = prefix + autoNo.ToString(format);
                return newCode;
            }
        }

    }
}
