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

namespace Focus.Business.Sizes.Queries.GetSizeDetails
{
    public class GetSizeDetailQuery : IRequest<SizeDetailsLookUpModel>
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<GetSizeDetailQuery, SizeDetailsLookUpModel>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                ILogger<GetSizeDetailQuery> logger,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<SizeDetailsLookUpModel> Handle(GetSizeDetailQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var size = await _context.Sizes.FindAsync(request.Id);

                    if (size != null)
                    {
                        return SizeDetailsLookUpModel.Create(size);
                    }
                    throw new NotFoundException(nameof(size), request.Id);
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
