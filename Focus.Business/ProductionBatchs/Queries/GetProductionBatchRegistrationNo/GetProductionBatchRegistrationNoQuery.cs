using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.ProductionBatchs.Queries.GetProductionBatchRegistrationNo
{
    public class GetProductionBatchRegistrationNoQuery : IRequest<string>
    {
        public class Handler : IRequestHandler<GetProductionBatchRegistrationNoQuery, string>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<GetProductionBatchRegistrationNoQuery> logger)
            {
                _context = context;
                _logger = logger;
            }

            public async Task<string> Handle(GetProductionBatchRegistrationNoQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var productionBatch = await _context.ProductionBatchs
                        .OrderBy(x => x.RegistrationNo)
                        .LastOrDefaultAsync(cancellationToken);

                    if (productionBatch != null)
                    {
                        if (string.IsNullOrEmpty(productionBatch.RegistrationNo))
                        {
                            return GenerateCodeFirstTime();
                        }

                        return GenerateNewCode(productionBatch.RegistrationNo);
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
                return "PB-00001";
            }

            public string GenerateNewCode(string soNumber)
            {
                string fetchNo = soNumber.Substring(3);
                Int32 autoNo = Convert.ToInt32((fetchNo));
                var format = "00000";
                autoNo++;
                var newCode = "PB-" +autoNo.ToString(format);
                return newCode;
            }
        }
    }
}

