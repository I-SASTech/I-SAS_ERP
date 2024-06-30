using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Focus.Domain.Enum;

namespace Focus.Business.Terminals.Queries
{
    public class GetTerminalCodeQuery : IRequest<string>
    {

        public TerminalType TerminalType { get; set; }
        public class Handler : IRequestHandler<GetTerminalCodeQuery, string>
        {
            public readonly IApplicationDbContext Context;
            public readonly ILogger Logger;

            public Handler(IApplicationDbContext context, ILogger<GetTerminalCodeQuery> logger)
            {
                Context = context;
                Logger = logger;
            }

            public async Task<string> Handle(GetTerminalCodeQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var autoCode = await Context.Terminals.OrderBy(x => x.Code).LastOrDefaultAsync(x=>x.TerminalType == request.TerminalType,cancellationToken);
                    if (autoCode != null)
                    {
                        if (string.IsNullOrEmpty(autoCode.Code))
                        {
                            return GenerateCodeFirstTime(request.TerminalType);
                        }
                        return GenerateNewCode(autoCode.Code,request.TerminalType);
                    }

                    return GenerateCodeFirstTime(request.TerminalType);
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex.Message);
                    throw new ApplicationException("Unable to process your request please contact support");
                }
            }

            public string GenerateCodeFirstTime(TerminalType terminalType)
            {
                if(terminalType == TerminalType.CashCounter)
                    return "CC-00001";
                else
                {
                    return "TR-00001";
                }
            }

            public string GenerateNewCode(string soNumber, TerminalType terminalType)
            {
                string fetchNo = soNumber.Substring(3);
                Int32 autoNo = Convert.ToInt32((fetchNo));
                var format = "00000";
                autoNo++;
                if (terminalType == TerminalType.CashCounter)
                {
                    var newCode = "CC-" + autoNo.ToString(format);
                    return newCode;
                }
                else
                {
                    var newCode = "TR-" + autoNo.ToString(format);
                    return newCode;
                }
                
            }
        }

    }
}
