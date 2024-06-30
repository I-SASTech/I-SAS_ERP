using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.ReparingOrders
{
    public class GetReparingOrderCode : IRequest<string>
    {

        public class Handler : IRequestHandler<GetReparingOrderCode, string>
        {
            public readonly IApplicationDbContext Context;
            public readonly ILogger Logger;

            public Handler(IApplicationDbContext context, ILogger<GetReparingOrderCode> logger)
            {
                Context = context;
                Logger = logger;
            }

            public async Task<string> Handle(GetReparingOrderCode request, CancellationToken cancellationToken)
            {
                try
                {
                    var purchaseOrder = await Context.ReparingOrders
                        .OrderByDescending(x => Convert.ToInt32(x.RegistrationNo))
                        .FirstOrDefaultAsync(cancellationToken);

                    if (purchaseOrder != null)
                    {
                        if (string.IsNullOrEmpty(purchaseOrder.RegistrationNo))
                        {
                            return GenerateCodeFirstTime();
                        }

                        return GenerateNewCode(purchaseOrder.RegistrationNo);
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
                return "1";
            }

            public string GenerateNewCode(string soNumber)
            {
                Int32 autoNo = Convert.ToInt32((soNumber));
                autoNo++;

                return autoNo.ToString();
            }
        }
    }
}

