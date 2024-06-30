using System;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.SyncRecords.Commands;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Focus.Business.PurchaseOrders.Commands
{
    public class AddUpdateActionCommand : IRequest<Guid>
    {
        public bool IsPurchasePost { get; set; }
        public string Code { get; set; }
        public string DocumentNo { get; set; }

        public ActionLookUpModel Action { get; set; }
        public class Handler : IRequestHandler<AddUpdateActionCommand, Guid>
        {
            //Create variable of IApplicationDbContext for Add and update database
            public readonly IApplicationDbContext Context;

            //Create logger interface variable for log error
            public readonly ILogger Logger;
            private string _code;
            private readonly IMediator _mediator;
            //Constructor

            public Handler(IApplicationDbContext context, ILogger<AddUpdateActionCommand> logger, IMediator mediator)
            {
                Context = context;
                Logger = logger;
                _mediator = mediator;
                
            }
            public async Task<Guid> Handle(AddUpdateActionCommand request, CancellationToken cancellationToken)
            {

                try
                {
                   

                    //Add New
                    if (request.IsPurchasePost)
                    {
                        var actions = new PurchaseInvoiceAction()
                        {
                            PurchaseInvoicePostId = request.Action.PurchaseOrderId,
                            ProcessId = request.Action.ProcessId,
                            CurrentDate = DateTime.UtcNow,
                            Date = request.Action.Date,
                            Description = request.Action.Description
                        };

                        //Add Department to database
                        await Context.PurchaseInvoiceActions.AddAsync(actions, cancellationToken);

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = Context.SyncLog(),
                            Code = request.Code,
                            DocumentNo = request.DocumentNo,
                           
                        }, cancellationToken);


                        await Context.SaveChangesAsync(cancellationToken);
                        return actions.Id;
                    }
                    var action = new PurchaseOrderAction()
                    {
                        PurchaseOrderId = request.Action.PurchaseOrderId,
                        ProcessId = request.Action.ProcessId,
                        CurrentDate = DateTime.UtcNow,
                        Date = request.Action.Date,
                        Description = request.Action.Description
                    };

                    //Add Department to database
                    await Context.PurchaseOrderActions.AddAsync(action, cancellationToken);


                    await _mediator.Send(new AddUpdateSyncRecordCommand()
                    {
                        SyncRecordModels = Context.SyncLog(),
                        Code = request.Code,
                        DocumentNo = request.DocumentNo,
                    }, cancellationToken);

                    await Context.SaveChangesAsync(cancellationToken);
                    return action.Id;

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
