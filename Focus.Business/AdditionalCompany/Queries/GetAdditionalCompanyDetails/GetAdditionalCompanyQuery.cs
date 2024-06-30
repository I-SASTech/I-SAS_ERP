using AutoMapper;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.AdditionalCompany.Queries.GetAdditionalCompanyDetails
{
    public class GetAdditionalCompanyQuery : IRequest<CompanyInformationLookUpModel>
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<GetAdditionalCompanyQuery, CompanyInformationLookUpModel>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                ILogger<GetAdditionalCompanyQuery> logger,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<CompanyInformationLookUpModel> Handle(GetAdditionalCompanyQuery request, CancellationToken cancellationToken)
            {
                var department = await _context.companyInformations.FindAsync(request.Id);

                if (department != null)
                {
                    return CompanyInformationLookUpModel.Create(department);
                }
                throw new NotFoundException(nameof(department), request.Id);
            }
        }
    }
}
