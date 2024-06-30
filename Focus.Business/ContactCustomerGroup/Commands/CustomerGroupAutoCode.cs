using Focus.Business.ContactCustomerGroup.Model;
using Focus.Business.ContactCustomerGroup.Queries;
using Focus.Business.Interface;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Threading;
using System;
using DocumentFormat.OpenXml.InkML;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Focus.Business.ContactCustomerGroup.Commands
{
    public class CustomerGroupAutoCode : IRequest<string>
    {
        public class Handler : IRequestHandler<CustomerGroupAutoCode, string>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<CustomerGroupAutoCode> logger)
            {
                _context = context;
                _logger = logger;
            }
            public async Task<string> Handle(CustomerGroupAutoCode request, CancellationToken cancellationToken)
            {
                try
                {
                    string code = await AutoGenerateFunds();

                    return code;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    throw new ApplicationException("Unable to process your request please contact support");
                }
            }
            //Funds Auto Code
            public async Task<string> AutoGenerateFunds()
            {
                var funds = await _context.CustomerGroups
                       .OrderBy(x => x.Code)
                       .LastOrDefaultAsync();

                if (funds != null)
                {
                    if (string.IsNullOrEmpty(funds.Code))
                    {
                        return GenerateCodeFirstTimeFunds();
                    }

                    return GenerateNewCodeFunds(funds.Code);
                }

                return GenerateCodeFirstTimeFunds();
            }
            public string GenerateCodeFirstTimeFunds()
            {
                return "CG-00001";
            }
            public string GenerateNewCodeFunds(string soNumber)
            {
                string fetchNo = soNumber.Substring(3);
                Int32 autoNo = Convert.ToInt32((fetchNo));
                var format = "00000";
                autoNo++;
                var newCode = "CG-" + autoNo.ToString(format);
                return newCode;
            }
        }
    }
}
