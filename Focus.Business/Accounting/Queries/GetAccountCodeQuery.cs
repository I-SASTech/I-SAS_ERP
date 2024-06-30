using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Drawing;
using Focus.Business.Interface;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.Accounting.Queries
{
    public class GetAccountCodeQuery : IRequest<string>
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<GetAccountCodeQuery, string>
        {
            public readonly IApplicationDbContext Context;
            public readonly ILogger Logger;

            public string Code { get;  set; }

            public Handler(IApplicationDbContext context, ILogger<GetAccountCodeQuery> logger)
            {
                Context = context;
                Logger = logger;
            }

            public async Task<string> Handle(GetAccountCodeQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var costCenter = await Context.CostCenters.Where(x => x.Id == request.Id).OrderBy(x=>x.Id).LastOrDefaultAsync(cancellationToken);
                    Account autoCode = null;
                    Code = costCenter.Code;
                    autoCode = costCenter.Code == "101000" ? costCenter.Accounts.OrderBy(x => x.Code).LastOrDefault(x => !x.Code.Contains("TR") && !x.Code.Contains("CC")) : costCenter.Accounts.OrderBy(x => x.Code).LastOrDefault();

                    if (autoCode != null)
                    {
                        if (string.IsNullOrEmpty(autoCode.Code))
                        {
                            return GenerateCodeFirstTime();
                        }
                        return GenerateNewCode(autoCode.Code);
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
                Int32 autoNo = Convert.ToInt32((Code));
                autoNo++;
                return autoNo.ToString(); 
            }

            public string GenerateNewCode(string soNumber)
            {
                Int32 autoNo = Convert.ToInt32((soNumber));
                autoNo++;
                var newCode = autoNo.ToString();
                return newCode;
                //string fetchNo = soNumber.Substring(3);
                //Int32 autoNo = Convert.ToInt32((fetchNo));
                //var format = "BR-00000";
                //autoNo++;

            }
        }
    }
}
