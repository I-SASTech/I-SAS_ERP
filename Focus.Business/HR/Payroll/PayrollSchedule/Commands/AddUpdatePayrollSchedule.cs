using System;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.SyncRecords.Commands;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Focus.Business.HR.Payroll.PayrollSchedule.Commands
{
    public class AddUpdatePayrollSchedule : IRequest<Guid>
    {
        //Get all variable in entity
        public PayrollScheduleLookUpModel PayrollSchedule { get; set; }

        //Create Handler class that inherit from IRequestHandler interface

        public class Handler : IRequestHandler<AddUpdatePayrollSchedule, Guid>
        {
            //Create variable of IApplicationDbContext for Add and update database
            public readonly IApplicationDbContext Context;

            //Create logger interface variable for log error
            public readonly ILogger Logger;
            private string _code;
            private readonly IMediator _mediator;
            //Constructor

            public Handler(IApplicationDbContext context, ILogger<AddUpdatePayrollSchedule> logger, IMediator mediator)
            {
                Context = context;
                Logger = logger;
                _mediator = mediator;
            }


            public async Task<Guid> Handle(AddUpdatePayrollSchedule request, CancellationToken cancellationToken)
            {

                try
                {
                    if (request.PayrollSchedule.Id != Guid.Empty)
                    {

                        var paySchedule = await Context.PaySchedules.FindAsync(request.PayrollSchedule.Id);

                        Random rnd = new Random();
                        for (int i = 0; i < 11; i++)
                        {
                            _code += rnd.Next(0, 9).ToString();
                        }

                        paySchedule.PayPeriod = request.PayrollSchedule.PayPeriod;
                        paySchedule.PayPeriodStartDate = request.PayrollSchedule.PayPeriodStartDate;
                        paySchedule.Name = request.PayrollSchedule.Name;
                        paySchedule.PayPeriodEndDate = request.PayrollSchedule.PayPeriodEndDate;
                        paySchedule.IfPayDayFallOnHoliday = request.PayrollSchedule.IfPayDayFallOnHoliday;
                        paySchedule.PayDate = request.PayrollSchedule.PayDate;
                        paySchedule.Default = request.PayrollSchedule.Default;
                        paySchedule.IsHourlyPay = request.PayrollSchedule.IsHourlyPay;

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = Context.SyncLog(),
                            Code = _code,
                        }, cancellationToken);

                        //Save changes to database
                        await Context.SaveChangesAsync(cancellationToken);

                        return paySchedule.Id;

                    }
                    else
                    {
                        Random rnd = new Random();
                        for (int i = 0; i < 11; i++)
                        {
                            _code += rnd.Next(0, 9).ToString();
                        }

                        var paySchedule = new PaySchedule()
                        {
                            PayPeriod = request.PayrollSchedule.PayPeriod,
                            PayPeriodStartDate = request.PayrollSchedule.PayPeriodStartDate,
                            Name = request.PayrollSchedule.Name,
                            PayPeriodEndDate = request.PayrollSchedule.PayPeriodEndDate,
                            IfPayDayFallOnHoliday = request.PayrollSchedule.IfPayDayFallOnHoliday,
                            PayDate = request.PayrollSchedule.PayDate,
                            Default = request.PayrollSchedule.Default,
                            IsHourlyPay = request.PayrollSchedule.IsHourlyPay
                        };
                        //Add Department to database
                        await Context.PaySchedules.AddAsync(paySchedule, cancellationToken);

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = Context.SyncLog(),
                            Code = _code,
                        }, cancellationToken);

                        await Context.SaveChangesAsync(cancellationToken);
                        return paySchedule.Id;



                    }

                }
                catch (ObjectAlreadyExistsException exception)
                {
                    Logger.LogError(exception.Message);
                    throw new ObjectAlreadyExistsException(exception.Message);
                }
                catch (NotFoundException exception)
                {
                    Logger.LogError(exception.Message);
                    throw new NotFoundException(exception.Message, "");
                }
                catch (Exception exception)
                {
                    Logger.LogError(exception.Message);
                    throw new ApplicationException("Unable to process your request please contact support");
                }
            }
        }
    }
}
