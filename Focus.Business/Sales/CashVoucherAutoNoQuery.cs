using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.Sales
{
    public class CashVoucherAutoNoQuery : IRequest<string>
    {
        public class Handler : IRequestHandler<CashVoucherAutoNoQuery, string>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<CashVoucherAutoNoQuery> logger)
            {
                _context = context;
                _logger = logger;
            }

            public async Task<string> Handle(CashVoucherAutoNoQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var cashVoucher = await _context.CashVouchers.OrderBy(x => x.Id)
                        .LastOrDefaultAsync(cancellationToken);

                    
                    string registration;

                    //Purchase Draft Auto Number
                    if (cashVoucher != null)
                    {
                        if (string.IsNullOrEmpty(cashVoucher.VoucherNo))
                        {
                            registration = "CV-00001";
                            return registration;
                        }
                        string fetchNo = cashVoucher.VoucherNo.Substring(4);
                        Int32 autoNo = Convert.ToInt32((fetchNo));
                        var format = "00000";
                        autoNo++;
                        var prefix = "CV-";
                        var newCode = prefix + autoNo.ToString(format);
                        registration = newCode;
                    }
                    else
                    {
                        registration = "CV-00001";
                    }



                    return registration;
                }
                catch (Exception e)
                {
                    _logger.LogInformation(e.Message);
                    throw new ApplicationException(e.Message);
                }
            }
        }

    }
}
