using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.MobileOrders.Queries.GetMobileOrderRegistrationNo
{
    public class GetMobileOrderRegistrationNoQuery : IRequest<string>
    {
        public class Handler : IRequestHandler<GetMobileOrderRegistrationNoQuery, string>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<GetMobileOrderRegistrationNoQuery> logger)
            {
                _context = context;
                _logger = logger;
            }

            public async Task<string> Handle(GetMobileOrderRegistrationNoQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var purchaseOrder = await _context.MobileOrders
                        .OrderBy(x => x.OrderNo)
                        .LastOrDefaultAsync(cancellationToken);

                    if (purchaseOrder != null)
                    {
                        if (string.IsNullOrEmpty(purchaseOrder.OrderNo))
                        {
                            return GenerateCodeFirstTime();
                        }

                        return GenerateNewCode(purchaseOrder.OrderNo);
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
                return "MO-00001";
            }

            public string GenerateNewCode(string soNumber)
            {
                string fetchNo = soNumber.Substring(3);
                Int32 autoNo = Convert.ToInt32((fetchNo));
                var format = "00000";
                autoNo++;
                var newCode = "MO-" + autoNo.ToString(format);
                return newCode;
            }
        }
    }
}

