
using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.PurchaseBills.Queries.GetPurchaseBillRegistrationNo
{
    public class GetPurchaseBillRegistrationNoQuery : IRequest<string>
    {
        public Guid? BranchId { get; set; }

        public class Handler : IRequestHandler<GetPurchaseBillRegistrationNoQuery, string>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<GetPurchaseBillRegistrationNoQuery> logger)
            {
                _context = context;
                _logger = logger;
            }

            public async Task<string> Handle(GetPurchaseBillRegistrationNoQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    string branchCode = "";
                    string autoCode;

                    if (request.BranchId != null)
                    {
                        autoCode = await _context.PurchaseBills
                            .Where(x => x.BranchId == request.BranchId)
                            .Select(x => x.RegistrationNo)
                            .OrderBy(x => x)
                            .LastOrDefaultAsync(cancellationToken);

                        var branch = await _context.Branches
                            .Where(x => x.Id == request.BranchId)
                            .Select(x => x.Code)
                            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

                        string firstLetter = branch.Substring(0, 1);
                        var fetchNo = Convert.ToInt32(branch.Substring(2));
                        branchCode = firstLetter + fetchNo + "-";
                    }
                    else
                    {
                        autoCode = await _context.PurchaseBills
                            .Select(x => x.RegistrationNo)
                            .OrderBy(x => x)
                            .LastOrDefaultAsync(cancellationToken);
                    }

                   
                    if (autoCode != null)
                    {
                        if (string.IsNullOrEmpty(autoCode))
                        {
                            return GenerateCodeFirstTime(branchCode);
                        }

                        return GenerateNewCode(autoCode, branchCode);
                    }

                    return GenerateCodeFirstTime(branchCode);
                }
                catch (Exception e)
                {
                    _logger.LogInformation(e.Message);
                    throw new ApplicationException(e.Message);
                }
            }

            public string GenerateCodeFirstTime(string branchCode)
            {
                return branchCode + "PB-00001";
            }

            public string GenerateNewCode(string soNumber, string branchCode)
            {
                var fetchNo = soNumber.Substring(soNumber.Length - 5);
                var autoNo = Convert.ToInt32((fetchNo));
                var format = "00000";
                autoNo++;
                var newCode = "PB-" +autoNo.ToString(format);
                return branchCode + newCode;
            }
        }
    }
}

