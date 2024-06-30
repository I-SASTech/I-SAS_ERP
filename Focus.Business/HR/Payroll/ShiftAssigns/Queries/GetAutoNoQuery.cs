using Focus.Business.Interface;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Focus.Business.HR.Payroll.ShiftAssigns.Queries
{
    public class GetAutoNoQuery : IRequest<string>
    {
        public class Handler : IRequestHandler<GetAutoNoQuery, string>
        {
            public readonly IApplicationDbContext Context;
            public readonly ILogger Logger;

            public Handler(IApplicationDbContext context, ILogger<GetAutoNoQuery> logger)
            {
                Context = context;
                Logger = logger;
            }

            public async Task<string> Handle(GetAutoNoQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var purchaseOrder = await Context.ShiftAssigns.AsNoTracking()
                        .OrderBy(x => x.Code)
                        .LastOrDefaultAsync(cancellationToken);

                    if (purchaseOrder != null)
                    {
                        if (string.IsNullOrEmpty(purchaseOrder.Code))
                        {
                            return GenerateCodeFirstTime();
                        }

                        return GenerateNewCode(purchaseOrder.Code);
                    }

                    return GenerateCodeFirstTime();
                }
                catch (Exception e)
                {
                    Logger.LogInformation(e.Message);
                    throw new ApplicationException(e.Message);
                }
            }

            public string GenerateCodeFirstTime()
            {
                return "SA-00001";
            }

            public string GenerateNewCode(string soNumber)
            {
                string fetchNo = soNumber.Substring(3);
                Int32 autoNo = Convert.ToInt32((fetchNo));
                var format = "00000";
                autoNo++;
                var newCode = "SA-" + autoNo.ToString(format);
                return newCode;
            }
        }

    }
}
