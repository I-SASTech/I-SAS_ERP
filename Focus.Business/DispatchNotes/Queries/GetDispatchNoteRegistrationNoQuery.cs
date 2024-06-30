using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.DispatchNotes.Queries
{
    public class GetDispatchNoteRegistrationNoQuery : IRequest<string>
    {
        public Guid? BranchId { get; set; }

        public class Handler : IRequestHandler<GetDispatchNoteRegistrationNoQuery, string>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<GetDispatchNoteRegistrationNoQuery> logger)
            {
                _context = context;
                _logger = logger;
            }

            public async Task<string> Handle(GetDispatchNoteRegistrationNoQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    string branchCode = "";
                    string saleOrder;
                    if (request.BranchId != null)
                    {
                        saleOrder = await _context.DispatchNotes
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
                        saleOrder = await _context.DispatchNotes
                            .Select(x => x.RegistrationNo)
                            .OrderBy(x => x)
                            .LastOrDefaultAsync(cancellationToken);
                    }

                    if (saleOrder != null)
                    {
                        if (string.IsNullOrEmpty(saleOrder))
                        {
                            return GenerateCodeFirstTime(branchCode);
                        }

                        return GenerateNewCode(saleOrder, branchCode);
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
                return branchCode + "DN-00001";
            }

            private string GenerateNewCode(string soNumber, string branchCode)
            {
                string fetchNo = soNumber.Substring(soNumber.Length - 5);

                Int32 autoNo = Convert.ToInt32((fetchNo));
                var format = "00000";
                autoNo++;
                var newCode = "DN-" + autoNo.ToString(format);
                return branchCode + newCode;
            }
        }

    }
}

