using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Spreadsheet;
using Focus.Business.Interface;
using Focus.Business.Prefix.Quries;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.EmployeeRegistrations.Queries
{
    public class GetEmployeeRegistrationCodeQuery : IRequest<string>
    {

        public class Handler : IRequestHandler<GetEmployeeRegistrationCodeQuery, string>
        {
            public readonly IApplicationDbContext Context;
            public readonly ILogger Logger;
            private readonly IMediator _mediator;

            public Handler(IApplicationDbContext context, ILogger<GetEmployeeRegistrationCodeQuery> logger, IMediator mediator)
            {
                Context = context;
                Logger = logger;
                _mediator = mediator;
            }

            public async Task<string> Handle(GetEmployeeRegistrationCodeQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var autoCode = await Context.EmployeeRegistrations.OrderBy(x => x.Code).LastOrDefaultAsync(cancellationToken);
                    var prefixRecord = await _mediator.Send(new PrefixiesDetails(), cancellationToken);
                    string middleCode = string.Empty;

                    if (autoCode != null)
                    {
                        string code = null;
                        if (string.IsNullOrEmpty(autoCode.Code))
                        {
                            middleCode = "EM";
                            return prefixRecord.Employee + "-" + (string.IsNullOrEmpty(middleCode) ? "" : middleCode + "-") + "00001";
                        }
                        else
                        {
                            string fetchNo = autoCode.Code.Substring(autoCode.Code.Length - 5);
                            //fetchNo =  fetchNo.Substring(fetchNo.Length-5);
                            Int32 autoNo = Convert.ToInt32((fetchNo));
                            var format = "00000";
                            autoNo++;


                            if (autoCode.Code.Contains("--"))
                            {
                                int firstIndex = autoCode.Code.IndexOf("-");
                                int secondIndex = autoCode.Code.IndexOf("-", firstIndex + 1);
                                string result = autoCode.Code.Substring(firstIndex + 1, secondIndex - firstIndex - 1);
                                middleCode = result;
                            }


                                code = prefixRecord.Employee + "-"  +(string.IsNullOrEmpty(middleCode) ? "" : middleCode + "-") + autoNo.ToString(format);
                        }
                     return code;
                    }
                    else
                    {
                        middleCode = "EM";
                       return prefixRecord.Employee + "-" + (string.IsNullOrEmpty(middleCode) ? "" : middleCode + "-") + "00001";
                    }

                }
                catch (Exception ex)
                {
                    Logger.LogError(ex.Message);
                    throw new ApplicationException("Unable to process your request please contact support");
                }
            }

           

            public string GenerateNewCode(string soNumber)
            {
                string fetchNo = soNumber.Substring(3);
                Int32 autoNo = Convert.ToInt32((fetchNo));
                var format = "00000";
                autoNo++;
                var newCode = "EM-" + autoNo.ToString(format);
                return newCode;
            }
        }
    }
}
