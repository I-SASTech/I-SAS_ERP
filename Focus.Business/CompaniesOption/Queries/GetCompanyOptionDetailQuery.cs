using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Focus.Business.CompaniesOption.Commands.AddUpdateOption;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Focus.Business.CompaniesOption.Queries
{
    public class GetCompanyOptionDetailQuery:IRequest<CompanyOptionLookUp>
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<GetCompanyOptionDetailQuery, CompanyOptionLookUp>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                ILogger<GetCompanyOptionDetailQuery> logger,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<CompanyOptionLookUp> Handle(GetCompanyOptionDetailQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var companyOption = await _context.CompanyOptions.FindAsync(request.Id);

                    if (companyOption != null)
                    {
                        return new CompanyOptionLookUp()
                        {
                            Id = companyOption.Id,
                            Label = companyOption.Label,
                            Value = companyOption.Value,
                        };
                    }
                    throw new NotFoundException(nameof(companyOption), request.Id);
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
