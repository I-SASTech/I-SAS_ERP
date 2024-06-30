using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.PurchasePosts.Queries.GetPurchasePostRegistrationNo
{
    public class GetPurchasePostRegistrationNoQuery : IRequest<string>
    {

        public bool IsPurchaseReturn { get;  set; }

        public class Handler : IRequestHandler<GetPurchasePostRegistrationNoQuery, string>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<GetPurchasePostRegistrationNoQuery> logger)
            {
                _context = context;
                _logger = logger;
            }

            public async Task<string> Handle(GetPurchasePostRegistrationNoQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var purchase = await _context.PurchasePosts
                        .OrderBy(x => x.RegistrationNo)
                        .Where(x=>x.IsPurchaseReturn==request.IsPurchaseReturn)
                        .LastOrDefaultAsync(cancellationToken);

                    if (purchase != null)
                    {
                        if (string.IsNullOrEmpty(purchase.RegistrationNo))
                        {
                            return GenerateCodeFirstTime(request.IsPurchaseReturn);
                        }

                        return GenerateNewCode(purchase.RegistrationNo, request.IsPurchaseReturn);
                    }

                    return GenerateCodeFirstTime(request.IsPurchaseReturn);
                }
                catch (Exception e)
                {
                    _logger.LogInformation(e.Message);
                    throw new ApplicationException(e.Message);
                }
            }

            public string GenerateCodeFirstTime(bool invoiceReturn)
            {
                if(!invoiceReturn)
                {
                    return "PIP-00001";
                }
                return "PIR-00001";
            }

            public string GenerateNewCode(string soNumber, bool invoiceReturn)
            {
                string fetchNo = soNumber.Substring(4);
                Int32 autoNo = Convert.ToInt32((fetchNo));
                var format = "00000";
                autoNo++;
                string prefix;
                if (!invoiceReturn)
                {
                    prefix = "PIP-";
                }
                else
                {
                    prefix = "PIR-";
                }
                var newCode = prefix + autoNo.ToString(format);
                return newCode;
            }
        }
    }
}
