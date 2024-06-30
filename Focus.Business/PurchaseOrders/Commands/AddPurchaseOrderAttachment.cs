using System;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.SyncRecords.Commands;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.PurchaseOrders.Commands
{
    public class AddPurchaseOrderAttachment : IRequest<Guid>
    {
        public Guid? Id { get; set; }
        public string Path { get; set; }
        public DateTime? Date { get; set; }
        public string Description { get; set; }

        public class Handler : IRequestHandler<AddPurchaseOrderAttachment, Guid>
        {
            //Create variable of IApplicationDbContext for Add and update database
            public readonly IApplicationDbContext Context;

            //Create logger interface variable for log error
            public readonly ILogger Logger;
            private readonly IMediator _mediator;
            private string _code;

            public Handler(IApplicationDbContext context, ILogger<AddPurchaseOrderAttachment> logger, IMediator mediator)
            {
                Context = context;
                Logger = logger;
                _mediator = mediator;
            }
            public async Task<Guid> Handle(AddPurchaseOrderAttachment request, CancellationToken cancellationToken)
            {

                try
                {
                    //Add New
                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }

                    var attachment = new PurchaseAttachment()
                    {
                        PurchaseOrderId = request.Id,
                        Path = request.Path,
                        Description = request.Description,
                        Date = request.Date

                    };

                    //Add Department to database
                    await Context.PurchaseAttachments.AddAsync(attachment, cancellationToken);

                    var actionData = await Context.CompanyProcess.AsNoTracking().FirstOrDefaultAsync(x =>
                        (x.ProcessName == "Attachment" || x.ProcessName == "مرفق") && x.Type == "Purchase", cancellationToken: cancellationToken);
                    if (actionData != null)
                    {
                        var actions = new ActionLookUpModel()
                        {
                            PurchaseOrderId = request.Id,
                            ProcessId = actionData.Id,
                            Date = DateTime.UtcNow,
                            Description = "Add Attachment"
                        };
                        var id = await _mediator.Send(new AddUpdateActionCommand
                        {
                            Action = actions
                        }, cancellationToken);
                    }

                    await _mediator.Send(new AddUpdateSyncRecordCommand()
                    {
                        SyncRecordModels = Context.SyncLog(),
                        Code = _code,
                      
                    }, cancellationToken);


                    await Context.SaveChangesAsync(cancellationToken);
                    return attachment.Id;

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
