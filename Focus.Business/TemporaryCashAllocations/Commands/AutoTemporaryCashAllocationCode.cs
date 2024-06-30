using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Interface;
using Focus.Domain.Enum;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.TemporaryCashAllocations.Commands
{
    public class AutoTemporaryCashAllocationCode : IRequest<string>
    {
        public PaymentVoucherType PaymentVoucherType { get; set; }
        public class Handler : IRequestHandler<AutoTemporaryCashAllocationCode, string>
        {
            public readonly IApplicationDbContext Context;
            public readonly ILogger Logger;

            public Handler(IApplicationDbContext context, ILogger<AutoTemporaryCashAllocationCode> logger)
            {
                Context = context;
                Logger = logger;
            }

            public async Task<string> Handle(AutoTemporaryCashAllocationCode request, CancellationToken cancellationToken)
            {
                try
                {
                    var autoCode = await Context.TemporaryCashAllocations.OrderBy(x => x.VoucherNumber).LastOrDefaultAsync(cancellationToken);
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
                    Logger.LogError(ex.Message);
                    throw new ApplicationException("Unable to process your request please contact support");
                }
            }

            public string GenerateCodeFirstTime()
            {
                return "CA-00001";
            }

            public string GenerateNewCode(string soNumber)
            {
                string fetchNo = soNumber.Substring(3);
                Int32 autoNo = Convert.ToInt32((fetchNo));
                var format = "00000";
                autoNo++;
                var prefix = "CA-";
                var newCode = prefix + autoNo.ToString(format);
                return newCode;
            }
        }
    }
}
