using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.HR.Payroll.Deductions
{
   public class GetDeductionCodeQuery : IRequest<string>
    {

        public class Handler : IRequestHandler<GetDeductionCodeQuery, string>
        {
            public readonly IApplicationDbContext Context;
            public readonly ILogger Logger;

            public Handler(IApplicationDbContext context, ILogger<GetDeductionCodeQuery> logger)
            {
                Context = context;
                Logger = logger;
            }

            public async Task<string> Handle(GetDeductionCodeQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var autoCode = await Context.Deductions.OrderBy(x => x.Code).LastOrDefaultAsync(cancellationToken);
                    if (autoCode != null)
                    {
                        if (string.IsNullOrEmpty(autoCode.Code))
                        {
                            return GenerateCodeFirstTime();
                        }
                        return GenerateNewCode(autoCode.Code);
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
                return "DD-00001";
            }

            public string GenerateNewCode(string soNumber)
            {
                string fetchNo = soNumber.Substring(3);
                Int32 autoNo = Convert.ToInt32((fetchNo));
                var format = "DD-00000";
                autoNo++;
                var newCode = autoNo.ToString(format);
                return newCode;
            }
        }
    }
}
