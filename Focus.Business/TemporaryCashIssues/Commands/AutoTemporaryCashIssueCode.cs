using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.TemporaryCashIssues.Commands
{
    public class AutoTemporaryCashIssueCode : IRequest<string>
    {
        public class Handler : IRequestHandler<AutoTemporaryCashIssueCode, string>
        {
            public readonly IApplicationDbContext Context;
            public readonly ILogger Logger;

            public Handler(IApplicationDbContext context, ILogger<AutoTemporaryCashIssueCode> logger)
            {
                Context = context;
                Logger = logger;
            }

            public async Task<string> Handle(AutoTemporaryCashIssueCode request, CancellationToken cancellationToken)
            {
                try
                {
                    var autoCode = await Context.TemporaryCashIssues.OrderBy(x => x.RegistrationNo).LastOrDefaultAsync(cancellationToken: cancellationToken);
                    if (autoCode != null)
                    {
                        if (string.IsNullOrEmpty(autoCode.RegistrationNo))
                        {
                            return GenerateCodeFirstTime();
                        }
                        return GenerateNewCode(autoCode.RegistrationNo);
                    }

                    return GenerateCodeFirstTime();
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex.Message);
                    throw new ApplicationException("Unable to process your request please contact support");
                }
            }

            public string GenerateCodeFirstTime()
            {
                return "TCI-00001";
            }

            public string GenerateNewCode(string soNumber)
            {
                string fetchNo = soNumber.Substring(4);
                Int32 autoNo = Convert.ToInt32((fetchNo));
                var format = "00000";
                autoNo++;
                var newCode = "TCI-" + autoNo.ToString(format);
                return newCode;
            }
        }

    }
}
