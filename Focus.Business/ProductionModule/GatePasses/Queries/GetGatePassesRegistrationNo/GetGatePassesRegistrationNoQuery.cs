using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.ProductionModule.GatePasses.Queries.GetGatePassesRegistrationNo
{
    public class GetGatePassesRegistrationNoQuery : IRequest<string>
    {

        public class Handler : IRequestHandler<GetGatePassesRegistrationNoQuery, string>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<GetGatePassesRegistrationNoQuery> logger)
            {
                _context = context;
                _logger = logger;
            }

            public async Task<string> Handle(GetGatePassesRegistrationNoQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var gatepasses = await _context.GatePasses
                        .OrderBy(x => x.RegistrationNo)
                        .LastOrDefaultAsync(cancellationToken);

                    if (gatepasses != null)
                    {
                        if (string.IsNullOrEmpty(gatepasses.RegistrationNo))
                        {
                            return GenerateCodeFirstTime();
                        }

                        return GenerateNewCode(gatepasses.RegistrationNo);
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
                return "GP-00001";
            }

            public string GenerateNewCode(string soNumber)
            {
                string fetchNo = soNumber.Substring(3);
                Int32 autoNo = Convert.ToInt32((fetchNo));
                var format = "00000";
                autoNo++;
                var newCode = "GP-" + autoNo.ToString(format);
                return newCode;
            }
        }
    }
}
