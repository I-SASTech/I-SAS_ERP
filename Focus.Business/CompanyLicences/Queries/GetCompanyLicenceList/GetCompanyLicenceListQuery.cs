using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.CompanyLicences.Queries.GetCompanyLicenceList
{
    public class GetCompanyLicenceListQuery : IRequest<List<GetCompanyLicenceListLookupModel>>
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<GetCompanyLicenceListQuery, List<GetCompanyLicenceListLookupModel>>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<GetCompanyLicenceListQuery> logger)
            {
                _context = context;
                _logger = logger;
            }

            public async Task<List<GetCompanyLicenceListLookupModel>> Handle(GetCompanyLicenceListQuery request,
                CancellationToken cancellationToken)
            {
                try
                {
                    var companyLicences = await _context.CompanyLicences
                        .AsNoTracking()
                        .Where(x => x.Id == request.Id).ToListAsync(cancellationToken);

                    return GetCompanyLicenceListLookupModel.Create(companyLicences);
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
