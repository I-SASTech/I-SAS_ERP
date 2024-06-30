using AutoMapper;
using AutoMapper.QueryableExtensions;
using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.AdditionalCompany.Queries.GetAdditionalCompanyList
{
    public class GetCompanyInformationListQuery : IRequest<AdditionalCompanyListModel>
    {
        public Guid CompanyId { get; set; }

        public class Handler : IRequestHandler<GetCompanyInformationListQuery, AdditionalCompanyListModel>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                ILogger<GetCompanyInformationListQuery> logger,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<AdditionalCompanyListModel> Handle(GetCompanyInformationListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var companyInformation = _context.companyInformations.AsQueryable();

                    var companyInformationList = await companyInformation
                        .OrderBy(x => x.NameArabic)
                        .Where(x => x.CompanyId == request.CompanyId)
                        .ProjectTo<CompanyInformationLookUpModel>(_mapper.ConfigurationProvider)
                        .ToListAsync(cancellationToken);

                    return new AdditionalCompanyListModel
                    {
                        CompanyInformation = companyInformationList
                    };
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
