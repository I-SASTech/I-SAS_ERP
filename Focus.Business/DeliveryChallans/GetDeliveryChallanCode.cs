using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.DeliveryChallans
{
    public class GetDeliveryChallanCode : IRequest<string>
    {
        public bool IsService { get; set; }
        public Guid? BranchId { get; set; }

        public class Handler : IRequestHandler<GetDeliveryChallanCode, string>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<GetDeliveryChallanCode> logger)
            {
                _context = context;
                _logger = logger;
            }

            public async Task<string> Handle(GetDeliveryChallanCode request, CancellationToken cancellationToken)
            {
                try
                {
                    string branchCode = "";
                    string purchaseOrder;
                    if (request.BranchId != null)
                    {
                        purchaseOrder = await _context.DeliveryChallans
                            .Where(x => x.IsService == request.IsService && x.BranchId == request.BranchId)
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
                        purchaseOrder = await _context.DeliveryChallans
                            .Where(x => x.IsService == request.IsService)
                            .Select(x => x.RegistrationNo)
                            .OrderBy(x => x)
                            .LastOrDefaultAsync(cancellationToken);
                    }

                    if (purchaseOrder != null)
                    {
                        if (string.IsNullOrEmpty(purchaseOrder))
                        {
                            return GenerateCodeFirstTime(request.IsService, branchCode);
                        }

                        return GenerateNewCode(purchaseOrder, request.IsService, branchCode);
                    }

                    return GenerateCodeFirstTime(request.IsService, branchCode);
                }
                catch (Exception e)
                {
                    _logger.LogInformation(e.Message);
                    throw new ApplicationException(e.Message);
                }
            }

            private string GenerateCodeFirstTime(bool isService, string branchCode)
            {
                if (isService)
                {
                    return branchCode + "SN-00001";
                }
                else
                {
                    return branchCode + "DN-00001";
                }

            }

            private string GenerateNewCode(string soNumber, bool isService, string branchCode)
            {
                string fetchNo = soNumber.Substring(soNumber.Length - 5);
                var autoNo = Convert.ToInt32((fetchNo));
                var format = "00000";
                autoNo++;
                var newCode = isService ? "SN-" + autoNo.ToString(format) : "DN-" + autoNo.ToString(format);

                return branchCode + newCode;
            }
        }
    }
}

