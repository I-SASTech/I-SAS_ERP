using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.InquiryRequest.Queries.GetInquiryList
{
    public class GetInquiryCodeQuery : IRequest<string>
    {
        public Guid? BranchId { get; set; }
        public class Handler : IRequestHandler<GetInquiryCodeQuery, string>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<GetInquiryCodeQuery> logger)
            {
                _context = context;
                _logger = logger;
            }

            public async Task<string> Handle(GetInquiryCodeQuery request, CancellationToken cancellationToken)
            {
                string autoCode;
                string branchCode = "";
                if (request.BranchId!=null)
                {
                    autoCode = await _context.Inquiries
                        .Where(x =>x.BranchId == request.BranchId)
                        .Select(x=>x.InquiryNumber)
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
                    autoCode = await _context.Inquiries
                        .Select(x => x.InquiryNumber)
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

            private string GenerateCodeFirstTime(string branchCode)
            {
                return branchCode+"INQ-00001";
            }

            private string GenerateNewCode(string soNumber, string branchCode)
            {
                string fetchNo = soNumber.Substring(soNumber.Length - 5);
                Int32 autoNo = Convert.ToInt32((fetchNo));
                var format = "INQ-00000";
                autoNo++;
                var newCode = autoNo.ToString(format);
                return branchCode+newCode;
            }
        }

    }
}
