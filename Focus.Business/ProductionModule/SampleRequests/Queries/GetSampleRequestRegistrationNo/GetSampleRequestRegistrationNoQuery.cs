using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.ProductionModule.SampleRequests.Queries.GetSampleRequestRegistrationNo
{
    public class GetSampleRequestRegistrationNoQuery : IRequest<string>
    {
        public class Handler : IRequestHandler<GetSampleRequestRegistrationNoQuery, string>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<GetSampleRequestRegistrationNoQuery> logger)
            {
                _context = context;
                _logger = logger;
            }

            public async Task<string> Handle(GetSampleRequestRegistrationNoQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var batchProcess = await _context.SampleRequests
                        .OrderBy(x => x.Code)
                        .LastOrDefaultAsync(cancellationToken);

                    if (batchProcess != null)
                    {
                        if (string.IsNullOrEmpty(batchProcess.Code))
                        {
                            return GenerateCodeFirstTime();
                        }

                        return GenerateNewCode(batchProcess.Code);
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
                return "SR-00001";
            }

            public string GenerateNewCode(string soNumber)
            {
                string fetchNo = soNumber.Substring(3);
                Int32 autoNo = Convert.ToInt32((fetchNo));
                var format = "00000";
                autoNo++;
                var newCode = "SR-" + autoNo.ToString(format);
                return newCode;
            }
        }
    }
}
