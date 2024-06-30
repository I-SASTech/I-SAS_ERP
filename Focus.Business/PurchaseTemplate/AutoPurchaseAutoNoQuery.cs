using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.PurchaseTemplate
{
    public class AutoPurchaseAutoNoQuery : IRequest<string>
    {
        public Guid? BranchId { get; set; }

        public class Handler : IRequestHandler<AutoPurchaseAutoNoQuery, string>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<AutoPurchaseAutoNoQuery> logger)
            {
                _context = context;
                _logger = logger;
            }

            public async Task<string> Handle(AutoPurchaseAutoNoQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    string purchaseTemplate;
                    string branchCode = "";
                    if (request.BranchId != null)
                    {
                        purchaseTemplate = await _context.AutoPurchaseTemplates
                            .AsNoTracking()
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
                        purchaseTemplate = await _context.AutoPurchaseTemplates
                            .AsNoTracking()
                            .Select(x => x.RegistrationNo)
                            .OrderBy(x => x)
                            .LastOrDefaultAsync(cancellationToken);
                    }


                    if (purchaseTemplate != null)
                    {
                        if (string.IsNullOrEmpty(purchaseTemplate))
                        {
                            return GenerateCodeFirstTime(branchCode);
                        }

                        return GenerateNewCode(purchaseTemplate, branchCode);
                    }

                    return GenerateCodeFirstTime(branchCode);
                }
                catch (Exception e)
                {
                    _logger.LogInformation(e.Message);
                    throw new ApplicationException(e.Message);
                }
            }

            private string GenerateCodeFirstTime(string branchCode)
            {
                return branchCode + "APO-00001";
            }

            private string GenerateNewCode(string soNumber, string branchCode)
            {
                string fetchNo = soNumber.Substring(soNumber.Length - 5);
                Int32 autoNo = Convert.ToInt32((fetchNo));
                var format = "00000";
                autoNo++;
                var newCode = "APO-" + autoNo.ToString(format);
                return branchCode + newCode;
            }
        }

    }
}
