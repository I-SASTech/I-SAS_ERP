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

namespace Focus.Business.Units.Queries
{
    public class GetUnitCodeQuery : IRequest<string>
    {

        public class Handler : IRequestHandler<GetUnitCodeQuery, string>
        {
            public readonly IApplicationDbContext Context;
            public readonly ILogger Logger;

            public Handler(IApplicationDbContext context, ILogger<GetUnitCodeQuery> logger)
            {
                Context = context;
                Logger = logger;
            }

            public async Task<string> Handle(GetUnitCodeQuery request, CancellationToken cancellationToken)
            {
                var autoCode = await Context.Units.OrderBy(x => x.Code).LastOrDefaultAsync(cancellationToken);
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
                return "UT-00001";
            }

            public string GenerateNewCode(string soNumber)
            {
                string fetchNo = soNumber.Substring(3);
                Int32 autoNo = Convert.ToInt32((fetchNo));
                var format = "00000";
                autoNo++;
                var newCode = "UT-" + autoNo.ToString(format);
                return newCode;
            }
        }
    }
}
