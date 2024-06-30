using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.MultiUps
{
    public class GetMultiUpCode : IRequest<string>
    {

        public class Handler : IRequestHandler<GetMultiUpCode, string>
        {
            public readonly IApplicationDbContext Context;
            public readonly ILogger Logger;

            public Handler(IApplicationDbContext context, ILogger<GetMultiUpCode> logger)
            {
                Context = context;
                Logger = logger;
            }

            public async Task<string> Handle(GetMultiUpCode request, CancellationToken cancellationToken)
            {
                try
                {
                    var purchaseOrder = await Context.MultiUps
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

