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

namespace Focus.Business.ProductionBatchs.Queries.GetProductionBatchList
{
   public class GetProductionTransferCode : IRequest<string>
    {
        public class Handler : IRequestHandler<GetProductionTransferCode, string>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<GetProductionTransferCode> logger)
            {
                _context = context;
                _logger = logger;
            }

            public async Task<string> Handle(GetProductionTransferCode request, CancellationToken cancellationToken)
            {
                try
                {
                    var productionBatch = await _context.ProductionStockTransfers
                        .OrderBy(x => x.Code)
                        .LastOrDefaultAsync(cancellationToken);

                    if (productionBatch != null)
                    {
                        if (string.IsNullOrEmpty(productionBatch.Code))
                        {
                            return GenerateCodeFirstTime();
                        }

                        return GenerateNewCode(productionBatch.Code);
                    }

                    return GenerateCodeFirstTime();
                }
                catch (Exception e)
                {
                    _logger.LogInformation(e.Message);
                    throw new ApplicationException(e.Message);
                }
            }

            public string GenerateCodeFirstTime()
            {
                return "PST-00001";
            }

            public string GenerateNewCode(string soNumber)
            {
                string fetchNo = soNumber.Substring(3);
                Int32 autoNo = Convert.ToInt32((fetchNo));
                var format = "00000";
                autoNo++;
                var newCode = "PST-" + autoNo.ToString(format);
                return newCode;
            }
        }
    }
}

