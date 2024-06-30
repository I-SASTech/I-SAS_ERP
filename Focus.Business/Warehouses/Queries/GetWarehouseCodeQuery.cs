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

namespace Focus.Business.Contacts.Queries
{
   public class GetWarehouseCodeQuery : IRequest<string>
    {

        public class Handler : IRequestHandler<GetWarehouseCodeQuery, string>
        {
            public readonly IApplicationDbContext Context;
            public readonly ILogger Logger;

            public Handler(IApplicationDbContext context, ILogger<GetWarehouseCodeQuery> logger)
            {
                Context = context;
                Logger = logger;
            }

            public async Task<string> Handle(GetWarehouseCodeQuery request, CancellationToken cancellationToken)
            {
                var autoCode = await Context.Warehouses.OrderBy(x => x.StoreID).LastOrDefaultAsync(cancellationToken);
                if (autoCode != null)
                {
                    if (string.IsNullOrEmpty(autoCode.StoreID))
                    {
                        return GenerateCodeFirstTime();
                    }
                    return GenerateNewCode(autoCode.StoreID);
                }

                return GenerateCodeFirstTime();
            }

            public string GenerateCodeFirstTime()
            {
                return "S001";
            }

            public string GenerateNewCode(string soNumber)
            {
                string fetchNo = soNumber.Substring(1);
                Int32 autoNo = Convert.ToInt32((fetchNo));
                var format = "000";
                autoNo++;
                var newCode = "S" + autoNo.ToString(format);
                return newCode;

            }
        }
    }
}