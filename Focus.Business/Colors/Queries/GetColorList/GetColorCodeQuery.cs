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

namespace Focus.Business.Colors.Queries
{
    public class GetColorCodeQuery : IRequest<string>
    {

        public class Handler : IRequestHandler<GetColorCodeQuery, string>
        {
            public readonly IApplicationDbContext Context;
            public readonly ILogger Logger;

            public Handler(IApplicationDbContext context, ILogger<GetColorCodeQuery> logger)
            {
                Context = context;
                Logger = logger;
            }

            public async Task<string> Handle(GetColorCodeQuery request, CancellationToken cancellationToken)
            {
                var autoCode = await Context.Colors.OrderBy(x => x.Code).LastOrDefaultAsync(cancellationToken);
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
                return "CR-00001";
            }

            public string GenerateNewCode(string soNumber)
            {
                string fetchNo = soNumber.Substring(3);
                Int32 autoNo = Convert.ToInt32((fetchNo));
                var format = "CR-00000";
                autoNo++;
                var newCode = autoNo.ToString(format);
                return newCode;
            }
        }
    }
}
