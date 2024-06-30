using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Focus.Business.SyncRecords.Commands;
using System.Net.Mail;

namespace Focus.Business.Attachments.Commands
{
    public class AddAttachmentCommand : IRequest<Guid>
    {
        public List<AttachmentLookUpModel> Attachment { get; set; }

        public class Handler : IRequestHandler<AddAttachmentCommand, Guid>
        {
            //Create variable of IApplicationDbContext for Add and update database
            public readonly IApplicationDbContext Context;

            //Create logger interface variable for log error
            public readonly ILogger Logger;
            private string _code;
            private readonly IMediator _mediator;
            //Constructor

            public Handler(IApplicationDbContext context, ILogger<AddAttachmentCommand> logger, IMediator mediator)
            {
                Context = context;
                Logger = logger;
                _mediator = mediator;
            }


            public async Task<Guid> Handle(AddAttachmentCommand request, CancellationToken cancellationToken)
            {

                try
                {
                    if (request.Attachment.Count > 0)
                    {
                        var documentId = request.Attachment.FirstOrDefault()?.DocumentId;
                        var attachments = Context.Attachments
                            .AsNoTracking()
                            .Where(x => x.DocumentId == documentId)
                            .AsQueryable();

                        Random rnd = new Random();
                        for (int i = 0; i < 11; i++)
                        {
                            _code += rnd.Next(0, 9).ToString();
                        }

                        if (attachments.Any())
                        {
                            Context.Attachments.RemoveRange(attachments);
                        }

                        foreach (var item in request.Attachment)
                        {
                            var attachment = new Domain.Entities.Attachment()
                            {
                                Date = item.Date,
                                DocumentId = item.DocumentId,
                                Description = item.Description,
                                FileName = item.FileName,
                                Path = item.Path
                            };
                            //Add Department to database
                            await Context.Attachments.AddAsync(attachment, cancellationToken);

                        }
                    }

                    await _mediator.Send(new AddUpdateSyncRecordCommand()
                    {
                        SyncRecordModels = Context.SyncLog(),
                        Code = _code,
                  

                    }, cancellationToken);

                    await Context.SaveChangesAsync(cancellationToken);
                    return Guid.NewGuid();

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
