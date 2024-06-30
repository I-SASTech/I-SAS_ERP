using System;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.SyncRecords.Commands;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Focus.Business.HR.Payroll.Allowances.Commands.AddAllowance
{
    public class AddUpdateAllowanceCommand : IRequest<Guid>
    {
        //Get all variable in entity
        public AllowanceLookUp Allowance { get; set; }

        //Create Handler class that inherit from IRequestHandler interface

        public class Handler : IRequestHandler<AddUpdateAllowanceCommand, Guid>
        {
            //Create variable of IApplicationDbContext for Add and update database
            public readonly IApplicationDbContext Context;

            //Create logger interface variable for log error
            public readonly ILogger Logger;
            private string _code;
            private readonly IMediator _mediator;
            //Constructor

            public Handler(IApplicationDbContext context, ILogger<AddUpdateAllowanceCommand> logger, IMediator mediator)
            {
                Context = context;
                Logger = logger;
                _mediator = mediator;
            }


            public async Task<Guid> Handle(AddUpdateAllowanceCommand request, CancellationToken cancellationToken)
            {

                try
                {
                    if (request.Allowance.Id != Guid.Empty)
                    {
                        var allowances = await Context.Allowances.FindAsync(request.Allowance.Id);

                        Random rnd = new Random();
                        for (int i = 0; i < 11; i++)
                        {
                            _code += rnd.Next(0, 9).ToString();
                        }

                        allowances.AllowanceTypeId = request.Allowance.AllowanceTypeId;
                        allowances.AmountType = request.Allowance.AmountType;
                        allowances.TaxPlan = request.Allowance.TaxPlan;
                        allowances.Code = request.Allowance.Code;
                        allowances.Description = request.Allowance.Description;
                        allowances.Amount = request.Allowance.Amount;
                        allowances.IsActive = request.Allowance.IsActive;

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = Context.SyncLog(),
                            Code = _code,
                            DocumentNo=allowances.Code

                        }, cancellationToken);

                        //Save changes to database
                        await Context.SaveChangesAsync(cancellationToken);

                        return allowances.Id;

                    }
                    else
                    {
                        Random rnd = new Random();
                        for (int i = 0; i < 11; i++)
                        {
                            _code += rnd.Next(0, 9).ToString();
                        }

                        var allowance = new Allowance
                        {
                            AllowanceTypeId = request.Allowance.AllowanceTypeId,
                            AmountType = request.Allowance.AmountType,
                            TaxPlan = request.Allowance.TaxPlan,
                            Code = request.Allowance.Code,
                            Description = request.Allowance.Description,
                            Amount = request.Allowance.Amount,
                            IsActive = request.Allowance.IsActive,
                        };
                        //Add Department to database
                        await Context.Allowances.AddAsync(allowance, cancellationToken);

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = Context.SyncLog(),
                            Code = _code,
                            DocumentNo = allowance.Code
                        }, cancellationToken);

                        await Context.SaveChangesAsync(cancellationToken);
                        return allowance.Id;



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
