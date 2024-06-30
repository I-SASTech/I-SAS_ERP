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

namespace Focus.Business.Origins.Queries.GetOriginDetails
{
    public class GetOriginDetailQuery : IRequest<OriginDetailsLookUpModel>
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<GetOriginDetailQuery, OriginDetailsLookUpModel>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                ILogger<GetOriginDetailQuery> logger,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<OriginDetailsLookUpModel> Handle(GetOriginDetailQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var origin = await _context.Origins.FindAsync(request.Id);

                    if (origin != null)
                    {
                        return OriginDetailsLookUpModel.Create(origin);
                    }
                    throw new NotFoundException(nameof(origin), request.Id);
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
