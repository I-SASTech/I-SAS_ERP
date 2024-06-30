using System;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.SyncRecords.Commands;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Focus.Business.HR.Payroll.Contributions.Commands
{
    public class AddUpdateContribution : IRequest<Guid>
    {
        //Get all variable in entity
        public ContributionLookUpModel Contribution { get; set; }

        //Create Handler class that inherit from IRequestHandler interface

        public class Handler : IRequestHandler<AddUpdateContribution, Guid>
        {
            //Create variable of IApplicationDbContext for Add and update database
            public readonly IApplicationDbContext Context;

            //Create logger interface variable for log error
            public readonly ILogger Logger;
            private string _code;
            private readonly IMediator _mediator;
            //Constructor

            public Handler(IApplicationDbContext context, ILogger<AddUpdateContribution> logger, IMediator mediator)
            {
                Context = context;
                Logger = logger;
                _mediator = mediator;
            }


            public async Task<Guid> Handle(AddUpdateContribution request, CancellationToken cancellationToken)
            {

                try
                {
                    if (request.Contribution.Id != Guid.Empty)
                    {

                        var contribution = await Context.Contributions.FindAsync(request.Contribution.Id);

                        Random rnd = new Random();
                        for (int i = 0; i < 11; i++)
                        {
                            _code += rnd.Next(0, 9).ToString();
                        }

                        contribution.NameInPayslip = request.Contribution.NameInPayslip;
                        contribution.NameInPayslipArabic = request.Contribution.NameInPayslipArabic;
                        contribution.Code = request.Contribution.Code;
                        contribution.AmountType = request.Contribution.AmountType;
                        contribution.Amount = request.Contribution.Amount;
                        contribution.Active = request.Contribution.Active;

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = Context.SyncLog(),
                            Code = _code,
                        }, cancellationToken);

                        //Save changes to database
                        await Context.SaveChangesAsync(cancellationToken);

                        return contribution.Id;

                    }
                    else
                    {
                        Random rnd = new Random();
                        for (int i = 0; i < 11; i++)
                        {
                            _code += rnd.Next(0, 9).ToString();
                        }

                        var contribution = new Contribution
                        {
                            AmountType = request.Contribution.AmountType,
                            Code = request.Contribution.Code,
                            NameInPayslip = request.Contribution.NameInPayslip,
                            NameInPayslipArabic = request.Contribution.NameInPayslipArabic,
                            Amount = request.Contribution.Amount,
                            Active = request.Contribution.Active,
                        };
                        //Add Department to database
                        await Context.Contributions.AddAsync(contribution, cancellationToken);

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = Context.SyncLog(),
                            Code = _code,
                        }, cancellationToken);

                        await Context.SaveChangesAsync(cancellationToken);
                        return contribution.Id;



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
