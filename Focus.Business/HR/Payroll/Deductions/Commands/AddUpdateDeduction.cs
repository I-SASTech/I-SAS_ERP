using System;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.SyncRecords.Commands;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Focus.Business.HR.Payroll.Deductions.Commands
{
    public class AddUpdateDeduction : IRequest<Guid>
    {
        //Get all variable in entity
        public DeductionLookUpModel Deduction { get; set; }

        //Create Handler class that inherit from IRequestHandler interface

        public class Handler : IRequestHandler<AddUpdateDeduction, Guid>
        {
            //Create variable of IApplicationDbContext for Add and update database
            public readonly IApplicationDbContext Context;

            //Create logger interface variable for log error
            public readonly ILogger Logger;
            private string _code;
            private readonly IMediator _mediator;

            public Handler(IApplicationDbContext context, ILogger<AddUpdateDeduction> logger, IMediator mediator)
            {
                Context = context;
                Logger = logger;
                _mediator = mediator;
            }


            public async Task<Guid> Handle(AddUpdateDeduction request, CancellationToken cancellationToken)
            {

                try
                {
                    if (request.Deduction.Id != Guid.Empty)
                    {

                        var deduction = await Context.Deductions.FindAsync(request.Deduction.Id);

                        Random rnd = new Random();
                        for (int i = 0; i < 11; i++)
                        {
                            _code += rnd.Next(0, 9).ToString();
                        }

                        deduction.NameInPayslip = request.Deduction.NameInPayslip;
                        deduction.NameInPayslipArabic = request.Deduction.NameInPayslipArabic;
                        deduction.Code = request.Deduction.Code;
                        deduction.AmountType = request.Deduction.AmountType;
                        deduction.TaxPlan = request.Deduction.TaxPlan;
                        deduction.Amount = request.Deduction.Amount;
                        deduction.Active = request.Deduction.Active;

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = Context.SyncLog(),
                            Code = _code,
                        }, cancellationToken);

                        //Save changes to database
                        await Context.SaveChangesAsync(cancellationToken);

                        return deduction.Id;

                    }
                    else
                    {
                        Random rnd = new Random();
                        for (int i = 0; i < 11; i++)
                        {
                            _code += rnd.Next(0, 9).ToString();
                        }

                        var deduction = new Deduction
                        {
                            AmountType = request.Deduction.AmountType,
                            TaxPlan = request.Deduction.TaxPlan,
                            Code = request.Deduction.Code,
                            NameInPayslip = request.Deduction.NameInPayslip,
                            NameInPayslipArabic = request.Deduction.NameInPayslipArabic,
                            Amount = request.Deduction.Amount,
                            Active = request.Deduction.Active,
                        };
                        //Add Department to database
                        await Context.Deductions.AddAsync(deduction, cancellationToken);

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = Context.SyncLog(),
                            Code = _code,
                        }, cancellationToken);

                        await Context.SaveChangesAsync(cancellationToken);
                        return deduction.Id;



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
