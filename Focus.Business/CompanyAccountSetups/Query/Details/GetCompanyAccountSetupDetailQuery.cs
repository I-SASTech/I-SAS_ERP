using AutoMapper;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.Brands.Queries.GetBrandDetails
{
    public class GetCompanyAccountSetupDetailQuery : IRequest<CompanyAccountSetupDetailsLookUpModel>
    {
        //public Guid Id { get; set; }

        public class Handler : IRequestHandler<GetCompanyAccountSetupDetailQuery, CompanyAccountSetupDetailsLookUpModel>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                ILogger<GetCompanyAccountSetupDetailQuery> logger,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<CompanyAccountSetupDetailsLookUpModel> Handle(GetCompanyAccountSetupDetailQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var companyAccountSetupId = _context.CompanyAccountSetups.Max(u => u.Id);
                    var companyAccountSetup = await _context.CompanyAccountSetups.FindAsync(companyAccountSetupId);

                    if (companyAccountSetup != null)
                    {
                        return CompanyAccountSetupDetailsLookUpModel.Create(companyAccountSetup);
                    }
                    throw new NotFoundException(nameof(companyAccountSetup), companyAccountSetupId);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    throw new ApplicationException("Unable to process your request please contact support");
                }
            }
        }
    }
}
