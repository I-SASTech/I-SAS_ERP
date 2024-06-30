using Focus.Business.CostCenters.Models;
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

namespace Focus.Business.CostCenters.Queries
{
    public class GetAllAccountsList : IRequest<List<AllAccountsLookupModel>>
    {
        public class Handler : IRequestHandler<GetAllAccountsList, List<AllAccountsLookupModel>>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<GetAllAccountsList> logger)
            {
                _context = context;
                _logger = logger;
            }
            public async Task<List<AllAccountsLookupModel>> Handle(GetAllAccountsList request, CancellationToken cancellationToken)
            {
                try
                {
                    var accounts = await _context.Accounts.AsNoTracking().Include(x => x.CostCenter).Select(x => new AllAccountsLookupModel()
                    {
                       Code = x.Code,
                       AccountName = x.Name,
                       AccountNameArabic = x.NameArabic,
                       CostCenterName = x.CostCenter.Name,
                    }).OrderBy(x => x.Code).ToListAsync();

                    return accounts;
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception.Message);
                    throw new ApplicationException("List Error");
                }
            }
        }
    }
}
