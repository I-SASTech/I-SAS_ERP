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

namespace Focus.Business.Brands.Queries.GetBrandDetails
{
    public class GetBrandDetailQuery : IRequest<BrandDetailsLookUpModel>
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<GetBrandDetailQuery, BrandDetailsLookUpModel>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                ILogger<GetBrandDetailQuery> logger,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<BrandDetailsLookUpModel> Handle(GetBrandDetailQuery request, CancellationToken cancellationToken)
            {
                var brand = await _context.Brands.FindAsync(request.Id);

                try
                {
                    if (brand != null)
                    {
                        return BrandDetailsLookUpModel.Create(brand);
                    }
                    throw new NotFoundException(nameof(brand), request.Id);
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
