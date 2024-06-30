using System;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Exceptions;
using Focus.Business.HR.Payroll.PayrollSchedule.Commands;
using Focus.Business.Interface;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Focus.Business.HR.Payroll.PayrollSchedule.Queries
{
    public class PayrollScheduleDetailQuery : IRequest<PayrollScheduleLookUpModel>
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<PayrollScheduleDetailQuery, PayrollScheduleLookUpModel>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<PayrollScheduleDetailQuery> logger)
            {
                _context = context;
                _logger = logger;
            }
            public async Task<PayrollScheduleLookUpModel> Handle(PayrollScheduleDetailQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var paySchedule = await _context.PaySchedules.FindAsync(request.Id);

                    if (paySchedule != null)
                    {
                        return new PayrollScheduleLookUpModel()
                        {
                            Id = paySchedule.Id,
                            PayPeriod = paySchedule.PayPeriod,
                            PayPeriodStartDate = paySchedule.PayPeriodStartDate,
                            Name = paySchedule.Name,
                            PayPeriodEndDate = paySchedule.PayPeriodEndDate,
                            IfPayDayFallOnHoliday = paySchedule.IfPayDayFallOnHoliday,
                            PayDate = paySchedule.PayDate,
                            Default = paySchedule.Default,
                            IsHourlyPay = paySchedule.IsHourlyPay
                        };
                    }
                    throw new NotFoundException(nameof(paySchedule), request.Id);
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
