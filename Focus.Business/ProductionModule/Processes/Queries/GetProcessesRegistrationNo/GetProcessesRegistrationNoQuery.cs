using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.ProductionModule.Processes.Queries.GetProcessesRegistrationNo
{
    public class GetProcessesRegistrationNoQuery : IRequest<string>
    {
        public class Handler : IRequestHandler<GetProcessesRegistrationNoQuery, string>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<GetProcessesRegistrationNoQuery> logger)
            {
                _context = context;
                _logger = logger;
            }

            public async Task<string> Handle(GetProcessesRegistrationNoQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var process = await _context.Processes
                        .OrderBy(x => x.Code)
                        .LastOrDefaultAsync(cancellationToken);

                    if (process != null)
                    {
                        if (string.IsNullOrEmpty(process.Code))
                        {
                            return GenerateCodeFirstTime();
                        }

                        return GenerateNewCode(process.Code);
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
                return "P-00001";
            }

            public string GenerateNewCode(string soNumber)
            {
                string fetchNo = soNumber.Substring(3);
                Int32 autoNo = Convert.ToInt32((fetchNo));
                var format = "00000";
                autoNo++;
                var newCode = "P-" + autoNo.ToString(format);
                return newCode;
            }
        }
    }
}
