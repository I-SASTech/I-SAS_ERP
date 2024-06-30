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

namespace Focus.Business.ProductMasters.Queries.GetProductMasterDetails
{
    public class GetProductMasterDetailQuery : IRequest<ProductMasterDetailsLookUpModel>
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<GetProductMasterDetailQuery, ProductMasterDetailsLookUpModel>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                ILogger<GetProductMasterDetailQuery> logger,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<ProductMasterDetailsLookUpModel> Handle(GetProductMasterDetailQuery request, CancellationToken cancellationToken)
            {
                var productMaster = await _context.ProductMasters.FindAsync(request.Id);

                try
                {
                    if (productMaster != null)
                    {
                        return ProductMasterDetailsLookUpModel.Create(productMaster);
                    }
                    throw new NotFoundException(nameof(productMaster), request.Id);
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
