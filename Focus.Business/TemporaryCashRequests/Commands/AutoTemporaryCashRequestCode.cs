using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.TemporaryCashRequests.Commands
{
    public class AutoTemporaryCashRequestCode : IRequest<string>
    {
        public class Handler : IRequestHandler<AutoTemporaryCashRequestCode, string>
        {
            public readonly IApplicationDbContext Context;
            public readonly ILogger Logger;

            public Handler(IApplicationDbContext context, ILogger<AutoTemporaryCashRequestCode> logger)
            {
                Context = context;
                Logger = logger;
            }

            public async Task<string> Handle(AutoTemporaryCashRequestCode request, CancellationToken cancellationToken)
            {
                try
                {
                    var autoCode =await Context.TemporaryCashRequests.OrderBy(x=>x.RegistrationNo).LastOrDefaultAsync(cancellationToken: cancellationToken);
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
                return "TC-00001";
            }

            public string GenerateNewCode(string soNumber)
            {
                string fetchNo = soNumber.Substring(3);
                Int32 autoNo = Convert.ToInt32((fetchNo));
                var format = "00000";
                autoNo++;
                var newCode = "TC-" + autoNo.ToString(format);
                return newCode;
            }
        }

    }
}
