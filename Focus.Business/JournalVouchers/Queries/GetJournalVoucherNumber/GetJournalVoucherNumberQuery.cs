using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.JournalVouchers.Queries.GetJournalVoucherNumber
{
    public class GetJournalVoucherNumberQuery : IRequest<string>
    {
        public string formName;
        public Guid? BranchId { get; set; }


        public class Handler : IRequestHandler<GetJournalVoucherNumberQuery, string>
        {
            public readonly IApplicationDbContext Context;
            public readonly ILogger Logger;

            public Handler(IApplicationDbContext context, ILogger<GetJournalVoucherNumberQuery> logger)
            {
                Context = context;
                Logger = logger;
            }

            public async Task<string> Handle(GetJournalVoucherNumberQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    bool openingCash;
                    string branchCode = "";
                    string autoCode;

                    if (request.formName == "OpeningCash")
                    {
                        openingCash = true;
                    }
                    else
                    {
                        openingCash = false;

                    }
                    if (request.BranchId != null)
                    {
                        autoCode = await Context.JournalVouchers
                            .Where(x =>  x.BranchId == request.BranchId)
                            .Select(x => x.VoucherNumber)
                            .OrderBy(x => x)
                            .LastOrDefaultAsync(cancellationToken);

                        var branch = await Context.Branches
                            .Where(x => x.Id == request.BranchId)
                            .Select(x => x.Code)
                            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

                        string firstLetter = branch.Substring(0, 1);
                        var fetchNo = Convert.ToInt32(branch.Substring(2));
                        branchCode = firstLetter + fetchNo + "-";
                    }
                    else
                    {
                         autoCode = await Context.JournalVouchers
                             .Where(x => x.OpeningCash == openingCash)
                             .Select(x => x.VoucherNumber)
                             .OrderBy(x => x)
                             .LastOrDefaultAsync(cancellationToken);

                    }
                    if (autoCode != null)
                    {
                        if (string.IsNullOrEmpty(autoCode))
                        {
                            return GenerateCodeFirstTime(request.formName, branchCode);
                        }
                        return GenerateNewCode(autoCode, request.formName, branchCode);
                    }

                    return GenerateCodeFirstTime(request.formName, branchCode);
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex.Message);
                    throw new ApplicationException("Unable to process your request please contact support");
                }
            }

            public string GenerateCodeFirstTime( string formName,string branchCode)
            {
                if (formName == "OpeningCash")
                {
                    return branchCode + "OC-00001";
                }

                else
                {
                    return branchCode + "JV-00001";

                }
            }

            public string GenerateNewCode(string soNumber,string formName, string branchCode)
            {
                if (formName == "OpeningCash")
                {
                    var fetchNo = soNumber.Substring(soNumber.Length - 5);
                    var autoNo = Convert.ToInt32((fetchNo));
                    var format = "00000";
                    autoNo++;
                    var newCode = "OC-" + autoNo.ToString(format);
                    return branchCode + newCode;
                }
                else
                {
                    var fetchNo = soNumber.Substring(soNumber.Length - 5);
                    var autoNo = Convert.ToInt32((fetchNo));
                    var format = "00000";
                    autoNo++;
                    var newCode = "JV-" + autoNo.ToString(format);
                    return branchCode + newCode;
                }


            }
        }
    }
}
