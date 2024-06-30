using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.BomSetup.Queries
{
    public class GetBomAutoCodeQuery : IRequest<string>
    {
        public class Handler : IRequestHandler<GetBomAutoCodeQuery, string>
        {
            public readonly IApplicationDbContext Context;
            public readonly ILogger Logger;

            public Handler(IApplicationDbContext context, ILogger<GetBomAutoCodeQuery> logger)
            {
                Context = context;
                Logger = logger;
            }

            public async Task<string> Handle(GetBomAutoCodeQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var autoCode = await Context.Boms.OrderBy(x => x.Code).LastOrDefaultAsync(cancellationToken);
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
                return "BM-00001";
            }

            public string GenerateNewCode(string soNumber)
            {
                string fetchNo = soNumber.Substring(3);
                Int32 autoNo = Convert.ToInt32((fetchNo));
                var format = "BM-00000";
                autoNo++;
                var newCode = autoNo.ToString(format);
                return newCode;
            }
        }
    }
}
