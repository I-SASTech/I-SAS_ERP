using Focus.Business.Interface;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Exceptions;
using Microsoft.EntityFrameworkCore;
using MediatR;
using System.Linq;

namespace Focus.Business.CompanyLicences.Queries.GetCompanyLicenceDetails
{
    public class GetCompanyLicenceDetailsQuery : IRequest<GetCompanyLicenceLookupModel>
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<GetCompanyLicenceDetailsQuery, GetCompanyLicenceLookupModel>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<GetCompanyLicenceDetailsQuery> logger)
            {
                _context = context;
                _logger = logger;
            }

            public async Task<GetCompanyLicenceLookupModel> Handle(GetCompanyLicenceDetailsQuery request,
                CancellationToken cancellationToken)
            {
                try
                {
                    var companyLicence = await _context.CompanyLicences
                        .AsNoTracking()
                        .OrderBy(x => x.Id == request.Id).LastOrDefaultAsync(cancellationToken);

                    if (companyLicence == null)
                        throw new NotFoundException("Company Licence", request.Id);

                    return new GetCompanyLicenceLookupModel
                    {
                        Id = companyLicence.Id,
                        CompanyId = companyLicence.CompanyId,
                        CompanyType = companyLicence.CompanyType,
                        FromDate = companyLicence.FromDate,
                        ToDate = companyLicence.ToDate,
                        NumberOfUsers = companyLicence.NumberOfUsers,
                        NumberOfTransactions = (int)companyLicence.NoOfTransactionsAllow,
                        IsActive = companyLicence.IsActive,
                        IsBlock = companyLicence.IsBlock
                    };
                }
                catch (Exception ex)
                {
                    _logger.LogInformation(ex.Message);
                    throw new ApplicationException(ex.Message);
                }
            }
        }
    }
}
