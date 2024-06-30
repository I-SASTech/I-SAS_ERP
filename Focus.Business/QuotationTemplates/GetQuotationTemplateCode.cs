using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.HR.Payroll.QuotationTemplates
{
   public class GetQuotationTemplateCode : IRequest<string>
   {
        public bool IsService { get; set; }
        public class Handler : IRequestHandler<GetQuotationTemplateCode, string>
       {
           public readonly IApplicationDbContext Context;
           public readonly ILogger Logger;

           public Handler(IApplicationDbContext context, ILogger<GetQuotationTemplateCode> logger)
           {
               Context = context;
               Logger = logger;
           }

           public async Task<string> Handle(GetQuotationTemplateCode request, CancellationToken cancellationToken)
           {
                try
                {
                    var purchaseOrder = await Context.QuotationTemplates
                        .OrderBy(x => x.RegistrationNo)
                        .Where(x=>x.IsService==request.IsService)
                        .LastOrDefaultAsync(cancellationToken);

                    if (purchaseOrder != null)
                    {
                        if (string.IsNullOrEmpty(purchaseOrder.RegistrationNo))
                        {
                            return GenerateCodeFirstTime(request.IsService);
                        }

                        return GenerateNewCode(purchaseOrder.RegistrationNo,request.IsService);
                    }

                    return GenerateCodeFirstTime(request.IsService);
                }
                catch (Exception e)
                {
                    Logger.LogInformation(e.Message);
                    throw new ApplicationException(e.Message);
                }
            }

            public string GenerateCodeFirstTime(bool IsService)
            {
                if(IsService)
                {
                    return "ST-00001";
                }
                else
                {
                    return "QT-00001";
                }
               
            }

            public string GenerateNewCode(string soNumber,bool IsService)
            {
                string fetchNo = soNumber.Substring(3);
                Int32 autoNo = Convert.ToInt32((fetchNo));
                var format = "00000";
                autoNo++;
                var newCode = IsService ? "ST-" + autoNo.ToString(format) : "QT-" + autoNo.ToString(format);

                return newCode;
            }
        }
    }
}

