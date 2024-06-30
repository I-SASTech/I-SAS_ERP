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

namespace Focus.Business.Logistics.Queries.GetLogisticDetails
{
    public class GetLogisticDetailQuery : IRequest<LogisticDetailsLookUpModel>
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<GetLogisticDetailQuery, LogisticDetailsLookUpModel>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                ILogger<GetLogisticDetailQuery> logger,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<LogisticDetailsLookUpModel> Handle(GetLogisticDetailQuery request, CancellationToken cancellationToken)
            {
                var logistic = await _context.Logistics.FindAsync(request.Id);

                try
                {
                    if (logistic != null)
                    {
                        return LogisticDetailsLookUpModel.Create(logistic);
                    }
                    throw new NotFoundException(nameof(logistic), request.Id);
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
