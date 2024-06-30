using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.SizeCQ.Queries
{
    public class GetSizeCodeQuery : IRequest<string>
    {

        public class Handler : IRequestHandler<GetSizeCodeQuery, string>
        {
            public readonly IApplicationDbContext Context;
            public readonly ILogger Logger;

            public Handler(IApplicationDbContext context, ILogger<GetSizeCodeQuery> logger)
            {
                Context = context;
                Logger = logger;
            }

            public async Task<string> Handle(GetSizeCodeQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var autoCode = await Context.Sizes.OrderBy(x => x.Code).LastOrDefaultAsync(cancellationToken);
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
                return "SZ-00001";
            }

            public string GenerateNewCode(string soNumber)
            {
                string fetchNo = soNumber.Substring(3);
                Int32 autoNo = Convert.ToInt32((fetchNo));
                var format = "00000";
                autoNo++;
                var newCode = "SZ-" + autoNo.ToString(format);
                return newCode;
            }
        }
    }
}
