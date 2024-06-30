using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.Logistics.Queries
{
    public class GetLogisticCodeQuery : IRequest<string>
    {
        public Domain.Enum.Logistics LogisticsForm { get; set; }
        public class Handler : IRequestHandler<GetLogisticCodeQuery, string>
        {
            public readonly IApplicationDbContext Context;
            public readonly ILogger Logger;

            public Handler(IApplicationDbContext context, ILogger<GetLogisticCodeQuery> logger)
            {
                Context = context;
                Logger = logger;
            }

            public async Task<string> Handle(GetLogisticCodeQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var autoCode = await Context.Logistics.OrderBy(x => x.Code).Where(x => x.LogisticsForm == request.LogisticsForm).LastOrDefaultAsync(cancellationToken);
                    if (autoCode != null)
                    {
                        if (string.IsNullOrEmpty(autoCode.Code))
                        {
                            return GenerateCodeFirstTime(request.LogisticsForm);
                        }
                        return GenerateNewCode(autoCode.Code, request.LogisticsForm);
                    }

                    return GenerateCodeFirstTime(request.LogisticsForm);
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex.Message);
                    throw new ApplicationException("Unable to process your request please contact support");
                }
            }

            public string GenerateCodeFirstTime(Domain.Enum.Logistics type)
            {
                if (Domain.Enum.Logistics.Transporter == (Domain.Enum.Logistics)type)
                {
                    return "TC-00001";
                }
                else if (Domain.Enum.Logistics.ClearanceAgent == (Domain.Enum.Logistics)type)
                {
                    return "CA-00001";
                }
                else
                {
                    return "SL-00001";
                }
            }

            public string GenerateNewCode(string soNumber, Domain.Enum.Logistics type)
            {
                string fetchNo = soNumber.Substring(3);
                Int32 autoNo = Convert.ToInt32((fetchNo));
                var format = "00000";
                autoNo++;
                string prefix;
                if (Domain.Enum.Logistics.Transporter == (Domain.Enum.Logistics)type)
                {
                    prefix = "TC-";
                }

                else if (Domain.Enum.Logistics.ClearanceAgent == (Domain.Enum.Logistics)type)
                {
                    prefix = "CA-";
                }
               
                else
                {
                    prefix = "SL-";
                }
                var newCode = prefix + autoNo.ToString(format);
                return newCode;
            }
        }
    }
}
