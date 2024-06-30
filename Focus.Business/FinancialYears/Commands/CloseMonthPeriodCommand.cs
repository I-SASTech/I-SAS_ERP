using Focus.Business.Interface;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Common;
using Focus.Business.Exceptions;

namespace Focus.Business.FinancialYears.Commands
{
    public class CloseMonthPeriodCommand : IRequest<Message>
    {
        public Guid Id { get; set; }
        public bool Status { get; set; }

        public class Handler : IRequestHandler<CloseMonthPeriodCommand, Message>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private string _code;

            public Handler(IApplicationDbContext context, ILogger<CloseMonthPeriodCommand> logger)
            {
                _context = context;
                _logger = logger;
            }

            public async Task<Message> Handle(CloseMonthPeriodCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var companySubmissionPeriod = await _context.CompanySubmissionPeriods.FindAsync(request.Id);

                    if (companySubmissionPeriod!=null)
                    {
                        companySubmissionPeriod.IsPeriodClosed = request.Status;
                        await _context.SaveChangesAsync(cancellationToken);

                        return new Message()
                        {
                            Id = companySubmissionPeriod.Id,
                            IsSuccess = true,
                            IsAddUpdate = "Status has been updated"
                        };
                    }
                    throw new NotFoundException("Company Submission Period not found","");
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception.Message);
                    throw new ApplicationException("Please Contact to support");
                }
            }
        }
    }
}
