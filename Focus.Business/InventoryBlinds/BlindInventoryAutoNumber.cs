using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Categories.Queries;
using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.InventoryBlinds
{
    public class BlindInventoryAutoNumber : IRequest<string>
    {

        public class Handler : IRequestHandler<BlindInventoryAutoNumber, string>
        {
            public readonly IApplicationDbContext Context;
            public readonly ILogger Logger;

            public Handler(IApplicationDbContext context, ILogger<BlindInventoryAutoNumber> logger)
            {
                Context = context;
                Logger = logger;
            }

            public async Task<string> Handle(BlindInventoryAutoNumber request, CancellationToken cancellationToken)
            {
                try
                {
                    var autoCode = await Context.InventoryBlinds.AsNoTracking().OrderBy(x => x.DocumentId).LastOrDefaultAsync(cancellationToken);
                    if (autoCode != null)
                    {
                        if (string.IsNullOrEmpty(autoCode.DocumentId))
                        {
                            return GenerateCodeFirstTime();
                        }
                        return GenerateNewCode(autoCode.DocumentId);
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
                return "IB-00001";
            }

            public string GenerateNewCode(string soNumber)
            {
                string fetchNo = soNumber.Substring(3);
                Int32 autoNo = Convert.ToInt32((fetchNo));
                var format = "IB-00000";
                autoNo++;
                var newCode = autoNo.ToString(format);
                return newCode;
            }
        }
    }
}
