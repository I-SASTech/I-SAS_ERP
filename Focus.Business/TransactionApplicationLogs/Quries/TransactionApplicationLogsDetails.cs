using Focus.Business.Interface;
using Focus.Business.TransactionApplicationLogs.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.TransactionApplicationLogs.Quries
{
    public class TransactionApplicationLogsDetails : IRequest<TransactionApplicationLogsLookupModel>
    {

        public Guid LocationId { get; set; }
        public class Handler : IRequestHandler<TransactionApplicationLogsDetails, TransactionApplicationLogsLookupModel>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private string _code;
            private readonly IMediator _mediator;
            public Handler(IApplicationDbContext context, ILogger<TransactionApplicationLogsDetails> logger, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
            }
            public async Task<TransactionApplicationLogsLookupModel> Handle(TransactionApplicationLogsDetails request, CancellationToken cancellationToken)
            {
                try
                {
                    var prefix = await _context.TransactionApplicationLogs.FirstOrDefaultAsync(cancellationToken);
                    if (prefix == null)
                    {
                        return null;

                    }
                    else
                    {
                        return new TransactionApplicationLogsLookupModel
                        {
                            Id = prefix.Id,
                            FreshLogMovingDays = prefix.FreshLogMovingDays,
                            DeleteFromHistory = prefix.DeleteFromHistory,
                            Date = prefix.Date,
                            IsActive = prefix.IsActive,
                            LocationId = prefix.LocationId,


                        };
                    }

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    throw new ApplicationException("Something went wrong " + ex.Message);
                }
            }
        }
    }
}
