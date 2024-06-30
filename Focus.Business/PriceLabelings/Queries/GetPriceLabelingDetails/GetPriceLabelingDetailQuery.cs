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

namespace Focus.Business.PriceLabelings.Queries.GetPriceLabelingDetails
{
    public class GetPriceLabelingDetailQuery : IRequest<PriceLabelingDetailsLookUpModel>
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<GetPriceLabelingDetailQuery, PriceLabelingDetailsLookUpModel>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                ILogger<GetPriceLabelingDetailQuery> logger,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<PriceLabelingDetailsLookUpModel> Handle(GetPriceLabelingDetailQuery request, CancellationToken cancellationToken)
            {
                var priceLabeling = await _context.PriceLabelings.FindAsync(request.Id);

                try
                {
                    if (priceLabeling != null)
                    {
                        return PriceLabelingDetailsLookUpModel.Create(priceLabeling);
                    }
                    throw new NotFoundException(nameof(priceLabeling), request.Id);
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
