using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.GoodReceives
{
    public class GetGoodReceiveCode : IRequest<string>
    {
        public Guid? BranchId { get; set; }

        public class Handler : IRequestHandler<GetGoodReceiveCode, string>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<GetGoodReceiveCode> logger)
            {
                _context = context;
                _logger = logger;
            }

            public async Task<string> Handle(GetGoodReceiveCode request, CancellationToken cancellationToken)
            {
                try
                {
                    string purchaseOrder;
                    string branchCode = "";
                    if (request.BranchId != null)
                    {
                        purchaseOrder = await _context.GoodReceives
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
                        purchaseOrder = await _context.GoodReceives
                            .Select(x => x.RegistrationNo)
                            .OrderBy(x => x)
                            .LastOrDefaultAsync(cancellationToken);
                    }

                    if (purchaseOrder != null)
                    {
                        if (string.IsNullOrEmpty(purchaseOrder))
                        {
                            return GenerateCodeFirstTime(branchCode);
                        }

                        return GenerateNewCode(purchaseOrder, branchCode);
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
                return branchCode + "GR-00001";
            }

            private string GenerateNewCode(string soNumber, string branchCode)
            {
                string fetchNo = soNumber.Substring(soNumber.Length - 5);
                Int32 autoNo = Convert.ToInt32((fetchNo));
                var format = "00000";
                autoNo++;
                var newCode = "GR-" + autoNo.ToString(format);
                return branchCode + newCode;
            }
        }

    }
}

