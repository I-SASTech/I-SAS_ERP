using Focus.Business.Interface;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Focus.Business.ProductGroups.Queries
{
    public class GetProductGroupCode : IRequest<string>
    {
        public class Handler : IRequestHandler<GetProductGroupCode, string>
        {
            public readonly IApplicationDbContext Context;
            public readonly ILogger Logger;

            public Handler(IApplicationDbContext context, ILogger<GetProductGroupCode> logger)
            {
                Context = context;
                Logger = logger;
            }

            public async Task<string> Handle(GetProductGroupCode request, CancellationToken cancellationToken)
            {
                var autoCode = await Context.ProductGroups.OrderBy(x => x.Code).LastOrDefaultAsync(cancellationToken);
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

            public string GenerateCodeFirstTime()
            {
                return "PG-00001";
            }

            public string GenerateNewCode(string soNumber)
            {
                string fetchNo = soNumber.Substring(3);
                Int32 autoNo = Convert.ToInt32((fetchNo));
                var format = "PG-00000";
                autoNo++;
                var newCode = autoNo.ToString(format);
                return newCode;
            }
        }

    }
}
